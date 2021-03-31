using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

static public class Utils
{
    
    static readonly Color Button_ImageColor     = new Color(1f, 1f, 1f, 1f);
    static readonly Color Button_NormalColor    = new Color(0f, 0f, 0f, 0f);
    static readonly Color Button_HighlightColor = new Color(0.8771f, 1f, 0.6761f, 0.1f);
    static readonly Color Button_PressedColor   = new Color(0.75f, 0.75f, 0.75f, 0.5f);
    static readonly Color Button_SelectedColor  = new Color(0.9f, 0.9f, 0.9f, 0.25f);
    static readonly Color Button_DisabledColor = new Color(0.75f, 0.75f, 0.75f, 0.5f);
    const float Button_ColorMultiplier = 1.0f;
    const float Button_ColorFadeDuration = 0.1f;

    static ColorBlock Button_Colors = new ColorBlock();

    static bool initialized = false;

    public static void Initialize()
    {
        if (initialized) return;

        initialized = true;

        // Colors
        Button_Colors.normalColor = Button_NormalColor;
        Button_Colors.highlightedColor = Button_HighlightColor;
        Button_Colors.pressedColor = Button_PressedColor;
        Button_Colors.selectedColor = Button_SelectedColor;
        Button_Colors.disabledColor = Button_DisabledColor;
        Button_Colors.colorMultiplier = Button_ColorMultiplier;
        Button_Colors.fadeDuration = Button_ColorFadeDuration;
    }

    public static void SetButtonColors (Button button)
    {
        button.transform.GetComponent<Image>().color = Button_ImageColor;
        button.colors = Button_Colors;
    }



}
