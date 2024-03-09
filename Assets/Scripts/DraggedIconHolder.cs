using UnityEngine.EventSystems;
using UnityEngine;
using System;

public class DraggedIconHolder : MonoBehaviour, IDropHandler
{
    public UIManager uiManager;
    public GalleryManager galleryManager;
    private GameObject dropped;
    private DragIcon draggableItem;
    public static event Action isIconDropped;
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            dropped = eventData.pointerDrag;
            draggableItem = dropped.GetComponent<DragIcon>();
            draggableItem.ParentAfterDrag = transform;
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;

            uiManager.EnableScreen(2);
            isIconDropped?.Invoke();
            galleryManager.OnCategoryButtonClick(draggableItem.directory);

        }
    }

    public void ResetHolderData()
    {
        if (draggableItem != null)
        {
            draggableItem.ResetPosition();
        }
    }
}
