using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private UiCntrl uiCntrl;

    private long xp = 0;

    // Start is called before the first frame update
    void Start()
    {
        UpdateXP(0);
    }

    public void NewGame()
    {
        EventManager.Instance.InvokeUpdateHealthRatio(100.0f, 100.0f);

        Debug.Log("New Game ...");
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
