using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetItemCntrl : MonoBehaviour
{
    [SerializeField] private GameData gameData;

    private float turn = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        turn += gameData.pickItemRotate * Time.deltaTime;

        transform.rotation = Quaternion.Euler(0.0f, turn, 0.0f);
    }
}
