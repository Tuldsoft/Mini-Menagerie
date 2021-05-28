using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DescrDetailsMonitor : MonoBehaviour
{
    [SerializeField]
    Text nameText = null;
    [SerializeField]
    Toggle showToggle = null;

    protected Descriptor descr = Descriptor.ActiveDescr;

    InputField nameInput;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        nameInput = nameText.GetComponent<InputField>();
        nameInput.text = descr.Name;
        showToggle.isOn = descr.AlwaysShow;
    }

    public void Toggle_Click()
    {
        descr.AlwaysShow = showToggle.isOn;
    }

    public void Close_Click()
    {
        MenuManager.CloseMenu(gameObject);
        Destroy(gameObject);
    }

    public void Name_Enter(string value)
    {
        // attempt rename
        
        if (!descr.Rename(value))
        {
            Debug.Log("Name already in use");
        }
        nameInput.text = descr.Name;
    }
}
