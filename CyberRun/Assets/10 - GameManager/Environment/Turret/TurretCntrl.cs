using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretCntrl : MonoBehaviour
{
    [SerializeField] private Transform muzzlePoint;

    private ProjectileSO projectile;

    private float gap = 1.0f;

    // Start is called before the first frame update
    public void SetTurret(ProjectileSO projectile)
    {
        this.projectile = projectile;

        StartCoroutine(Firer());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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

            GameObject shot = projectile.Create(muzzlePoint.position, transform.forward);
            Destroy(shot, 2.0f);
            yield return null;
        }
    }
}
