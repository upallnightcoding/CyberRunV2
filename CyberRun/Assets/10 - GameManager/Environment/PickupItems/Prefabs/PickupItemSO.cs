using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PickupItem", menuName = "CyberRun/Pickup Item")]
public class PickupItemSO : ScriptableObject
{
    public GameData gameData;

    public GameObject prefab;

    public Vector3 camerPos;

    public void Create(GameObject parent)
    {
        GameObject pickup = Instantiate(prefab, parent.transform);
        pickup.GetComponent<MiniCameraPosCntrl>().CameraPos = camerPos;
        Debug.Log($"Camer Position: {camerPos}");

        Vector2 position = Random.insideUnitCircle * 7.0f;

        pickup.transform.localPosition = new Vector3(position.x, gameData.pickItemHeight, position.y);
    }
}
