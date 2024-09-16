using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerSorter : MonoBehaviour
{
    private SpriteRenderer parentRenderer;

    private List<Obsticle> obsticles = new List<Obsticle>();

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(Camera.main.transform.position.y);
        parentRenderer = transform.parent.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Door")
        {
          //  Debug.Log("Works");
            Obsticle o = collision.GetComponent<Obsticle>();
            o.DoorUse();
            // obsticles.Add(o);
        }
        // every tag  = obstical will get a layer - 1 from player layer
        if (collision.tag == "Obsticle")
        {
            Obsticle o = collision.GetComponent<Obsticle>();

            o.FadeOut();
           // o.DoorUse();
            //Debug.Log(Camera.main.transform.position.y);

            if (obsticles.Count == 0 || o.MySpriteRenderer.sortingOrder - 1 < parentRenderer.sortingOrder)
            {
                  parentRenderer.sortingOrder = o.MySpriteRenderer.sortingOrder - 1;
            }
            obsticles.Add(o);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Door")
        {
            Obsticle o = collision.GetComponent<Obsticle>();
            //obsticles.Remove(o);
        }
        if (collision.tag == "Obsticle") 
        {
            Obsticle o = collision.GetComponent<Obsticle>();
            o.FadeIn();
            obsticles.Remove(o);

            if(obsticles.Count == 0)
            {
                parentRenderer.sortingOrder = 200;
            }
            else
            {
                obsticles.Sort();
                parentRenderer.sortingOrder = obsticles[0].MySpriteRenderer.sortingOrder - 1;
            }
           
        }
    }
}
