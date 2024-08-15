using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "CyberRun/Player")]
public class PlayerSO : ScriptableObject, IDamageCntrl
{
    public float health = 100.0f;

    public float GetHealth()
    {
        return (health);
    }

    public void Kill(GameObject gameObject)
    {
        Debug.Log("Player has just died ...");
    }
}
