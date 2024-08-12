using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentCntrl : MonoBehaviour
{
    [SerializeField] private GameData gameData;

    [SerializeField] private GameObject environment;
    [SerializeField] private GameObject player;

    [SerializeField] private GameObject plateFw;
    [SerializeField] private GameObject platformfw;
    [SerializeField] private GameObject wallFw;

    [SerializeField] private EnemySO enemy;

    [SerializeField] private TargetItemSO[] targetItems;
    [SerializeField] private GameObject[] wallPrefab;
    [SerializeField] private PickupItemSO[] pickupItems;
    [SerializeField] private TurretSO[] turretPrefab;
    [SerializeField] private GameObject[] shieldPrefab;
    [SerializeField] private GameObject[] papersPrefab;

    private float offset = 17.5f;
    private float z = 0.0f;
    private float diff;
    private int nPlates = 7;

    private int currentLevel = 0;

    private Queue<GameObject> plateQueue = new();

    private bool startRunning = false;

    public void Start()
    {
        diff = offset * (nPlates - 1);
        z = -offset * 2.0f;

        StartNewLevel();
    }

    public void StartNewLevel()
    {
        CreatePlatform();
    }

    // Start is called before the first frame update
    public void NewRun(GameLevel gameLevel)
    {
        InitializeEnvironment(gameLevel);

        player.SetActive(true);

        startRunning = true;
    }

    private void InitNewLevel()
    {
        startRunning = false;

        StartNewLevel();
    }

    // Update is called once per frame
    void Update()
    {
        if (startRunning)
        {
            if (player.transform.position.z + diff > z)
            {
                plateQueue.Enqueue(CreatePlate());

                Destroy(plateQueue.Dequeue());
            }
        }
    }

    private void InitializeEnvironment(GameLevel gameLevel)
    {
        for (int i = 0; i < nPlates; i++)
        {
            plateQueue.Enqueue(CreatePlate());
        }
    }

    private GameObject CreatePlate()
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
            .Decorate(15, papersPrefab, 7.5f)
            .Position(new Vector3(0.0f, 0.0f, z))
            .Build();

        PlaceTargetItem(5, plate);
        //PlaceEnemy(plate);
        //PlacePickupItem(plate);
        //PlaceTurret(plate);
        //PlaceShieldItem(plate);

        z += offset;

        return (plate);
    }

    /**
     * CreatePlatform() - 
     */
    private GameObject CreatePlatform()
    {
        Framework framework = new();

        GameObject plate = framework
            .Blueprint(platformfw)
            .Decorate(15, papersPrefab, 7.5f)
            .Position(new Vector3(0.0f, 0.0f, 0.0f))
            .Build();

        return (plate);
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

    /**
     * CreateEnemy - 
     */
    private void PlaceEnemy(GameObject parent)
    {
        for (int i = 0; i < 4; i++)
        {
            enemy.Create(parent);
        }
    }

    private void PlaceTurret(GameObject parent)
    {
        int choice = Random.Range(0, turretPrefab.Length);
        GameObject turret = turretPrefab[choice].Create(parent);

        bool leftRight = Random.Range(0, 2) == 0;

        float x = (leftRight) ? 7.5f : -7.5f;
        float y = 0.0f;
        float z = (Random.insideUnitCircle * 7.0f).y;
        turret.transform.localPosition = new Vector3(x, y, z);

        float turn = (leftRight) ? 270.0f : 90.0f;
        turret.transform.localRotation = Quaternion.Euler(0.0f, turn, 0.0f);
    }

    private void PlacePickupItem(GameObject parent)
    {
        int choice = Random.Range(0, pickupItems.Length);

        pickupItems[choice].Create(parent);
    }

    private void PlaceShieldItem(GameObject parent)
    {
        int choice = Random.Range(0, shieldPrefab.Length);

        GameObject shield = Instantiate(shieldPrefab[choice], parent.transform);

        Vector2 position = Random.insideUnitCircle * 7.0f;

        shield.transform.localPosition = new Vector3(position.x, 0.0f, position.y);
    }

    private void PlaceTargetItem(int n, GameObject parent)
    {
        for (int i = 0; i < n; i++)
        {
            int choice = Random.Range(0, targetItems.Length);

            GameObject target = targetItems[choice].Create(parent.transform);
        }
    }

    private void OnEnable()
    {
        EventManager.Instance.OnStartGamePlay += NewRun;
        EventManager.Instance.OnInitNewLevel += InitNewLevel;
    }

    private void OnDisable()
    {
        EventManager.Instance.OnStartGamePlay -= NewRun;
        EventManager.Instance.OnInitNewLevel -= InitNewLevel;
    }
}


