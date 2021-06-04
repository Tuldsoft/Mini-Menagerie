using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DescrDetailsMonitor_NUM : DescrDetailsMonitor
{
    [SerializeField]
    Text defaultText = null, precisionText = null, minText = null, maxText = null, incrementText = null;

    InputField defaultIF, minIF, maxIF, incrementIF;

    Descriptor_Number descrNUM;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        descrNUM = (descr as Descriptor_Number);
        
        defaultIF = defaultText.GetComponent<InputField>();
        minIF = minText.GetComponent<InputField>();
        maxIF = maxText.GetComponent<InputField>();
        incrementIF = incrementText.GetComponent<InputField>();

        RefreshFields();
    }

    public void Default_Enter(string value)
    {
        descrNUM.Default = value;
        RefreshFields();
    }

    public void Min_Enter(string value)
    {
        descrNUM.Min = value;
        RefreshFields();
    }

    public void Max_Enter(string value)
    {
        descrNUM.Max = value;
        RefreshFields();
    }

    public void Increment_Enter(string value)
    {
        descrNUM.Increment = value;
        RefreshFields();
    }

    public void Precision_Increase()
    {
        descrNUM.Precision++;
        RefreshFields();
    }

    public void Precision_Decrease()
    {
        descrNUM.Precision--;
        RefreshFields();
    }

    void RefreshFields()
    {
        defaultIF.text = descrNUM.Default ?? "None";
        minIF.text = descrNUM.Min ?? "None";
        maxIF.text = descrNUM.Max ?? "None";
        incrementIF.text = descrNUM.Increment ?? "1";
        precisionText.text = descrNUM.Precision.ToString();
    }

}
