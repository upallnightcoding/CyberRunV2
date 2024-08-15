using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCntrl : MonoBehaviour
{
    private float health = 0.0f;

    private IDamageCntrl controls = null;

    // Update is called once per frame
    public void Set(IDamageCntrl controls)
    {
        this.controls = controls;

        this.health = controls.GetHealth();
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        Debug.Log($"Taking Damage ... {damage}/{health}");

        if (health <= 0.0f)
        {
            controls.Kill(gameObject);

            //Destroy(gameObject);
        } else
        {
            EventManager.Instance.InvokeOnUpdateHealth(damage, 0.0f);
        }
    }
}

public interface IDamageCntrl
{
    public float GetHealth();

    public void Kill(GameObject gameObject);
}
