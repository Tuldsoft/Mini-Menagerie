using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMiniListPanel : ScrollListPanel
{

    // SetPanel() does not need overwriting

    public void NewButton_Click()
    {
        MiniCollection.NewMini();
        monitor.PopulateGrid();
    }

}