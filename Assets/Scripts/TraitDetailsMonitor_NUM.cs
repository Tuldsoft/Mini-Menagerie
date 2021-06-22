using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TraitDetailsMonitor_NUM : TraitDetailsMonitor
{
    [SerializeField]
    Text defaultText = null, precisionText = null, minText = null, maxText = null, incrementText = null;

    InputField defaultIF, minIF, maxIF, incrementIF;

    Trait_NUM traitNUM;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        traitNUM = (trait as Trait_NUM);
        
        defaultIF = defaultText.GetComponent<InputField>();
        minIF = minText.GetComponent<InputField>();
        maxIF = maxText.GetComponent<InputField>();
        incrementIF = incrementText.GetComponent<InputField>();

        RefreshFields();
    }

    public void Default_Enter(string value)
    {
        traitNUM.Default = value;
        RefreshFields();
    }

    public void Min_Enter(string value)
    {
        traitNUM.Min = value;
        RefreshFields();
    }

    public void Max_Enter(string value)
    {
        traitNUM.Max = value;
        RefreshFields();
    }

    public void Increment_Enter(string value)
    {
        traitNUM.Increment = value;
        RefreshFields();
    }

    public void Precision_Increase()
    {
        traitNUM.Precision++;
        RefreshFields();
    }

    public void Precision_Decrease()
    {
        traitNUM.Precision--;
        RefreshFields();
    }

    void RefreshFields()
    {
        defaultIF.text = traitNUM.Default ?? "None";
        minIF.text = traitNUM.Min ?? "None";
        maxIF.text = traitNUM.Max ?? "None";
        incrementIF.text = traitNUM.Increment ?? "1";
        precisionText.text = traitNUM.PrecisionSample;
    }

}
