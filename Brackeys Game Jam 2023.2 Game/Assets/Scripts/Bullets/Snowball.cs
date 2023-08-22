using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snowball : MonoBehaviour
{
    
    [SerializeField] private float velocty = 30;
    [SerializeField] private float livingTime = 5;
    [SerializeField] public int damage = 1;
    
    
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject,livingTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * (velocty * Time.deltaTime);
    }
    
    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("add collision");
    }
    
}
