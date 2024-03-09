using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollUsingButton : MonoBehaviour
{
    public ScrollRect scrollRect;
    public float scrollSpeed = 5f;
    public ScrollButton leftButton;
    //public Button RightButton;
    private void Start()
    {
    }
    private void Update()
    {
        if (leftButton.isDown)
        {
            ScrollLeft();
        }
    }

    public void ScrollLeft()
    {
        if (scrollRect != null)
        {
            if (scrollRect.horizontalNormalizedPosition >= 0)
            {
                Debug.LogError("1111");
                scrollRect.horizontalNormalizedPosition -= scrollSpeed;
            }
        }
    }
}
