using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentCntrl : MonoBehaviour
{
    [SerializeField] private GameObject platePrefab;
    [SerializeField] private GameObject environment;
    [SerializeField] private Transform player;

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
        Instantiate(platePrefab, new Vector3(0.0f, 0.0f, z), Quaternion.identity);
        z += offset;
    }
}
