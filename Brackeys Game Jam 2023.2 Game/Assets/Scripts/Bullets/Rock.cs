using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    
    [SerializeField] private float velocty = 10;
    [SerializeField] private float livingTime = 5;
    [SerializeField] public int damage = 5;
    
    
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject,livingTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * (velocty * Time.deltaTime);
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == Layers.Player)
        {
            other.gameObject.GetComponent<HPComponent>().TakeDamage(damage);
            Destroy(gameObject);
        }

    }
    
}
