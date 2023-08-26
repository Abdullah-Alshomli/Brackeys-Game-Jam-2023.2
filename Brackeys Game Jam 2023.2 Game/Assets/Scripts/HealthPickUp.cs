using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : MonoBehaviour
{
    [SerializeField] private int heal = 30;
    private GameObject player;
    private void Start()
    {
        player = FindObjectOfType<PlayerCotrol>().gameObject;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == Layers.Player)
        {
            player.GetComponent<HPComponent>().Heal(heal);
            heal = 0;
            Destroy(gameObject);
        }
    }
}
