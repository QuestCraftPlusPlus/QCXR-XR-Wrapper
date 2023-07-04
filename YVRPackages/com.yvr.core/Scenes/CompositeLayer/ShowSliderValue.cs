using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowSliderValue : MonoBehaviour
{
    public Slider slider;
    public Text text;

    // Update is called once per frame
    void Update()
    {
        text.text = slider.value.ToString();
    }
}
