using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private UiCntrl uiCntrl;

    [SerializeField] private GameLevel[] gameLevel;

    private GameLevel currentLevel;

    private long xp = 0;
    private int level = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    /**
     * NewRun() - 
     */
    public void NewRun()
    {
        currentLevel = gameLevel[level];

        uiCntrl.InitializeNewRun();

        StartCoroutine(StartLevelCountDown(currentLevel));
    }

    /**
     * StartLevelCountDown()-
     */
    private IEnumerator StartLevelCountDown(GameLevel gameLevel)
    {
        float totalTime = 4.0f;
        float timer = totalTime;

        uiCntrl.ShowGameLevel(gameLevel);

        yield return null;

        while (timer >= 0.0f)
        {
            timer -= Time.deltaTime;
            uiCntrl.CountDownSlider(timer/totalTime);

            yield return null;
        }

        EventManager.Instance.InvokeOnStartNewRun(gameLevel);
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

    private void OnWonLevel()
    {
        currentLevel = gameLevel[++level];

        uiCntrl.showLevelCompletePanel();

        StartCoroutine(StartLevelCountDown(currentLevel));
    }

    private void OnEnable()
    {
        EventManager.Instance.OnUpdateXP += UpdateXP;
        EventManager.Instance.OnWonLevel += OnWonLevel;
    }

    private void OnDisable()
    {
        EventManager.Instance.OnUpdateXP -= UpdateXP;
        EventManager.Instance.OnWonLevel -= OnWonLevel;
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
