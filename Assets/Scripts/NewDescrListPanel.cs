using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewDescrListPanel : ScrollListPanel<Descriptor>
{

    // SetPanel() does not need overwriting

    public void NewButton_Click()
    {
        MenuManager.GoToMenu(MenuName.DescrPicker);

        StartCoroutine(WaitForClose());
    }

    IEnumerator WaitForClose()
    {
        yield return new WaitWhile(MenuManager.MenuOpen);
        monitor.PopulateGrid();
        yield return null;
    }

}
