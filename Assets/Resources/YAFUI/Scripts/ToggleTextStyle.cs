using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class ToggleTextStyle : MonoBehaviour
{

    private Text thisText;

    [Header("True Text Style")]
    public FontData trueFont;
    public Color trueColor;

    [Header("False Text Style")]
    public FontData falseFont;
    public Color falseColor;


    // Use this for initialization
    void Start ()
    {
        thisText = GetComponent<Text>();
        Toggle parentToggle = GetComponentInParent<Toggle>();

        if (parentToggle == null)
        {
            Debug.LogError("No Parent Toggle for This GameObject");
            return;
        }

        parentToggle.onValueChanged.AddListener(ChangeStyle);
        ChangeStyle(parentToggle.isOn);

    }

    void ChangeStyle(bool value)
    {
        if (value)
        {
            thisText.font = trueFont.font;
            thisText.fontStyle = trueFont.fontStyle;
            thisText.fontSize = trueFont.fontSize;
            thisText.lineSpacing = trueFont.lineSpacing;
            thisText.supportRichText = trueFont.richText;
            thisText.alignment = trueFont.alignment;
            thisText.alignByGeometry = trueFont.alignByGeometry;
            thisText.horizontalOverflow = trueFont.horizontalOverflow;
            thisText.verticalOverflow = trueFont.verticalOverflow;
            thisText.resizeTextForBestFit = trueFont.bestFit;
            thisText.resizeTextMaxSize = trueFont.maxSize;
            thisText.resizeTextMinSize = trueFont.minSize;
            thisText.color = trueColor;
        }
        else
        {
            thisText.font = falseFont.font;
            thisText.fontStyle = falseFont.fontStyle;
            thisText.fontSize = falseFont.fontSize;
            thisText.lineSpacing = falseFont.lineSpacing;
            thisText.supportRichText = falseFont.richText;
            thisText.alignment = falseFont.alignment;
            thisText.alignByGeometry = falseFont.alignByGeometry;
            thisText.horizontalOverflow = falseFont.horizontalOverflow;
            thisText.verticalOverflow = falseFont.verticalOverflow;
            thisText.resizeTextForBestFit = falseFont.bestFit;
            thisText.resizeTextMaxSize = falseFont.maxSize;
            thisText.resizeTextMinSize = falseFont.minSize;
            thisText.color = falseColor;
        }
    }
}
