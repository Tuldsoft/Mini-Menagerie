using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NewMiniListPanel : ScrollListPanel<Mini>
{

    // SetPanel() does not need overwriting

    public void NewButton_Click()
    {
        MiniCollection.NewMini();
        monitor.PopulateGrid(true); // include ScrollToEnd()
    }

}
