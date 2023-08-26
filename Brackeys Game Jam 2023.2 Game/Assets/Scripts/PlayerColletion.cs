using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColletion : MonoBehaviour
{
    [SerializeField] private float invincibilityTime = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        invincibilityTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        invincibilityTime -= Time.deltaTime;
    }


    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == Layers.Enemis && invincibilityTime <= 0)
        {
            gameObject.GetComponent<HPComponent>().TakeDamage(10);
        }
    }
}
