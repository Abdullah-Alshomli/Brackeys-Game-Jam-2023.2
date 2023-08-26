using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hookStart : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject lava;
    [SerializeField] private GameObject show;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == Layers.Player)
        {
            lava.GetComponent<Lava>().lavaStart();
            player.GetComponent<PlayerCotrol>().startHook();
            show.SetActive(true);
            Destroy(gameObject);
        }
    }
}
