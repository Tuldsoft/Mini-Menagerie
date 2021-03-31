using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Parent of MiniListPanel, potentially others
public abstract class ScrollListPanel : MonoBehaviour
{
    protected ScrollListMonitor monitor = null; // Set by SetPanel()

    public virtual void SetPanel(object obj, ScrollListMonitor monitor)
    {
        this.monitor = monitor;
    }

}
