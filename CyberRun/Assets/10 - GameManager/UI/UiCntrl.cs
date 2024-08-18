using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiCntrl : MonoBehaviour
{
    [SerializeField] private TMP_Text xp_Text;
    [SerializeField] private TMP_Text ammoCount_Text;
    [SerializeField] private GameObject currentGunPosition;

    [Header("Player Controls")]
    [SerializeField] private Slider position_Slider;

    [Header("Health Controls")]
    [SerializeField] private Slider health_Slider;
    [SerializeField] private TMP_Text healthRatio_Text;

    [Header("Level Count Down")]
    [SerializeField] private Slider countDownSlider;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private TMP_Text levelTextGamePlay;

    [Header("UI Panels")]
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject nextLevelPanel;
    [SerializeField] private GameObject gamePlayPanel;
    [SerializeField] private GameObject levelCompletePanel;
    [SerializeField] private GameObject nextLevelDisplayPanel;

    [Header("Game Timer")]
    [SerializeField] private TMP_Text gameTime;

    private GameObject cameraGun = null;

    // Start is called before the first frame update
    void Start()
    {
        health_Slider.value = 1.0f;
    }

    public void ShowLevelCompletePanel()
    {
        HideAllPanels();

        levelCompletePanel.SetActive(true);
    }

    public void ShowNextLevelDisplayPanel()
    {
        nextLevelDisplayPanel.SetActive(true);
    }

    /**
     * HideAllPanels() - 
     */
    private void HideAllPanels()
    {
        mainMenuPanel.SetActive(false);
        gamePlayPanel.SetActive(false);
        nextLevelPanel.SetActive(false);
        levelCompletePanel.SetActive(false);

        levelCompletePanel.SetActive(false);
        nextLevelDisplayPanel.SetActive(false);
    }

    /**
     * InitializeNewRun() - 
     */
    public void StartNewLevel()
    {
        HideAllPanels();

        nextLevelPanel.SetActive(true);
        gamePlayPanel.SetActive(true);
    }

    private void StartGamePlay(GameLevel gameLevel)
    {
        HideAllPanels();

        nextLevelPanel.SetActive(false);
        gamePlayPanel.SetActive(true);

        StartCoroutine(StartGameTimer(gameLevel));
    }

    private void InitNewLevel()
    {
        StartNewLevel();
    }

    public void ShowLevelName(GameLevel gameLevel)
    {
        levelText.text = gameLevel.name;
        levelTextGamePlay.text = gameLevel.name;
    }

    public void CountDownSlider(float value)
    {
        countDownSlider.value = value;
    }

    public void UpdateXP(long value)
    {
        xp_Text.text = value.ToString();
    }

    public void UpdateAmmoCount(int ammoCount, int maxAmmoCount)
    {
        ammoCount_Text.text = ammoCount.ToString() + "/" + maxAmmoCount.ToString();
    }

    public void UpdateHealth(float health, float maxHealth)
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

    /**
     * StartGameTimer()- 
     */
    private IEnumerator StartGameTimer(GameLevel gameLevel)
    {
        bool isRunning = true;
        int seconds = 0;
        int minutes = 0;

        while (isRunning)
        {
            gameTime.text = minutes.ToString("D2") + ":" + seconds.ToString("D2");

            yield return new WaitForSeconds(1.0f);

            if (seconds++ == 59)
            {
                seconds = 0;
                minutes++;
            }
        }

        yield return null;
    }

    private void OnEnable()
    {
        EventManager.Instance.OnChangeGun += UpdateChangeGun;
        EventManager.Instance.OnStartGamePlay += StartGamePlay;
        EventManager.Instance.OnInitNewLevel += InitNewLevel;
    }

    private void OnDisable()
    {
        EventManager.Instance.OnChangeGun -= UpdateChangeGun;
        EventManager.Instance.OnStartGamePlay -= StartGamePlay;
        EventManager.Instance.OnInitNewLevel -= InitNewLevel;
    }
}
