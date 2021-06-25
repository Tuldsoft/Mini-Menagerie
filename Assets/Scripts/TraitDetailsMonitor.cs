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

        popResult = await MenuManager.LaunchPopup(PopupType.OKCancel, message);

        // old version that used coroutines and delegates and static members
        /*if (trait.IncludeAll) // trait is currently set to true, but toggle just turned off
        {
            MenuManager.LaunchDialogBox(DialogBoxType.OKCancel, "Disabling this does not remove it on existing minis.");
            StartCoroutine(MenuManager.WaitForClose(DBResponse));
        }
        else // trait is currently set to false, but toggle just turned on
        {
            MenuManager.LaunchDialogBox(DialogBoxType.OKCancel, 
                "Enabling this will include the trait on any Mini that does not yet have it yet.");
            StartCoroutine(MenuManager.WaitForClose(DBResponse));
        }*/

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

    // coroutine-based dialogbox is no longer needed, no more referencing static members
    /*void DBResponse()
    {
        if (trait.IncludeAll) // trait is currently set to true, but toggle just turned off
        {
            if (DialogBoxMonitor.Response == DialogBoxResponse.OK)
            {
                trait.IncludeAll = includeToggle.isOn; // false
            }
            else
            {
                // ANYTIME THIS IS SET IT RETRIGGERS THE DIALOGBOX
                includeToggle.isOn = trait.IncludeAll; // re-tick to true
            }
        }
        else  // trait is currently set to false, but toggle just turned on
        {
            if (DialogBoxMonitor.Response == DialogBoxResponse.OK)
            {
                trait.IncludeAll = includeToggle.isOn; // true
                MiniCollection.AddTraitToAllMinis(trait);
            }
            else
            {
                includeToggle.isOn = trait.IncludeAll; // re-tick back to false
            }
        }
        toggling = false;
    }*/

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
