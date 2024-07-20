using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "CyberRun/Projectile")]
public class ProjectileSO : ScriptableObject
{
    public string projectileName;

    public GameObject prefab;

    public float damage;
}
