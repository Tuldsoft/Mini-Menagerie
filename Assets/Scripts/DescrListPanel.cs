using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DescrListPanel : ScrollListPanel<Descriptor>
{
    [SerializeField]
    protected Text nameText;
    [SerializeField]
    protected Image typeImage, radiantImage, defaultImage, trashbutton;

    protected Descriptor descriptor = null;
    bool isPickerPanel = false;

    public override void SetPanel(object obj, ScrollListMonitor<Descriptor> monitor)
    {
        base.SetPanel(obj, monitor); // registers monitor

        // setup UI elements
        if (obj is Descriptor d)
        {
            descriptor = d;
            nameText.text = d.Name;
            typeImage.sprite = ImageUtils.DescrTypeSprites[d.Type];
            radiantImage.enabled = d.AlwaysShow;
            //default image is included in the prefab
            //trashbutton image is included in the prefab

            if (monitor is DescrPickerMonitor)
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
            Mini.ActiveMini.AddDescriptor(Descriptor.Copy(descriptor));

            //monitor.PopulateGrid(true); // scroll to end

            // PickerList -> Canvas -> prefab
            MenuManager.CloseMenu(monitor.gameObject.transform.parent.transform.parent.gameObject);
        }
        else
        {
            Descriptor.SetActive(descriptor);
            MenuManager.GoToMenu(MenuName.DescrDetails);

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
