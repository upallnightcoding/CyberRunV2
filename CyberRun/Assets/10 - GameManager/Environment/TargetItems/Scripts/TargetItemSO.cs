using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TargetItem", menuName = "CyberRun/Target")]
public class TargetItemSO : ScriptableObject, IDamageCntrl
{
    public GameData gameData;

    public GameObject prefab;

    public GameObject createPrefab;

    public float health = 0.0f;

    public float turn = 10.0f;

    public int xp = 10;

    public float speed = 0.0f;

    public float damage = 0.0f;

    public GameObject Create(Transform parent)
    {
        Vector2 unitPosition = Random.insideUnitCircle * 7.0f;

        Vector3 position = new Vector3(unitPosition.x, gameData.pickItemHeight, unitPosition.y);

        GameObject target = Instantiate(prefab, parent);
        target.GetComponent<TargetItemCntrl>().Set(this);
        target.GetComponent<DamageCntrl>().Set(this);
        target.transform.localPosition = position;

        if (createPrefab != null)
        {
            GameObject fx = Instantiate(createPrefab, target.transform);
            fx.transform.localPosition = Vector3.zero;
        }

        return (target);
    }

    public float GetHealth() => health;

    public Material GetDamageReaction() => null;

    public void Kill(GameObject gameObject)
    {
        EventManager.Instance.InvokeOnUpdateXP(xp);

        Destroy(gameObject);
    }

    public void UpdateHealth(float damage)
    {
        
    }
}
