using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniDescrListPanel_CheckBox : MiniDescrListPanel
{
    [SerializeField]
    Toggle DescrToggle;

    public override void SetPanel(object obj, ScrollListMonitor<Descriptor> monitor)
    {
        base.SetPanel(obj, monitor);

        if (!(descriptor is Descriptor_CheckBox))
            return;

        DescrToggle.GetComponentInChildren<Text>().text = (descriptor as Descriptor_CheckBox).Name;
        DescrToggle.isOn = (descriptor as Descriptor_CheckBox).IsChecked;

    }

    public void CheckBox_Click()
    {
        (descriptor as Descriptor_CheckBox).IsChecked = DescrToggle.isOn;
    }
}
