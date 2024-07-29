using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCntrl : MonoBehaviour
{
    private float totalDamage = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<DamageCntrl>(out DamageCntrl damageHealth))
        {
            damageHealth.TakeDamage(totalDamage);
        }

        if (collision.gameObject.TryGetComponent<PlayerDamageCntrl>(out PlayerDamageCntrl playerDamage))
        {
            playerDamage.TakeDamage(totalDamage);
        }

        Destroy(gameObject);
    }
}
