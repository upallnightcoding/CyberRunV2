using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCntrl : MonoBehaviour
{
    private float totalDamage = 10.0f;

    private string ignore = null;

    public void Set(string ignore) => this.ignore = ignore;

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<DamageCntrl>(out DamageCntrl damageHealth))
        {
            if ((ignore == null) || ((ignore != null) && (!collision.transform.gameObject.CompareTag(ignore))))
            {
                damageHealth.TakeDamage(totalDamage);
            }
        }

        Destroy(gameObject);
    }
}
