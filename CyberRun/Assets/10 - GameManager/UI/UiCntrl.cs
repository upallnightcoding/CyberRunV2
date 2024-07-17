using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiCntrl : MonoBehaviour
{
    [SerializeField] private Slider posSlider;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PositionChange()
    {
        EventManager.Instance.InvokeOnSliderMovement(posSlider.value);
    }
}
