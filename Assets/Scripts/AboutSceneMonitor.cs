using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AboutSceneMonitor : MonoBehaviour
{
    private void Awake()
    {
        Initializer.Initialize();

    }

    public async void Debug_Click()
    {

        PopupResult pResult = await MenuManager.LaunchPopup(PopupType.YesNo, "This is a test message");

        Debug.Log("Result is: " + pResult.ToString());

    }
}
