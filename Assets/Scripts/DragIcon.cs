using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragIcon : MonoBehaviour, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [HideInInspector] public Transform ParentAfterDrag;
    [HideInInspector] public bool IsDropped;
    public Image ItemImage;
    public float MinTimeToHold = 2f;
    public string directory;
    private float clickTime;
    private bool canDrag = false;
    private bool isPointerDown = false;
    private bool isDragging = false;
    private Transform intialParent;
    private void Start()
    {
        intialParent = transform.parent;
    }
    private void Update()
    {
        if (isPointerDown)
        {
            if (!canDrag)
            {
                if (Time.time - clickTime >= MinTimeToHold && !isDragging)
                {
                    canDrag = true;
                }
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        isDragging = true;
        if (canDrag)
        {
            ItemImage.raycastTarget = false;
            transform.SetParent(transform.root);
            transform.SetAsLastSibling();
            transform.position = Input.mousePosition;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;
        canDrag = false;
        transform.SetParent(ParentAfterDrag);
        ItemImage.raycastTarget = true;
        transform.localPosition = Vector3.zero;

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isPointerDown = true;
        clickTime = Time.time;
        ParentAfterDrag = transform.parent;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPointerDown = false;
    }
   

    public void ResetPosition()
    {
        transform.SetParent(intialParent);
        transform.localPosition = Vector3.zero;
    }
}
