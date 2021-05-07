using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Parent of MiniListPanel, potentially others
public abstract class ScrollListPanel<T> : MonoBehaviour where T : IComparable<T>
{
    protected ScrollListMonitor<T> monitor = null; // Set by SetPanel()

    public virtual void SetPanel(object obj, ScrollListMonitor<T> monitor)
    {
        this.monitor = monitor;
    }

}
