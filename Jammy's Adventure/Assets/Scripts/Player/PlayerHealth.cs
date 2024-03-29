﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Damagable
{
    public static Action Died;

    [SerializeField] private Color[] jarColors;

    [SerializeField] private MeshRenderer jarGlassRenderer;

    [SerializeField] private MeshRenderer faceRenderer;

    protected override void Start()
    {
        base.Start();
        UpdateHealth();
    }

    // TODO: Troubleshoot Death Animation!

    protected override void OnTakeDamage()
    {
        if (!invulnerable && currentHealth > 0)
        {
            Debug.Log("Player has taken damage!");

            currentHealth--;
            UpdateHealth();

            if (currentHealth != 0)
            {
                PlayerParticles.Play();
                StartCoroutine(RunInvincibilityFrames());
            }
            else
            {
                Died();
            }
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

        faceRenderer.material.mainTextureOffset = new Vector2(0.125f * (float)currentHealth, 0.0f);
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
