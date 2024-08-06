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
    [SerializeField] private TMP_Text healthRatio_Text;
    [SerializeField] private TMP_Text ammoCount_Text;
    [SerializeField] private GameObject currentGunPosition;

    private GameObject cameraGun = null;

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

    public void UpdateHealthRatio(float health, float maxHealth)
    {
        health_Slider.value = health / maxHealth;

        healthRatio_Text.text = ((int)health).ToString() + "/" + ((int)maxHealth).ToString();
    }

    public void UpdateChangeGun(GameObject pickup)
    {
        if (cameraGun != null) {
            Destroy(cameraGun);
        }

        cameraGun = Instantiate(pickup, Vector3.zero, Quaternion.identity);
        Vector3 position = pickup.GetComponent<MiniCameraPosCntrl>().CameraPos;
        cameraGun.transform.position = position;
    }

    public void PositionChange()
    {
        EventManager.Instance.InvokeOnSliderMovement(position_Slider.value);
    }

    private void OnEnable()
    {
        EventManager.Instance.OnUpdateHealth += UpdateHealthRatio;
        EventManager.Instance.OnChangeGun += UpdateChangeGun;
    }

    private void OnDisable()
    {
        EventManager.Instance.OnUpdateHealth -= UpdateHealthRatio;
        EventManager.Instance.OnChangeGun -= UpdateChangeGun;
    }
}
