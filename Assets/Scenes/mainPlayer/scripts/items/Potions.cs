using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potions : MonoBehaviour
{
    [SerializeField]
    private int potionIndex;

    [SerializeField]
    private int potionHealing;

    [SerializeField]
    private int potionDamage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player p = collision.GetComponentInParent<Player>();

        if (collision.tag == "Player")
        {
            if (potionIndex == 1)
            {
                p.health.myCurrentValue += potionHealing;
                Destroy(this.gameObject);
            }

            if (potionIndex == 2)
            {
                p.damage += potionDamage;
            }
        }
    }
}
