using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damagable : MonoBehaviour
{
    public Action Damage;

    public Action Heal;

    public Action Restore;

    [SerializeField] protected int maxHealth = 5;

    [SerializeField] protected int invincibilityTime = 10;

    protected bool invulnerable = false;

    protected int currentHealth = 5;

    protected virtual void Start()
    {
        currentHealth = maxHealth;
    }

    private void OnEnable()
    {
        Damage += OnTakeDamage;
        Heal += OnHeal;
        Restore += OnRestore;
    }

    private void OnDisable()
    {
        Damage -= OnTakeDamage;
        Heal -= OnHeal;
        Restore -= OnRestore;
    }

    protected virtual void OnTakeDamage()
    {
        if (!invulnerable && currentHealth > 0)
        {
            currentHealth--;
        }
    }

    protected virtual void OnHeal()
    {
        if (currentHealth < maxHealth)
        {
            currentHealth++;
        }
    }

    protected virtual void OnRestore()
    {
        currentHealth = maxHealth;
    }
}
