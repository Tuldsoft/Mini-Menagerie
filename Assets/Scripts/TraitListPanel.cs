using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TraitListPanel : ScrollListPanel<Trait>
{
    [SerializeField]
    protected Text nameText;
    [SerializeField]
    protected Image typeImage, radiantImage, defaultImage, trashbutton;

    protected Trait trait = null;
    bool isPickerPanel = false;

    public override void SetPanel(object obj, ScrollListMonitor<Trait> monitor)
    {
        base.SetPanel(obj, monitor); // registers monitor

        // setup UI elements
        if (obj is Trait tr)
        {
            trait = tr;
            nameText.text = tr.Name;
            typeImage.sprite = ImageUtils.TraitTypeSprites[tr.Type];
            radiantImage.enabled = tr.IncludeAll;
            //default image is included in the prefab
            //trashbutton image is included in the prefab

            if (monitor is TraitPickerMonitor)
                SetAsPickerPanel();
        }
    }

    void SetAsPickerPanel()
    {
        isPickerPanel = true;

        // remove trash button and shift type and default to the right
        trashbutton.enabled = false;
        typeImage.GetComponent<RectTransform>().anchoredPosition
            += new Vector2(100f, 0f);

        radiantImage.transform.parent.gameObject.transform.localPosition
            += new Vector3(100f, 0f);
        nameText.GetComponent<RectTransform>().offsetMax += new Vector2(100f, 0f);
        
    }


    public virtual void Panel_Click()
    {
        if (isPickerPanel)
        {
            Mini.ActiveMini.AddTrait(Trait.Copy(trait));

            //monitor.PopulateGrid(true); // scroll to end

            // PickerList -> Canvas -> prefab
            MenuManager.CloseMenu(monitor.gameObject.transform.parent.transform.parent.gameObject);
        }
        else
        {
            Trait.SetActive(trait);
            MenuManager.GoToMenu(MenuName.TraitDetails);

            StartCoroutine(WaitForClose());
        }
    }

    IEnumerator WaitForClose()
    {
        yield return new WaitWhile(MenuManager.MenuOpen);
        monitor.PopulateGrid();
        yield return null;
    }
}
