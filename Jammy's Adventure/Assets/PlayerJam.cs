using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJam : MonoBehaviour
{
    [SerializeField] private Transform jam;

    public static Action<float> CollectJam;

    public static Action<float> LoseJam;

    private float jamAmount = 0.0f;

    private float JamAmount
    {
        get { return jamAmount; }
    }

    private void Start()
    {
        UpdateJam();
    }

    private void OnEnable()
    {
        CollectJam += OnCollectJam;
        LoseJam += OnLoseJam;
    }

    private void OnDisable()
    {
        CollectJam -= OnCollectJam;
        LoseJam -= OnLoseJam;
    }

    public void OnCollectJam(float amount)
    {
        jamAmount += amount;

        UpdateJam();
    }

    public void OnLoseJam(float amount)
    {
        jamAmount -= amount;

        UpdateJam();
    }

    private void UpdateJam()
    {
        jamAmount = Mathf.Clamp01(jamAmount);

        if (jamAmount < 0.001f)
        {
            jamAmount = 0.001f;
        }

        jam.localScale = new Vector3(1.0f, jamAmount, 1.0f);
    }
}
