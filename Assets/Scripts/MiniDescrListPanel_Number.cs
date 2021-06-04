using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniDescrListPanel_Number : MiniDescrListPanel
{
    [SerializeField]
    Text valueText = null;

    InputField valueInput = null;

    Descriptor_Number descr_NUM;

    public override void SetPanel(object obj, ScrollListMonitor<Descriptor> monitor)
    {
        base.SetPanel(obj, monitor);

        descr_NUM = descriptor as Descriptor_Number;

        if (IsNull(descr_NUM)) return;

        valueInput = valueText.GetComponent<InputField>();
        Utils.SetInputFieldColors(valueInput);
        valueInput.text = descr_NUM.Value;
    }

    public void InputText_Edit(string text)
    {
        if (IsNull(descr_NUM)) return;

        descr_NUM.Value = text;
        valueInput.text = descr_NUM.Value;
    }

    public void IncreaseButton_Click()
    {
        if (IsNull(descr_NUM)) return;

        descr_NUM.Increase();
        valueInput.text = descr_NUM.Value;
    }

    public void DecreaseButton_Click()
    {
        if (IsNull(descr_NUM)) return;
    
        descr_NUM.Decrease();
        valueInput.text = descr_NUM.Value;
    }
}