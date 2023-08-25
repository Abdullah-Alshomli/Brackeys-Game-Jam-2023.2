using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PathFinding : MonoBehaviour
{
    private GameObject Player;
    private NavMeshAgent NavMeshAgent;
    
    
    // Start is called before the first frame update
    void Start()
    {
        Player = FindObjectOfType<PlayerCotrol>().gameObject;
        NavMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Player)
        {
            NavMeshAgent.destination = Player.transform.position;
        }
    }
}
