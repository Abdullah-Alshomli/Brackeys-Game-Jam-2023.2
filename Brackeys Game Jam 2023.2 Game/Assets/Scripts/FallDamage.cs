using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDamage : MonoBehaviour
{
    public float fallDamageThreshold = 10f;
    public float fallDamageMultiplier = 3f;
    private HPComponent playerHP;
    private void Start()
    {
        playerHP = GetComponent<HPComponent>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.y > fallDamageThreshold && collision.gameObject.layer != Layers.Enemis)
        {
            float damage = (collision.relativeVelocity.y - fallDamageThreshold) * fallDamageMultiplier;
            playerHP.TakeDamage((int)damage);
            Debug.Log("Player took " + (int)damage + " damage from falling.");
        }
    }
}
