using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "CyberRun/Gun")]
public class GunSO : ScriptableObject
{
    public string gunName;

    public GameObject prefab;
}
