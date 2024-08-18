using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCntrl : MonoBehaviour
{
    private float health = 0.0f;

    private IDamageCntrl controls = null;

    private bool reacting = false;

    // Update is called once per frame
    public void Set(IDamageCntrl controls)
    {
        this.controls = controls;

        this.health = controls.GetHealth();
    }

    public void TakeDamage(float damage)
    {
        if (damage > 0.0f)
        {
            health -= damage;

            controls.UpdateHealth(damage);

            if (health <= 0.0f)
            {
                controls.Kill(gameObject);
            }
            else
            {
                DamageReaction();
            }
        }
    }

    private void DamageReaction()
    {
        Material reAction = controls.GetDamageReaction();

        if ((reAction != null) && (!reacting))
        {
            StartCoroutine(DamageReact(reAction));
        }
    }

    private IEnumerator DamageReact(Material material)
    {
        reacting = true;

        SkinnedMeshRenderer renderer = gameObject.GetComponentInChildren<SkinnedMeshRenderer>();
        Material current = renderer.material;
        renderer.material = material;

        yield return new WaitForSeconds(0.1f);

        renderer.material = current;
        reacting = false;
    }
}

public interface IDamageCntrl
{
    public float GetHealth();

    public void Kill(GameObject gameObject);

    public Material GetDamageReaction();

    public void UpdateHealth(float damage);
}
