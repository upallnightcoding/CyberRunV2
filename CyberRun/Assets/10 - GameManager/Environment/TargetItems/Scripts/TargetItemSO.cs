using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TargetItem", menuName = "CyberRun/Target")]
public class TargetItemSO : ScriptableObject
{
    public GameObject prefab;

    public float health = 0.0f;

    public float turn = 10.0f;

    public GameObject Create(Transform parent)
    {
        GameObject target = Instantiate(prefab, parent);
        target.GetComponent<TargetItemCntrl>().SetTarget(turn);
        target.GetComponent<DamageCntrl>().SetHealth(health);

        return (target);
    }
}
