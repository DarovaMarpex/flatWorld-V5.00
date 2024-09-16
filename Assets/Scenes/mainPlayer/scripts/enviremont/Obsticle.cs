using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Obsticle : MonoBehaviour, IComparable<Obsticle>
{
    public SpriteRenderer MySpriteRenderer { get; set; }

    private Color defaultColor;
    private Color fadedColour;
    private Transform CurrentPos;
    private Player PlayerCurrentPos;
    public Enemy EnemyStatus;
//	public String RoomName;

    public int CompareTo(Obsticle other)
    {
        if (MySpriteRenderer.sortingOrder > other.MySpriteRenderer.sortingOrder)
        {
            return 1;
        }
        else if (MySpriteRenderer.sortingOrder < other.MySpriteRenderer.sortingOrder)
        {
            return -1;
        }
            return 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        MySpriteRenderer = GetComponent<SpriteRenderer>();
        CurrentPos = GameObject.FindGameObjectWithTag("Player").transform;
        PlayerCurrentPos = CurrentPos.GetComponent<Player>();
        //Debug.Log(PlayerCurrentPos);
        defaultColor = MySpriteRenderer.color;
        fadedColour = defaultColor;
        fadedColour.a = 0.4f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void FadeOut()
    {
        MySpriteRenderer.color = fadedColour;
    }
    
    public void FadeIn()
    {
        MySpriteRenderer.color = defaultColor;
    }
    public void DoorUse()
    {
		//SceneManager.LoadScene(RoomName);
           // PlayerCurrentPos.transform.position = new Vector2(PlayerCurrentPos.transform.position.x, PlayerCurrentPos.transform.position.y + 80f);
           // Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y + 80f, -10);
    }


}
