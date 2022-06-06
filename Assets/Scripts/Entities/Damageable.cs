using UnityEngine;

public class Damageable : MonoBehaviour
{
    [SerializeField] int maxHealth;
    int currentHealth;

    void Start() {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage) {
        currentHealth -= damage;

        Debug.Log(currentHealth);
        if (currentHealth <= 0) {
            Die();
        }
    }

    void Die() {
        gameObject.SetActive(false);
    }
}
