using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class player_damage : MonoBehaviour {
	
	public GameObject skull;
	public Text Kills_Count;
	
	public int health = 10;
	int kill_count = 0;
	int damage_barrel = 11;
	int damage_terrain = 6;

	public Level level; 
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//skull.GetComponent<Transform>().Rotation.y += gameObject.rotation.y;
	}
	
	void OnCollisionEnter (Collision col)
	{
		if (col.gameObject.name == "Terrain") {
			if (health >= 0)
			{
				health -= damage_terrain;
			}
			else{
				Destroy (gameObject);
				Instantiate(skull,new Vector3(transform.position.x,transform.position.y,0.0f),new Quaternion(transform.rotation.x,transform.rotation.y-90,transform.rotation.y-90,transform.rotation.w));
			}
		}
		
		if (col.gameObject.name == "barrel(Clone)") {
			if (health >= 0)
			{
				health -= damage_barrel;
			}
			else
			{
				Destroy (gameObject);
				Instantiate(skull,new Vector3(transform.position.x,transform.position.y,0.3f),new Quaternion(transform.rotation.x,transform.rotation.y-90,transform.rotation.y-90,transform.rotation.w));

				level.death += 1;
				Kills_Count.text = level.death.ToString() + "/" + level.enemies;

				if (level.death >= level.enemies)
					{
						//Time.timeScale = 0;
						SceneManager.LoadScene("lighting_test");
					}
			}
		}
	}
}
