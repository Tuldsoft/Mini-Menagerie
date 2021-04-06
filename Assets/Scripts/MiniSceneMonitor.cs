using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MiniSceneMonitor : MonoBehaviour
{
    [SerializeField]
    Text NameText = null;

    [SerializeField]
    GameObject MainScrollContent = null;
    
    // Awake is called before Start
    private void Awake()
    {
        Initializer.Initialize();
    }

    // Start is called before the first frame update
    void Start()
    {
        NameText.GetComponent<InputField>().text = Mini.ActiveMini.Name;
        Debug.Log("MainscrollContent GridLayout cell width is: " +
            MainScrollContent.GetComponent<GridLayoutGroup>().cellSize.x);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BackButton_Click()
    {
        MenuManager.GoToMenu(MenuName.Browse);

    }

    public void NameText_Edit(string value)
    {
        Mini.ActiveMini.Rename(value);
        NameText.text = Mini.ActiveMini.Name;
    }
}
