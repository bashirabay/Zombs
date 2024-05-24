using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public float health, MaxHealth;
    [SerializeField]
    private HealthBarUI healthBar;

    void Start()
    {
        healthBar.SetMaxHealth(MaxHealth);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        health = Mathf.Clamp(health, 0, MaxHealth);
        healthBar.SetHealth(health);
    }

    public void AddHealth(float amount)
    {
        health += amount;
        health = Mathf.Clamp(health, 0, MaxHealth);
        healthBar.SetHealth(health);
    }
}
