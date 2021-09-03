using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TraitDetailsMonitor : MonoBehaviour
{
    [SerializeField]
    Text nameText = null;
    [SerializeField]
    Toggle includeToggle = null;

    bool toggling = false;

    protected Trait trait = Trait.ActiveTrait;

    InputField nameInput;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        nameInput = nameText.GetComponent<InputField>();
        nameInput.text = trait.Name;
        toggling = true; // disables the dialogbox from showing, and from messing up existing minis
        includeToggle.isOn = trait.IncludeAll;
        toggling = false;
    }

    public async void IncludeToggle_Click()
    {
        if (toggling) return; // this method may call itself, btu should only run once
        toggling = true;      // set to false after method is complete, to prevent multiple calls

        // if IncludeAll, trait is true but toggle just turned off
        // if !IncludeAll, trait is false but toggle just turned on
        
        PopupResult popResult;
        string message = trait.IncludeAll ?
            "Disabling this does not remove it on existing minis." :
            "Enabling this will include the trait on any Mini that does not yet have it yet.";

        popResult = await MenuManager.LaunchPopupAsync(PopupType.OKCancel, message);

        // result handling now done asyncronously, in-method
        if (popResult == PopupResult.OK)
        {
            // carry the toggle change to the trait
            trait.IncludeAll = includeToggle.isOn;
            // if the change turned on IncludeAll, add the trait to all minis
            if (trait.IncludeAll)
                MiniCollection.AddTraitToAllMinis(trait);
        }
        else
        {
            // Cancel: remove the toggle, leave IncludeAll as-is
            includeToggle.isOn = trait.IncludeAll;
        }

        toggling = false;
    }

    public void Close_Click()
    {
        MenuManager.CloseMenu(gameObject);
        Destroy(gameObject);
    }

    public void Name_Enter(string value)
    {
        // attempt rename
        
        if (!trait.Rename(value))
        {
            Debug.Log("Name already in use");
        }
        nameInput.text = trait.Name;
    }

    
}
