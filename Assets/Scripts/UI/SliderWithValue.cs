using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[ExecuteInEditMode]
public class SliderWithValue : MonoBehaviour
{

    public Slider slider;
    public Text text;
    public string feature;
    public string unit;
    public float decimals;
    public bool quantita;


    void OnEnable()
    {
        slider.onValueChanged.AddListener(ChangeValue);
        ChangeValue(slider.value);
    }
    void OnDisable()
    {
        slider.onValueChanged.RemoveAllListeners();
    }

    void ChangeValue(float value)
    {
        if(quantita) {
            text.text = value.ToString(feature + ": " + decimals) + " " + unit;
        } else
        {
            text.text = value.ToString(feature + ": #." + decimals) + " " + unit;
        }
    }


}