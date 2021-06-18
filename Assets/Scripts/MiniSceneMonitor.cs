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

    ScrollListMonitor<Trait> traitListMonitor;

    // Awake is called before Start
    private void Awake()
    {
        Initializer.Initialize();
    }

    // Start is called before the first frame update
    void Start()
    {
        NameText.GetComponent<InputField>().text = Mini.ActiveMini.Name;

        // Resize cell size X to expand/shrink to available area
        float canvasX = GetComponent<RectTransform>().rect.width;
        float cellSizeY = MainScrollContent.GetComponent<GridLayoutGroup>().cellSize.y;
        float paddingX = MainScrollContent.GetComponent<GridLayoutGroup>().padding.left;
        paddingX += MainScrollContent.GetComponent<GridLayoutGroup>().padding.right;
        Vector2 newCellSize = new Vector2 ( canvasX - paddingX, cellSizeY );

        MainScrollContent.GetComponent<GridLayoutGroup>().cellSize = newCellSize;

        traitListMonitor = gameObject.GetComponentInChildren<ScrollListMonitor<Trait>>();

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

    public void ShowHide_Click()
    {
        MenuManager.GoToMenu(MenuName.ShowHide);
        StartCoroutine(MenuManager.WaitForClose(traitListMonitor.PopulateGrid));
    }
}
