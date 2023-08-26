using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPComponent : MonoBehaviour
{
    [SerializeField] private int HP = 10;
    [SerializeField] private int maxHP = 100;
    private bool isActive = true;


    public bool IsActive
    {
        get => isActive;
        set => isActive = value;
    }

    public int Hp
    {
        get => HP;
        set => HP = value;
    }


    public void TakeDamage(int Damage)
    {
        if (IsActive)
        {
            HP -= Damage;
            if (HP <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
    
    public void Heal(int HealAmount)
    {
        if (IsActive)
        {
            if (HealAmount + HP < maxHP + 1)
            {
                HP += HealAmount;
            }
            else
            {
                Hp = maxHP;
            }


        }
    }
    
}
