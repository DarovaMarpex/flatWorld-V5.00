using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : Enemy
{
    [SerializeField]
    public GameObject bullet;
   
        public void Shoot()
        {
            Instantiate(bullet, transform.position, Quaternion.identity);

        }
}
