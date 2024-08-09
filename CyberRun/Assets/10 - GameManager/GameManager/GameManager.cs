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
        uiCntrl.showNextLevelPanel();
        uiCntrl.UpdateHealthRatio(100.0f, 100.0f);

        currentLevel = gameLevel[level];

        StartCoroutine(LevelCountDown(currentLevel));
    }

    /**
     * LevelCountDown()-
     */
    private IEnumerator LevelCountDown(GameLevel gameLevel)
    {
        uiCntrl.showLevel(level);

        float totalTime = 4.0f;
        float timer = totalTime;

        yield return null;

        while (timer >= 0.0f)
        {
            timer -= Time.deltaTime;
            uiCntrl.CountDownSlider(timer/totalTime);

            yield return null;
        }

        uiCntrl.showGamePlayPanel();

        EventManager.Instance.InvokeOnNewRun(gameLevel);
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
        uiCntrl.showLevelCompletePanel();

        currentLevel = gameLevel[++level];

        StartCoroutine(LevelCountDown(currentLevel));
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
