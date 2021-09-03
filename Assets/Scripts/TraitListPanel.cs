using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Pieces of the TraitPicker, etc.
public class TraitListPanel : ScrollListPanel<Trait>
{
    [SerializeField]
    protected Text nameText;
    [SerializeField]
    protected Image typeImage, radiantImage, allMinisImage, trashbutton;

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
            typeImage.sprite = ImageUtils.TraitTypeSprites[tr.TType];
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


    // used by both TraitListMonitor and TraitPickerMonitor
    public virtual void Panel_Click()
    {
        if (isPickerPanel)
        {
            Mini.ActiveMini.Traits.AddTrait(Trait.Copy(trait));

            // PickerList -> Canvas -> prefab
            MenuManager.CloseMenu(monitor.gameObject.transform.parent.transform.parent.gameObject);
        }
        else
        {
            Trait.SetActive(trait);
            MenuManager.GoToMenu(MenuName.TraitDetails);

            StartCoroutine(MenuManager.WaitForClose(monitor.PopulateGrid));
        }
    }

    public async void TrashButton_Click()
    {
        PopupResult popResult = await MenuManager.LaunchPopupAsync(PopupType.OKCancel,
            $"Destroying {trait.Name} will remove it from all minis. Proceed?");

        if (popResult == PopupResult.OK)
        {
            MiniCollection.RemoveTraitFromAllMinis(trait);
            Trait.RemoveTrait(trait);
            monitor.PopulateGrid();
        }
    }



}
