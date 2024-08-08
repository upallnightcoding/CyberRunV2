using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private UiCntrl uiCntrl;

    [SerializeField] private GameLevel[] gameLevel;

    private long xp = 0;
    private int level = 0;

    // Start is called before the first frame update
    void Start()
    {
        UpdateXP(0);
    }

    /**
     * NewRun() - 
     */
    public void NewRun()
    {
        uiCntrl.showNextLevelPanel();
        uiCntrl.UpdateHealthRatio(100.0f, 100.0f);

        StartCoroutine(LevelCountDown(level));

        Debug.Log("New Game ...");
    }

    /**
     * LevelCountDown()-
     */
    private IEnumerator LevelCountDown(int level)
    {
        uiCntrl.SetLevel(level);

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

        EventManager.Instance.InvokeOnNewRun(level);
    }

    public void UpdateXP(long value)
    {
        xp += value;

        uiCntrl.UpdateXP(xp);
    }

    private void OnEnable()
    {
        EventManager.Instance.OnUpdateXP += UpdateXP;
    }

    private void OnDisable()
    {
        EventManager.Instance.OnUpdateXP -= UpdateXP;
    }
}

[System.Serializable]
class GameLevel
{
    [SerializeField] private int xp;
    [SerializeField] private int nTargetItems;
}
