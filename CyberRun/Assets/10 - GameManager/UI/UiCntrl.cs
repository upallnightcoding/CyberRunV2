using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiCntrl : MonoBehaviour
{
    [SerializeField] private Slider posSlider;
    [SerializeField] private TMP_Text xp;
    [SerializeField] private Slider healthSlider;

    // Start is called before the first frame update
    void Start()
    {
        healthSlider.value = 1.0f;
    }

    public void UpdateXP(long value)
    {
        xp.text = value.ToString();
    }

    public void PositionChange()
    {
        EventManager.Instance.InvokeOnSliderMovement(posSlider.value);
    }
}
