using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Attaches to panels in Scrolllists. Maintains a reference to its monitor.
// Has many children. Each child adds to the SetPanel() method.
public abstract class ScrollListPanel<T> : MonoBehaviour where T : IComparable<T>
{
    protected ScrollListMonitor<T> monitor = null; // Set by SetPanel()

    public virtual void SetPanel(object obj, ScrollListMonitor<T> monitor)
    {
        this.monitor = monitor;
    }

}
