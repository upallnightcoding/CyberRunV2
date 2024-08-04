using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretCntrl : MonoBehaviour
{
    [SerializeField] private Transform muzzlePoint;

    private ProjectileSO projectile;

    private float gap = 1.0f;

    /**
     * SetTurret() - 
     */
    public void SetTurret(ProjectileSO projectile)
    {
        this.projectile = projectile;

        StartCoroutine(Firer());
    }

    /**
     * Firer() - 
     */
    private IEnumerator Firer()
    {
        bool firing = true;

        while (firing)
        {
            float timer = 0.0f;

            while (timer <= gap)
            {
                timer += Time.deltaTime;
                yield return null;
            }

            projectile.Create(muzzlePoint.position, transform.forward, "Enemy");
            yield return null;
        }
    }
}
