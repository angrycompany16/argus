using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitbox : MonoBehaviour
{
    [SerializeField] int damage;

    void OnTriggerEnter2D(Collider2D other) {
        if (other.TryGetComponent(out Damageable damageable)) {
            damageable.TakeDamage(damage);
        }
    }
}
