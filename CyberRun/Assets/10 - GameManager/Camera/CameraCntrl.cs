using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCntrl : MonoBehaviour
{
    [SerializeField] private Transform player;

    private Vector3 offset;
    private Vector3 velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 newPosition = player.position + offset;

        transform.position = Vector3.Slerp(transform.position, newPosition, 0.5f);
    }
}
