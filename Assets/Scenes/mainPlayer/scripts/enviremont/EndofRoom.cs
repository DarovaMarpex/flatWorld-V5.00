using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndofRoom : MonoBehaviour
{
	public string RoomName;
	public GameObject[] enemies;
	public bool dooruse = false;
	public int enemyCount;
	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		enemies = GameObject.FindGameObjectsWithTag("Enemy");
		enemyCount = enemies.Length;
		if (enemyCount < 1)
		{
			dooruse = true;
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player" && dooruse == true) { // and other condition = true
			SceneManager.LoadScene(RoomName);
			
		}
	}

}
