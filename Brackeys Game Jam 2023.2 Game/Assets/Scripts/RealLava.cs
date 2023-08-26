using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealLava : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == Layers.Player)
        {
            other.gameObject.GetComponent<HPComponent>().TakeDamage(30);
        }
    }
}
