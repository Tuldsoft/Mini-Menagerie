using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DescrListPanel : ScrollListPanel<Descriptor>
{
    [SerializeField]
    Text nameText;
    [SerializeField]
    Image typeImage, radiantImage, defaultImage, trashbutton;

    Descriptor descriptor = null;

    public override void SetPanel(object obj, ScrollListMonitor<Descriptor> monitor)
    {
        base.SetPanel(obj, monitor); // registers monitor

        // setup UI elements
        if (obj is Descriptor d)
        {
            descriptor = d;
            nameText.text = d.Name;
            typeImage.sprite = ImageUtils.DescrTypeSprites[d.Type];
            radiantImage.enabled = d.AlwaysShow;
            //default image is included in the prefab
            //trashbutton image is included in the prefab
            
            // below code only used for text buttons, sets background colors
            //Utils.SetButtonColors(nameText.GetComponentInChildren<Button>());

        }
    }

    public void Panel_Click()
    {
        Descriptor.SetActive(descriptor);
        MenuManager.GoToMenu(MenuName.DescrDetails);

        StartCoroutine(WaitForClose());
    }

    IEnumerator WaitForClose()
    {
        yield return new WaitWhile(MenuManager.MenuOpen);
        monitor.PopulateGrid();
        yield return null;
    }
}
