using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // every tag  = obstical will get a layer - 1 from player layer
        if (collision.tag == "Door")
        {
            Obsticle o = collision.GetComponent<Obsticle>();

            o.DoorUse();
        }
    }
}
