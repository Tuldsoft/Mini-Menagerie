using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowHideListPanel : ScrollListPanel<Trait>
{
    Trait trait = null;

    [SerializeField]
    Text nameText = null, showLabel = null, hideLabel = null;

    [SerializeField]
    Slider showHideSlider = null;

    public override void SetPanel(object obj, ScrollListMonitor<Trait> monitor)
    {
        base.SetPanel(obj, monitor);

        if (obj is Trait setTrait)
        {
            trait = setTrait;
            nameText.text = trait.Name;
            showHideSlider.value = trait.Show ? 1 : 0;
            ShowHideLabels(trait.Show);
        }
    }

    public void ShowHideSlider_Change(float value)
    {
        trait.Show = FloatToBool(value);
        ShowHideLabels(trait.Show);
    }

    bool FloatToBool(float value) 
    { 
        return Mathf.RoundToInt(value) != 0;
    }

    void ShowHideLabels(bool show)
    {
        if (show)
        {
            showLabel.color = Color.white;
            hideLabel.color = Color.gray;
        }
        else
        {
            showLabel.color = Color.gray;
            hideLabel.color = Color.white;
        }
    }
}
