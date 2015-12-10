using UnityEngine;
using System.Collections;

public class PuddingScript : MonoBehaviour {
	
	public bool success;

	// Use this for initialization
	void Start () 
	{
		success = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(this.transform.position.x > 1000)
		{
			GameObject.Find("ReactionGameController").GetComponent<ReactionGameControllerScript>().spawnedPuddings.Remove(this.gameObject);
			Destroy(transform.gameObject);
		}
	}
}
