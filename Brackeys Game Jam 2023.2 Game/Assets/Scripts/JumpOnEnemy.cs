using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpOnEnemy : MonoBehaviour
{
    [SerializeField] private float jumpOnEnemyForce = 5; 
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == Layers.Enemis)
        {
            Destroy(other.gameObject);
            Rigidbody rb = this.transform.root.gameObject.GetComponent<Rigidbody>();
            rb.AddForce(Vector3.up * jumpOnEnemyForce , mode: ForceMode.Impulse);
        }
    }
}
