using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderBehaviour : MonoBehaviour
{
    private Slider _slider;

    public float Progress
    {
        get { return _slider.value; }
        set
        {
            _slider.value = value;
        }

    }
    public void Start()
    {
        _slider = GetComponent<Slider>();
    }
}