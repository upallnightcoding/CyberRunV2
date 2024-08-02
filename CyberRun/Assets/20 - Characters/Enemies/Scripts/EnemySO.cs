using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "CyberRun/Enemy")]
public class EnemySO : ScriptableObject, IDamageCntrl
{
    public GameObject prefab;

    public GameObject killPrefab;

    public float health = 0.0f;

    public int xp = 0;

    public GameObject Create(GameObject parent)
    {
        GameObject enemy = Instantiate(prefab, parent.transform);
        enemy.GetComponent<DamageCntrl>().Set(this);

        Vector2 position = Random.insideUnitCircle * 7.0f;

        enemy.transform.localPosition = new Vector3(position.x, 0.0f, position.y);

        return (enemy);
    }

    public void Kill(Vector3 position)
    {
        EventManager.Instance.InvokeOnUpdateXP(xp);

        if (killPrefab)
        {
            GameObject fx = Instantiate(killPrefab, position, Quaternion.identity);

            Destroy(fx, 5.0f);
        }
    }

    public float GetHealth() => health;
}

public interface IDamageCntrl
{
    public float GetHealth();

    public void Kill(Vector3 position);
}
