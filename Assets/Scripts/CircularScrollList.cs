using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CircularScrollList : MonoBehaviour
{
    public bool InitByUser = false;
    private ScrollRect _scrollRect;
    private ContentSizeFitter _contentSizeFitter;
    private VerticalLayoutGroup _verticalLayoutGroup;
    private HorizontalLayoutGroup _horizontalLayoutGroup;
    private GridLayoutGroup _gridLayoutGroup;
    private bool _isVertical = false;
    private bool _isHorizontal = false;
    private bool _hasDisabledGridComponents = false;
    private List<RectTransform> items = new List<RectTransform>();
    private Vector2 _newAnchoredPosition = Vector2.zero;
    private float _treshold = 100f;
    private int _itemCount = 0;
    private float _recordOffsetX = 0;
    private float _recordOffsetY = 0;

    void Awake()
    {
        if (!InitByUser)
            Init();
    }

    public void Init()
    {
        if (GetComponent<ScrollRect>() != null)
        {
            _scrollRect = GetComponent<ScrollRect>();
            _scrollRect.onValueChanged.AddListener(OnScroll);
            _scrollRect.movementType = ScrollRect.MovementType.Unrestricted;

            for (int i = 0; i < _scrollRect.content.childCount; i++)
            {
                items.Add(_scrollRect.content.GetChild(i).GetComponent<RectTransform>());
            }
            if (_scrollRect.content.GetComponent<VerticalLayoutGroup>() != null)
            {
                _verticalLayoutGroup = _scrollRect.content.GetComponent<VerticalLayoutGroup>();
            }
            if (_scrollRect.content.GetComponent<HorizontalLayoutGroup>() != null)
            {
                _horizontalLayoutGroup = _scrollRect.content.GetComponent<HorizontalLayoutGroup>();
            }
            if (_scrollRect.content.GetComponent<GridLayoutGroup>() != null)
            {
                _gridLayoutGroup = _scrollRect.content.GetComponent<GridLayoutGroup>();
            }
            if (_scrollRect.content.GetComponent<ContentSizeFitter>() != null)
            {
                _contentSizeFitter = _scrollRect.content.GetComponent<ContentSizeFitter>();
            }

            _isHorizontal = _scrollRect.horizontal;
            _isVertical = _scrollRect.vertical;

            if (_isHorizontal && _isVertical)
            {
                Debug.LogError("UI_InfiniteScroll doesn't support scrolling in both directions, please choose one direction (horizontal or vertical)");
            }

            _itemCount = _scrollRect.content.childCount;
        }
        else
        {
            Debug.LogError("UI_InfiniteScroll => No ScrollRect component found");
        }
    }

    void DisableGridComponents()
    {
        if (_isVertical)
        {
            _recordOffsetY = items[0].GetComponent<RectTransform>().anchoredPosition.y - items[1].GetComponent<RectTransform>().anchoredPosition.y;
        }
        if (_isHorizontal)
        {
            _recordOffsetX = items[1].GetComponent<RectTransform>().anchoredPosition.x - items[0].GetComponent<RectTransform>().anchoredPosition.x;
        }

        if (_verticalLayoutGroup)
        {
            _verticalLayoutGroup.enabled = false;
        }
        if (_horizontalLayoutGroup)
        {
            _horizontalLayoutGroup.enabled = false;
        }
        if (_contentSizeFitter)
        {
            _contentSizeFitter.enabled = false;
        }
        if (_gridLayoutGroup)
        {
            _gridLayoutGroup.enabled = false;
        }
        _hasDisabledGridComponents = true;
    }

    public void OnScroll(Vector2 pos)
    {
        if (!_hasDisabledGridComponents)
            DisableGridComponents();

        float scrollPercentage = (_isHorizontal ? pos.x : pos.y);
        UpdateItemPositionsAlongArc(scrollPercentage);
    }

    private void UpdateItemPositionsAlongArc(float scrollPercentage)
    {
        for (int i = 0; i < items.Count; i++)
        {
            Vector2 newPosition = CalculateItemPositionAlongArc(i, scrollPercentage);
            items[i].anchoredPosition = newPosition;
        }
    }

    private Vector2 CalculateItemPositionAlongArc(int index, float scrollPercentage)
    {
        // Example: Using a quadratic Bezier curve for simplicity
        float t = (float)index / (_itemCount - 1);  // Normalize index to [0, 1]
        Vector2 p0 = Vector2.zero;  // Starting point
        Vector2 p1 = new Vector2(100f, 300f);  // Control point
        Vector2 p2 = new Vector2(200f, 0f);  // End point
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        float uuu = uu * u;
        float ttt = tt * t;

        Vector2 p = uuu * p0; // (1-t)^3 * p0
        p += 3 * uu * t * p1; // 3(1-t)^2 * t * p1
        p += 3 * u * tt * p2; // 3(1-t) * t^2 * p2
        p += ttt * Vector2.right; // t^3 * p3 (assuming the curve moves to the right)

        float curveWidth = 100f;  // Adjust this value based on your curve
        float normalizedPosition = scrollPercentage % 1f;
        float xOffset = normalizedPosition * curveWidth;

        return p + new Vector2(xOffset, 0f);
    }
}

