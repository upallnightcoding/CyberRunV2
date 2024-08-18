using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameData gameData;
    [SerializeField] private UiCntrl uiCntrl;

    [SerializeField] private GameLevel[] gameLevel;

    private GameLevel currentLevel;

    private long xp = 0;
    private float health = 100.0f;
    private float maxHealth = 100.0f;
    private int level = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    /**
     * NewRun() - 
     */
    public void StartNewRun()
    {
        currentLevel = gameLevel[level];

        EventManager.Instance.InvokeOnUpdateHealth(0.0f, 0.0f);

        StartNewLevel();
    }

    private void StartNewLevel()
    {
        StartCoroutine(StartNewLevelCountDown(currentLevel, false));
    }

    /**
     * OnWonLevel() - Event Message Callback
     */
    private void OnWonLevel()
    {

        if (++level < gameLevel.Length)
        {
            currentLevel = gameLevel[level];
        }

        StartCoroutine(StartNewLevelCountDown(currentLevel, true));
    }

    private void LoseLevel()
    {

    }

    private void StopRun()
    {

    }

    private void InitNewLevel()
    {
        xp = 0;
        uiCntrl.UpdateXP(xp);
    }

    /**
     * UpdateHealthRatio() - 
     */
    private void UpdateHealth(float deltaHealth, float deltaMaxHealth)
    {
        health -= deltaHealth;
        maxHealth += deltaMaxHealth;

        uiCntrl.UpdateHealth(health, maxHealth);
    }

    /**
     * StartLevelCountDown()-
     */
    private IEnumerator StartNewLevelCountDown(GameLevel gameLevel, bool completed)
    {
        if (completed)
        {
            uiCntrl.ShowLevelCompletePanel();

            yield return new WaitForSeconds(2.0f);

            uiCntrl.ShowNextLevelDisplayPanel();

            yield return new WaitForSeconds(2.0f);
        }

        float totalTime = 4.0f;
        float timer = totalTime;

        EventManager.Instance.InvokeOnInitNewLevel();

        uiCntrl.ShowLevelName(gameLevel);

        yield return null;

        while (timer >= 0.0f)
        {
            timer -= Time.deltaTime;
            uiCntrl.CountDownSlider(timer/totalTime);

            yield return null;
        }

        EventManager.Instance.InvokeOnStartGamePlay(gameLevel);
    }

    /**
     * UpdateXP() - 
     */
    public void UpdateXP(long value)
    {
        xp += value;

        uiCntrl.UpdateXP(xp);

        if (xp >= currentLevel.xp)
        {
            EventManager.Instance.InvokeOnWonLevel();
        }
    }

   

    private void OnEnable()
    {
        EventManager.Instance.OnUpdateXP += UpdateXP;
        EventManager.Instance.OnWonLevel += OnWonLevel;
        EventManager.Instance.OnInitNewLevel += InitNewLevel;
        EventManager.Instance.OnUpdateHealth += UpdateHealth;
    }

    private void OnDisable()
    {
        EventManager.Instance.OnUpdateXP -= UpdateXP;
        EventManager.Instance.OnWonLevel -= OnWonLevel;
        EventManager.Instance.OnInitNewLevel -= InitNewLevel;
        EventManager.Instance.OnUpdateHealth -= UpdateHealth;
    }
}

[System.Serializable]
public class GameLevel
{
    public int level;
    public string name;
    public int xp;
    public int nTargetItems;
}
