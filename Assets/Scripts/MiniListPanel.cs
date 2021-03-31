using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MiniListPanel : ScrollListPanel
{
    Mini mini = null;
    
    [SerializeField]
    Text nameText = null;

    


    public override void SetPanel(object obj, ScrollListMonitor monitor)
    {
        base.SetPanel(obj, monitor);

        if (!(obj is Mini setMini))
            return;

        mini = setMini;
        
        nameText.text = mini.ToString();

        Utils.SetButtonColors(gameObject.GetComponentInChildren<Button>());

    }

    public void Panel_Click()
    {
        MenuManager.ActiveMini = mini;
        MenuManager.GoToMenu(MenuName.View_Mini);

    }
    
}
