using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuItem : MonoBehaviour
{
    MenuName menuName = MenuName.Browse;
    MenuMonitor monitor = null;

    public void SetMenuItem(MenuName menuName, MenuMonitor monitor)
    {
        gameObject.GetComponent<Text>().text = MenuManager.MenuNamesDict[menuName];
        this.menuName = menuName;
        this.monitor = monitor;

        
    }

    public void MenuItem_Click()
    {
        MenuManager.GoToMenu(menuName);
        monitor.CloseMenu();
    }

}
