using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiCntrl : MonoBehaviour
{
    [SerializeField] private Slider position_Slider;
    [SerializeField] private TMP_Text xp_Text;
    [SerializeField] private Slider health_Slider;
    [SerializeField] private TMP_Text ammoCount_Text;

    // Start is called before the first frame update
    void Start()
    {
        health_Slider.value = 1.0f;
    }

    public void UpdateXP(long value)
    {
        xp_Text.text = value.ToString();
    }

    public void UpdateAmmoCount(int ammoCount, int maxAmmoCount)
    {
        ammoCount_Text.text = ammoCount.ToString() + "/" + maxAmmoCount.ToString();
    }

    public void PositionChange()
    {
        EventManager.Instance.InvokeOnSliderMovement(position_Slider.value);
    }
}
