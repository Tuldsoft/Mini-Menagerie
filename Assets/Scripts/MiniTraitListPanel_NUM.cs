using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniTraitListPanel_NUM : MiniTraitListPanel
{
    [SerializeField]
    Text valueText = null;

    InputField valueInput = null;

    Trait_NUM traitNUM;

    public override void SetPanel(object obj, ScrollListMonitor<Trait> monitor)
    {
        base.SetPanel(obj, monitor);

        traitNUM = trait as Trait_NUM;

        if (IsNull(traitNUM)) return;

        valueInput = valueText.GetComponent<InputField>();
        Utils.SetInputFieldColors(valueInput);
        valueInput.text = traitNUM.Value;
    }

    public void InputText_Edit(string text)
    {
        if (IsNull(traitNUM)) return;

        traitNUM.Value = text;
        valueInput.text = traitNUM.Value;
    }

    public void IncreaseButton_Click()
    {
        if (IsNull(traitNUM)) return;

        traitNUM.Increase();
        valueInput.text = traitNUM.Value;
    }

    public void DecreaseButton_Click()
    {
        if (IsNull(traitNUM)) return;
    
        traitNUM.Decrease();
        valueInput.text = traitNUM.Value;
    }
}