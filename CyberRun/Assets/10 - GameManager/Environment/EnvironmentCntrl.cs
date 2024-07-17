using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentCntrl : MonoBehaviour
{
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private GameObject environment;
    [SerializeField] private Transform player;

    private float offset = 7.5f;
    private float z = 0.0f;
    private float diff;

    // Start is called before the first frame update
    void Start()
    {
        diff = offset * 5;

        for (int i = 0; i < 5; i++)
        {
            CreateTile();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.z + diff > z)
        {
            CreateTile();
        }
    }

    private void CreateTile()
    {
        Instantiate(tilePrefab, new Vector3(0.0f, 0.0f, z), Quaternion.identity);
        z += offset;
    }
}
