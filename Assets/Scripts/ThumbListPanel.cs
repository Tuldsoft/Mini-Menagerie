using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// T is Mini, because it can't be a Sprite
public class ThumbListPanel : ScrollListPanel<Mini>
{
    Sprite sprite = null;

    [SerializeField]
    Image thumbImage = null;


    public override void SetPanel(object obj, ScrollListMonitor<Mini> monitor)
    {
        base.SetPanel(obj, monitor);

        if (!(obj is Sprite setSprite))
            return;

        sprite = setSprite;

        thumbImage.sprite = sprite;
        // Utils.SetButtonColors(GetComponent<Button>()); would make buttons invis until mouseover

    }

    public void Panel_Click()
    {
        // later
        if (monitor is ThumbListMonitor)
            (monitor as ThumbListMonitor).BigThumb.sprite = thumbImage.sprite;

        Mini.ActiveMini.SetThumbnail(thumbImage.sprite);
    }
}
