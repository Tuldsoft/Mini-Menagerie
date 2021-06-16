using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniTraitListPanel_CHK : MiniTraitListPanel
{
    [SerializeField]
    Toggle TraitToggle;

    public override void SetPanel(object obj, ScrollListMonitor<Trait> monitor)
    {
        base.SetPanel(obj, monitor);

        if (!(trait is Trait_CHK))
            return;

        TraitToggle.GetComponentInChildren<Text>().text = (trait as Trait_CHK).Name;
        TraitToggle.isOn = (trait as Trait_CHK).IsChecked;

    }

    public void CheckBox_Click()
    {
        (trait as Trait_CHK).IsChecked = TraitToggle.isOn;
    }
}
