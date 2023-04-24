using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tower : MonoBehaviour, IDamagable
{
    public static Tower instance;

    [SerializeField] float maxHealth = 200;
    [SerializeField] Slider healthBar;

    public Transform hitPlace;

    float currentHealth;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.maxValue = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        currentHealth = Mathf.Max(currentHealth -= Time.deltaTime * 3f, 0);

        healthBar.value = currentHealth;

        if (currentHealth <= 0) Die();
    }

    public void AddHealth(float healthAmount)
    {
        float healthToAdd = healthAmount;

        if (healthAmount + currentHealth > maxHealth) currentHealth = maxHealth;
        else currentHealth += healthToAdd;
    }

    public void ApplyDamage(float dmg)
    {
        currentHealth = Mathf.Max(currentHealth - dmg, 0);
    }

    void Die()
    {
        GameManagerHandler.instance.GameOver();
    }
}
