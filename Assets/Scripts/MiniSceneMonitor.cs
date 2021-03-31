using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MiniSceneMonitor : MonoBehaviour
{
    [SerializeField]
    Text aText = null;
    
    // Awake is called before Start
    private void Awake()
    {
        Initializer.Initialize();
    }

    // Start is called before the first frame update
    void Start()
    {
        aText.text = MenuManager.ActiveMini.Name;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
