using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniDescrListPanel_Text : MiniDescrListPanel
{
    [SerializeField]
    Text InputText = null;

    public override void SetPanel(object obj, ScrollListMonitor<Descriptor> monitor)
    {
        base.SetPanel(obj, monitor);

        if (!(descriptor is Descriptor_Text)) 
            return;
        
        InputText.GetComponent<InputField>().text = (descriptor as Descriptor_Text).Text;

    }

    public void InputText_Edit(string text)
    {
        if (!(descriptor is Descriptor_Text))
            return;

        (descriptor as Descriptor_Text).Text = text;
    }
}
