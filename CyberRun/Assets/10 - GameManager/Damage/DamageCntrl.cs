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

        if (health <= 0.0f)
        {
            controls.Kill(gameObject.transform.position);

            Destroy(gameObject);
        }
    }
}

public interface IDamageCntrl
{
    public float GetHealth();

    public void Kill(Vector3 position);
}
