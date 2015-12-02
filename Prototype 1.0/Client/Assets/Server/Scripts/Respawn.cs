using UnityEngine;
using System.Collections;

public class Respawn : MonoBehaviour {

	public GameObject player;
	public GameObject spawnPoint;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter (){
		player.transform.position = spawnPoint.transform.position;
	}
}
