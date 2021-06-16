using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TraitDetailsMonitor_TXT : TraitDetailsMonitor
{
    [SerializeField]
    Text defaultText = null;

    InputField defaultInputField;

    Trait_TXT traitTXT;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        traitTXT = (trait as Trait_TXT);
        defaultInputField = defaultText.GetComponent<InputField>();
        defaultInputField.text = traitTXT.DefaultText;

    }
    
    public void Default_Enter(string value)
    {
        traitTXT.DefaultText = value;
        defaultInputField.text = traitTXT.DefaultText;
    }
}
