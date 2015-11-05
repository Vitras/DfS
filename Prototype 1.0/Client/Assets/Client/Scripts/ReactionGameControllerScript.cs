using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ReactionGameControllerScript : MonoBehaviour {

	public int points;
	public GameObject pudding;
	public GameObject SpawnPoint;
	public List<GameObject> spawnedPuddings;
	public GameObject Piston;

	// Use this for initialization
	void Start () 
	{
		points = 0;
		InvokeRepeating("EverySecond",0,1);
		spawnedPuddings = new List<GameObject>();
	}

	private void EverySecond()
	{
		GameObject onePudding = (GameObject)Instantiate(pudding,SpawnPoint.transform.position,transform.rotation);
		spawnedPuddings.Add(onePudding);
	}
	
	// Update is called once per frame
	void Update () 
	{
		foreach(GameObject p in spawnedPuddings)
		{
			Vector3 pos = p.transform.position;
			pos.x++;
			p.transform.position = pos;

			if(pos.x > 100)
			{
				//TODO fix this
				//spawnedPuddings.Remove(p);
				//Destroy(p.transform.gameObject);
			}
		}
	}

	public void	ActivatePiston()
	{
		Piston.GetComponent<Animator>().Play(0);
	}
}
