using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniDescrListPanel_Number : MiniDescrListPanel
{
    [SerializeField]
    Text valueText = null;

    InputField valueInput = null;

    public override void SetPanel(object obj, ScrollListMonitor<Descriptor> monitor)
    {
        base.SetPanel(obj, monitor);

        if (descriptor is Descriptor_Number dn)
        {
            valueInput = valueText.GetComponent<InputField>();
            Utils.SetInputFieldColors(valueInput);
            valueInput.text = dn.Value;
        }
    }

    public void InputText_Edit(string text)
    {
        if (descriptor is Descriptor_Number dn)
        {
            dn.Value = text;
        }
    }

    public void IncreaseButton_Click()
    {
        if (descriptor is Descriptor_Number dn)
        {
            dn.Increase();
            valueInput.text = dn.Value;
        }
    }

    public void DecreaseButton_Click()
    {
        if (descriptor is Descriptor_Number dn)
        {
            dn.Decrease();
            valueInput.text = dn.Value;
        }
    }
}