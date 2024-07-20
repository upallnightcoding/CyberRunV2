using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunCntrl : MonoBehaviour
{
    [SerializeField] private Transform muzzlePoint;
    [SerializeField] private ProjectileSO projectile;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fire()
    {
        GameObject go = Instantiate(projectile.prefab, muzzlePoint.position, Quaternion.identity);
        go.GetComponentInChildren<Rigidbody>().AddForce(transform.forward * 300.0f, ForceMode.Impulse);
        Destroy(go, 3.0f);
    }
}
