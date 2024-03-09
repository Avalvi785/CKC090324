using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfiniteScrollView : MonoBehaviour
{
    public ScrollRect ScrollRect;
    public RectTransform ViewPortTransform;
    public RectTransform ContentPanelTransform;
    public HorizontalLayoutGroup HLG;
    public RectTransform[] Itemlist;
    Vector2 OldVelocity;
    bool isUpdated;
    // Start is called before the first frame update
    void Start()
    {
        isUpdated = false;
        OldVelocity = Vector2.zero;
        int itemsToadd = Mathf.CeilToInt(ViewPortTransform.rect.width / (Itemlist[0].rect.width + HLG.spacing));
        for (int i = 0; i < itemsToadd; i++)
        {
            RectTransform rt = Instantiate(Itemlist[i % Itemlist.Length], ContentPanelTransform);
            rt.SetAsLastSibling();
        }
        for (int i = 0; i < itemsToadd; i++)
        {
            int num = Itemlist.Length - i - 1;
            while (num < 0)
            {
                num += Itemlist.Length;
            }
            RectTransform rt = Instantiate(Itemlist[i % Itemlist.Length], ContentPanelTransform);
            rt.SetAsFirstSibling();
        }

        ContentPanelTransform.localPosition = new Vector3((0 - (Itemlist[0].rect.width + HLG.spacing) * itemsToadd), ContentPanelTransform.localPosition.y,
            ContentPanelTransform.localPosition.z);

    }

    // Update is called once per frame
    void Update()
    {
        if (isUpdated)
        {
            isUpdated = false;
            ScrollRect.velocity = OldVelocity;
        }
        if (ContentPanelTransform.localPosition.x > 0)
        {
            Canvas.ForceUpdateCanvases();
            OldVelocity = ScrollRect.velocity;
            ContentPanelTransform.localPosition -= new Vector3(Itemlist.Length * (Itemlist[0].rect.width + HLG.spacing), 0, 0);
            isUpdated = true;
        }
        if (ContentPanelTransform.localPosition.x < 0 - (Itemlist.Length * (Itemlist[0].rect.width + HLG.spacing)))
        {
            Canvas.ForceUpdateCanvases();
            OldVelocity = ScrollRect.velocity;
            ContentPanelTransform.localPosition += new Vector3(Itemlist.Length * (Itemlist[0].rect.width + HLG.spacing), 0, 0);
            isUpdated = true;
        }
    }
}
