using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentCntrl : MonoBehaviour
{
    [SerializeField] private GameData gameData;
    [SerializeField] private GameObject platePrefab;
    [SerializeField] private GameObject environment;
    [SerializeField] private Transform player;

    [SerializeField] private GameObject[] pickupItems;

    private float offset = 17.5f;
    private float z = 0.0f;
    private float diff;

    // Start is called before the first frame update
    void Start()
    {
        diff = offset * 5;

        for (int i = 0; i < 5; i++)
        {
            CreatePlate();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.z + diff > z)
        {
            CreatePlate();
        }
    }

    private void CreatePlate()
    {
        GameObject plate = 
            Instantiate(platePrefab, new Vector3(0.0f, 0.0f, z), Quaternion.identity);

        PlacePickupItems(plate);

        z += offset;
    }

    private void PlacePickupItems(GameObject parent)
    {
        int n = 5;

        for (int i = 0; i < n; i++)
        {
            GameObject coin = Instantiate(pickupItems[0], parent.transform);

            Vector2 position = Random.insideUnitCircle * 7.0f;

            coin.transform.localPosition = new Vector3(position.x, gameData.pickItemHeight, position.y);
        }
    }
}
