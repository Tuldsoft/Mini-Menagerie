using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniTraitListPanel_TXT : MiniTraitListPanel
{
    [SerializeField]
    Text InputText = null;

    public override void SetPanel(object obj, ScrollListMonitor<Trait> monitor)
    {
        base.SetPanel(obj, monitor);

        if (!(trait is Trait_TXT)) 
            return;
        
        InputText.GetComponent<InputField>().text = (trait as Trait_TXT).Text;

    }

    public void InputText_Edit(string text)
    {
        if (!(trait is Trait_TXT))
            return;

        (trait as Trait_TXT).Text = text;
    }
}
