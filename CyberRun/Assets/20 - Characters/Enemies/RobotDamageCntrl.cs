using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotDamageCntrl : DamageCntrl
{
    [SerializeField] private GameObject explosionPrefab;
    
    public override void Damage()
    {
        EventManager.Instance.InvokeOnUpdateXP(50);

        Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}
