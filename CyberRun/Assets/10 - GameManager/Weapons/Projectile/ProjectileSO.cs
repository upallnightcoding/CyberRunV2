using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "CyberRun/Projectile")]
public class ProjectileSO : ScriptableObject
{
    public string projectileName;

    public GameObject prefab;

    public float damage;

    public float speed = 300.0f;

    public float destroyTiming = 3.0f;

    public void Create(Vector3 muzzlePoint, Vector3 direction, string ignore = null)
    {
        GameObject projectile = Instantiate(prefab, muzzlePoint, Quaternion.identity);
        projectile.GetComponentInChildren<Rigidbody>().AddForce(direction * speed, ForceMode.Impulse);
        projectile.GetComponent<ProjectileCntrl>().Set(ignore);

        Destroy(projectile, destroyTiming);
    }
}
