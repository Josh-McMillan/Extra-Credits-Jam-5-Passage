using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Damagable
{
    [SerializeField] private Color[] jarColors;

    [SerializeField] private MeshRenderer jarGlassRenderer;

    protected override void Start()
    {
        base.Start();
        UpdateHealth();
    }

    protected override void OnTakeDamage()
    {
        if (!invulnerable && currentHealth > 0)
        {
            Debug.Log("Player has taken damage!");

            currentHealth--;
            UpdateHealth();
            StartCoroutine(RunInvincibilityFrames());
        }
    }

    protected override void OnHeal()
    {
        if (currentHealth < maxHealth)
        {
            currentHealth++;
            UpdateHealth();
        }
    }

    protected override void OnRestore()
    {
        currentHealth = maxHealth;
        UpdateHealth();
    }

    private void UpdateHealth()
    {
        if (currentHealth > 0)
        {
            jarGlassRenderer.material.color = jarColors[currentHealth - 1];
        }
    }

    IEnumerator RunInvincibilityFrames()
    {
        invulnerable = true;

        for (int i = 0; i < invincibilityTime; i++)
        {
            jarGlassRenderer.enabled = false;
            yield return new WaitForSeconds(0.1f);

            jarGlassRenderer.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }

        invulnerable = false;
    }
}
