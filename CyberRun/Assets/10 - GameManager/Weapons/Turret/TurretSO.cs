using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Turret", menuName = "CyberRun/Turret")]
public class TurretSO : ScriptableObject
{
    public GameObject prefab;

    public ProjectileSO projectile;
    
    public GameObject Create(GameObject parent)
    {
        GameObject turret = Instantiate(prefab, parent.transform);
        turret.GetComponent<TurretCntrl>().SetTurret(projectile);

        return (turret);
    }
}
