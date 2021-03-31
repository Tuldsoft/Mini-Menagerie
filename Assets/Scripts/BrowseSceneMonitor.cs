using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrowseSceneMonitor : MonoBehaviour
{
    MiniListMonitor miniListMonitor = null;



    // Awake is called before Start()
    private void Awake()
    {
        Initializer.Initialize();
    }

    // Start is called before the first frame update
    void Start()
    {
        miniListMonitor = GetComponentInChildren<MiniListMonitor>();
    }
        
}
