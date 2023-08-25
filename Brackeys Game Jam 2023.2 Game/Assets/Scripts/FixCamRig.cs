using System;
using System.Collections; 
using System.Collections.Generic;
using UnityEngine;

public class FixCamRig : MonoBehaviour
{
    [SerializeField]private GameObject Player;
    [SerializeField]private float SmoothTime = 0.3f;

    private Vector3 Velocity = Vector3.zero;
    // Update is called once per frame

    private void Start()
    {
        Player = FindObjectOfType<PlayerCotrol>().gameObject;
    }

    void Update()
    {
        if (Player)
        {
            // Player is not destroyed
            transform.position = Vector3.SmoothDamp(transform.position,Player.transform.position,ref Velocity,SmoothTime);

        }
    }
}
