using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Shooting : MonoBehaviour
{
    [SerializeField] private GameObject BulletPrefab;
    [SerializeField] private GameObject BulletSpawnPoint;

    public void Shoot()
    {
        Instantiate(BulletPrefab, BulletSpawnPoint.transform.position, BulletSpawnPoint.transform.rotation);
    }
}
