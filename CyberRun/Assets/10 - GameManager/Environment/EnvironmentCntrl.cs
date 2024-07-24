using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentCntrl : MonoBehaviour
{
    [SerializeField] private GameData gameData;
    [SerializeField] private GameObject environment;
    [SerializeField] private Transform player;

    [SerializeField] private GameObject[] pickupItems;
    [SerializeField] private GameObject plateFw;
    [SerializeField] private GameObject wallFw;

    [SerializeField] private GameObject[] wallPrefab;

    [SerializeField] private GameObject robot;

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

    private void xxxCreatePlate()
    {
        GameObject plate = 
            Instantiate(plateFw, new Vector3(0.0f, 0.0f, z), Quaternion.identity);

        PlacePickupItems(plate);

        z += offset;
    }

    private void CreatePlate()
    {
        Framework framework = new();

        GameObject plate = framework
            .Blueprint(plateFw)
            .Apply(CreateWall(), "Anchor00", 90.0f)
            .Apply(CreateWall(), "Anchor01", 90.0f)
            .Apply(CreateWall(), "Anchor02", 90.0f)
            .Apply(CreateWall(), "Anchor03", 90.0f)
            .Apply(CreateWall(), "Anchor04", 90.0f)
            .Apply(CreateWall(), "Anchor05", 90.0f)
            .Apply(CreateWall(), "Anchor06", 90.0f)
            .Apply(CreateWall(), "Anchor07", -90.0f)
            .Apply(CreateWall(), "Anchor08", -90.0f)
            .Apply(CreateWall(), "Anchor09", -90.0f)
            .Apply(CreateWall(), "Anchor10", -90.0f)
            .Apply(CreateWall(), "Anchor11", -90.0f)
            .Apply(CreateWall(), "Anchor12", -90.0f)
            .Apply(CreateWall(), "Anchor13", -90.0f)
            .Position(new Vector3(0.0f, 0.0f, z))
            .Build();

        PlacePickupItems(plate);
        CreateEnemy(plate);

        z += offset;
    }

    private GameObject CreateWall()
    {
        Framework framework = new();

        GameObject go = framework
            .Blueprint(wallFw)
            .Assemble(wallPrefab, "base")
            .Build();

        return (go);
    }

    private void CreateEnemy(GameObject parent)
    {
        GameObject enemy = Instantiate(robot, parent.transform);

        enemy.transform.localPosition = Vector3.zero;
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
