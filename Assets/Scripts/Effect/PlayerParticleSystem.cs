﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticleSystem : MonoBehaviour
{
    public ParticleSystem myParticleSystem;
    ParticleSystem.EmissionModule myEmission;

    public void DoubleSlingshotEffect()
    {
        // trail renderer will not be turn on when timescale is not 1, means during double slingshot
        myEmission = myParticleSystem.emission;
        myEmission.enabled = true;
    }

    public void OffParticleSystem()
    {
        myEmission = myParticleSystem.emission;
        myEmission.enabled = false;
    }
    
}
