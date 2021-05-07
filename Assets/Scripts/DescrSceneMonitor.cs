using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DescrSceneMonitor : MonoBehaviour
{
    // Awake is called before Start()
    private void Awake()
    {
        Initializer.Initialize();
    }

    public void Menu_Click()
    {
        MenuManager.GoToMenu(MenuName.Menu);
    }



}
