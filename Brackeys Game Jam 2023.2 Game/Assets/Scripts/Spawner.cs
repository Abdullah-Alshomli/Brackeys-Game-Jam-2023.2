using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{

    [SerializeField] private GameObject enemy;

    [SerializeField] private float cooldown = 5;
    private float time;

    private int maxNuberOfEnemis = 10;
    // Start is called before the first frame update
    private void Start()
    {
        time = cooldown;
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        if (time <= 0)
        {

            PathFinding[] listOfEnemys = FindObjectsOfType<PathFinding>();
            if (listOfEnemys.Length <= 20)
            {
                int x = Random.Range(1, 11);
                Debug.Log(x);
                if (x > 5)
                {
                    Instantiate(enemy,transform.position,transform.rotation); 
                }
            }
            time = cooldown;
        }
    }
}
