using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour, IDamagable
{
    public static PlayerHealth instance;

    [SerializeField] int health = 50;
    [SerializeField] Slider healthBar;

    int currentHealth;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        currentHealth = health;
        healthBar.maxValue = health;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.value = currentHealth;

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    public void ApplyDamage(float dmg)
    {
        currentHealth = Mathf.Max((int)(currentHealth - dmg), 0);
    }

    public void HealUp(int heal)
    {
        currentHealth = Mathf.Max(currentHealth + heal, health);
    }

    void Die()
    {
        // Win Screen
        PlayerMovement.instance.isDead = true;
        GameManagerHandler.instance.GameOver();
    }
}
