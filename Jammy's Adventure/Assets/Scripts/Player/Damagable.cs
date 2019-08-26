using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damagable : MonoBehaviour
{
    public Action Damage;

    public Action Heal;

    public Action Restore;

    public Action Kill;

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
        Kill += OnKill;
    }

    private void OnDisable()
    {
        Damage -= OnTakeDamage;
        Heal -= OnHeal;
        Restore -= OnRestore;
        Kill -= OnKill;
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

    protected virtual void OnKill()
    {
        currentHealth = 0;
    }

    protected virtual void OnRestore()
    {
        currentHealth = maxHealth;
    }
}
