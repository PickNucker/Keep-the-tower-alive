using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void Start()
    {
        Destroy(this, 5f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            IDamagable dmg = collision.transform.GetComponent<IDamagable>();
            dmg.ApplyDamage(PlayerShoot.instance.DamageOfRangeWeapon);         
        }
        Destroy(this.gameObject);
    }
}
