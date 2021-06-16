using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TraitDetailsMonitor_CHK : TraitDetailsMonitor
{
    [SerializeField]
    Toggle defaultToggle = null;

    Trait_CHK traitCHK;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        traitCHK = (trait as Trait_CHK);
        defaultToggle.isOn = traitCHK.DefaultIsChecked;
    }

    public void Default_Click()
    {
        traitCHK.DefaultIsChecked = defaultToggle.isOn;

    }
}
