using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class Lava : MonoBehaviour
{



    [SerializeField] private float cooldown = 5;
    [SerializeField] private float riseSpeed = 0.00001f;
    public static bool canRise = false;
    private float time = 0;
    // Start is called before the first frame update
    void Start()
    { 
        time = cooldown;
    }


    private void FixedUpdate()
    {
        if (!canRise)
        {
            return;
        }
        time -= Time.deltaTime;
        if (time < cooldown)
        {
            time = cooldown;
        }
        transform.position += new Vector3(0, riseSpeed * Time.deltaTime, 0);
    }

    public void lavaStart()
    {
        canRise = true;
    }

}
