using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SliderLabel : MonoBehaviour
{
    public string text = "Value: ";
    public string unit = "pts";

    public int decimals = 2;
    

    private Text thisText;
    private Slider parentSlider;

	// Use this for initialization
	void Start ()
    {
        thisText = GetComponent<Text>();
        parentSlider = GetComponentInParent<Slider>();

        parentSlider.onValueChanged.AddListener(UpdateLabel);
        UpdateLabel(parentSlider.value);
    }
    
    void UpdateLabel(float value)
    {
        thisText.text = text + parentSlider.value.ToString("F" + decimals.ToString()) + unit;
    }
}
