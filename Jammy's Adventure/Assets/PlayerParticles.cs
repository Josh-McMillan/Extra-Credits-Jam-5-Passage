using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticles : MonoBehaviour
{
    public static Action Play;

    [SerializeField] private ParticleSystem glass;
    [SerializeField] private ParticleSystem jam;

    private void OnEnable()
    {
        Play += OnPlay;
    }

    private void OnDisable()
    {
        Play -= OnPlay;
    }

    public void OnPlay()
    {
        glass.Play();
        jam.Play();
    }
}
