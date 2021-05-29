using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DescrDetailsMonitor_CHK : DescrDetailsMonitor
{
    [SerializeField]
    Toggle defaultToggle = null;

    Descriptor_CheckBox descrCHK;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        descrCHK = (descr as Descriptor_CheckBox);
        defaultToggle.isOn = descrCHK.DefaultIsChecked;
    }

    public void Default_Click()
    {
        descrCHK.DefaultIsChecked = defaultToggle.isOn;

    }
}
