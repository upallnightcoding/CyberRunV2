using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "CyberRun/Game Data")]
public class GameData : ScriptableObject
{
    [Header("Player Attributes")]
    public float playerSpeed = 4.0f;

    [Space]
    [Header("Environment Attributes")]
    public float pickItemHeight = 1.0f;
    public float pickItemRotate = 10.0f;
}
