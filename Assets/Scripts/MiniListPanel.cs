using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MiniListPanel : ScrollListPanel<Mini>
{
    Mini mini = null;
    
    [SerializeField]
    Text nameText = null;
    [SerializeField]
    Image thumbImage = null;

    


    public override void SetPanel(object obj, ScrollListMonitor<Mini> monitor)
    {
        base.SetPanel(obj, monitor);

        if (!(obj is Mini setMini))
            return;

        mini = setMini;
        
        nameText.text = mini.ToString();
        thumbImage.sprite = mini.Thumbnail;

        Utils.SetButtonColors(gameObject.GetComponentInChildren<Button>());

    }

    public void Panel_Click()
    {
        Mini.SetActiveMini(mini);
        MenuManager.GoToMenu(MenuName.View_Mini);

    }
    
}
