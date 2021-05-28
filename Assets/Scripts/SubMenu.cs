using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubMenu : MonoBehaviour
{
    public void Close_Click()
    {
        MenuManager.CloseMenu(gameObject);
    }
}
