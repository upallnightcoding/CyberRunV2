using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetItemCntrl : MonoBehaviour
{
    [SerializeField] private GameData gameData;

    private float turn = 0.0f;
    private Vector3 direction = new Vector3(0.0f, 0.0f, -1.0f);
    private float speed = 5.0f;
    private TargetItemSO control;
    private float damage;

    public void Set(TargetItemSO targetItemSO)
    {
        this.control = targetItemSO;
        this.turn = targetItemSO.turn;
        this.speed = targetItemSO.speed;
        this.damage = targetItemSO.damage;
    }

    // Update is called once per frame
    void Update()
    {
        turn += gameData.pickItemRotate * Time.deltaTime;

        transform.rotation = Quaternion.Euler(0.0f, turn, 0.0f);

        transform.position += speed * direction * Time.deltaTime;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<DamageCntrl>(out DamageCntrl damageHealth))
        {
            damageHealth.TakeDamage(damage);
        }
    }
}
