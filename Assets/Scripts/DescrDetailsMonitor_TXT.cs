using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DescrDetailsMonitor_TXT : DescrDetailsMonitor
{
    [SerializeField]
    Text defaultText = null;

    InputField defaultInputField;

    Descriptor_Text descrTXT;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        descrTXT = (descr as Descriptor_Text);
        defaultInputField = defaultText.GetComponent<InputField>();
        defaultInputField.text = descrTXT.DefaultText;

    }
    
    public void Default_Enter(string value)
    {
        descrTXT.DefaultText = value;
        defaultInputField.text = descrTXT.DefaultText;
    }
}
