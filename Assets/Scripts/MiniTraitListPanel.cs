using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class MiniTraitListPanel : ScrollListPanel<Trait>
{
    protected Trait trait = null;

    [SerializeField]
    Text nameText = null; 
    
    [SerializeField]
    Button trashButton = null;

    public override void SetPanel(object obj, ScrollListMonitor<Trait> monitor)
    {
        base.SetPanel(obj, monitor);

        if (obj is Trait setTrait)
        {
            trait = setTrait;
            nameText.text = trait.Name;
            trashButton.interactable = !trait.IncludeAll;

            //Utils.SetButtonColors(gameObject.GetComponentInChildren<Button>());
        }
    }

    protected bool IsNull(Trait trait)
    {
        bool test = (trait is null);
        if (test)
            Debug.Log("Trait is null.");
        return test;
    }

    public void RemoveButton_Click()
    {
        MenuManager.LaunchDialogBox(DialogBoxType.OKCancel, $"Remove {trait.Name}?");
        StartCoroutine(MenuManager.WaitForClose(RemoveTrait));
    }

    void RemoveTrait()
    {
        if (DialogBoxMonitor.Response == DialogBoxResponse.OK)
        {
            Mini.ActiveMini.RemoveTrait(trait);
            monitor.PopulateGrid();
        }
        
    }
}