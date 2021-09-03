using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewTraitListPanel : ScrollListPanel<Trait>
{

    // SetPanel() does not need overwriting

    public void NewButton_Click()
    {
        // Make an async version of traitpicker launch
        MenuManager.GoToMenu(MenuName.TraitPicker);

        StartCoroutine(MenuManager.WaitForClose(monitor.PopulateGrid));
    }

}
