using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCntrl : MonoBehaviour
{
    [SerializeField] private GameData gameData;
    [SerializeField] private PlayerSO playerSO;
    [SerializeField] private Transform gunPoint;
    [SerializeField] private GunSO gunSO;
    [SerializeField] private ProjectileSO projectileSO;

    private Animator animator;

    private Vector3 direction;
    private float speed = 0.0f;

    private GameObject gun = null;

    private GameObject currentShield = null;

    private bool isRunning = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        GetComponent<DamageCntrl>().Set(playerSO);

        direction = new Vector3(0.0f, 0.0f, 1.0f);

        gun = Instantiate(gunSO.prefab, gunPoint.position, Quaternion.identity);
        gun.transform.SetParent(gunPoint.transform);
    }

    private void NewRun(GameLevel gameLevel)
    {
        speed = gameData.playerSpeed;

        animator.SetFloat("speed", speed);
        animator.SetBool("startrunning", true);

        isRunning = true;
    }

    private void InitNewLevel()
    {
        isRunning = false;
        animator.SetFloat("speed", 0.0f);
        animator.SetBool("startrunning", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isRunning)
        {
            transform.Translate(speed * direction * Time.deltaTime, Space.World);
            Quaternion rotTarget = Quaternion.LookRotation(direction);
            this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, rotTarget, 500.0f * Time.deltaTime);
        }
    }

    public void Fire(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            gun.GetComponent<GunCntrl>().Fire();
        }
    }

    private void MovePlayer(float movement)
    {
        direction = new Vector3(movement, 0.0f, 1.0f).normalized;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.TryGetComponent<PickupItemCntrl>(out PickupItemCntrl pickup))
        {
            Debug.Log($"PlayerCntrl - OnTriggerEnter - PickupItemCntrl - {collision.gameObject}");
            EventManager.Instance.InvokeOnChangeGun(collision.gameObject);
        }

        if (collision.gameObject.TryGetComponent<ShieldCntrl>(out ShieldCntrl shield))
        {
            if (currentShield)
            {
                Destroy(currentShield);
            }
            
            currentShield = collision.gameObject.transform.parent.gameObject;
            currentShield.transform.parent = gameObject.transform;

            StartCoroutine(PositionShield(currentShield.transform.position, gameObject.transform.position));
        }
    }

    private IEnumerator PositionShield(Vector3 start, Vector3 end)
    {
        float timer = 0.0f;
        float duration = 1.0f;

        while (timer <= duration)
        {
            currentShield.transform.position = Vector3.Slerp(start, gameObject.transform.position, timer/duration);
            timer += Time.deltaTime;
            yield return null;
        }
    }

    private void OnEnable()
    {
        EventManager.Instance.OnSliderMovement += MovePlayer;
        EventManager.Instance.OnStartGamePlay += NewRun;
        EventManager.Instance.OnInitNewLevel += InitNewLevel;
    }

    private void OnDisable()
    {
        EventManager.Instance.OnSliderMovement -= MovePlayer;
        EventManager.Instance.OnStartGamePlay -= NewRun;
        EventManager.Instance.OnInitNewLevel -= InitNewLevel;
    }
}
