using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCntrl : MonoBehaviour
{
    [SerializeField] private GameData gameData;
    [SerializeField] private Transform gunPoint;
    [SerializeField] private GunSO gunSO;
    [SerializeField] private ProjectileSO projectileSO;

    private Animator animator;

    private Vector3 direction;
    private float speed;

    private GameObject gun = null;

    private GameObject currentShield = null;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();

        direction = new Vector3(0.0f, 0.0f, 1.0f);
        speed = gameData.playerSpeed;

        animator.SetFloat("speed", 1.0f);

        gun = Instantiate(gunSO.prefab, gunPoint.position, Quaternion.identity);
        gun.transform.SetParent(gunPoint.transform);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(speed * direction * Time.deltaTime, Space.World);

        Quaternion rotTarget = Quaternion.LookRotation(direction);
        this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, rotTarget, 500.0f * Time.deltaTime);
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
            //currentShield.transform.position = gameObject.transform.position;
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
    }

    private void OnDisable()
    {
        EventManager.Instance.OnSliderMovement -= MovePlayer;
    }
}
