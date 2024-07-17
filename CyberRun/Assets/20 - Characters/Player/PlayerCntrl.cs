using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCntrl : MonoBehaviour
{
    private Animator animator;

    private Vector3 direction;
    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();

        direction = new Vector3(1.0f, 0.0f, 1.0f);
        speed = 5.0f;

        animator.SetFloat("speed", 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(speed * direction * Time.deltaTime, Space.World);
    }

    private void MovePlayer(float movement)
    {
        Debug.Log($"MovePlayer: {movement}");
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
