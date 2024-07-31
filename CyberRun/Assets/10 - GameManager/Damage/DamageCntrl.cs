using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCntrl : MonoBehaviour
{
    private float health = 0.0f;

    // Update is called once per frame
    public void SetHealth(float health)
    {
        this.health = health;
    }

    public virtual void Damage()
    {
        EventManager.Instance.InvokeOnUpdateXP(10);

        Destroy(gameObject);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0.0f)
        {
            Damage();
        }
    }
}
