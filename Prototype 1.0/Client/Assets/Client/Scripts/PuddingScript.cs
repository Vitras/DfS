using UnityEngine;
using System.Collections;

public class PuddingScript : MonoBehaviour {

	public GameObject parent;
	public bool success;

	// Use this for initialization
	void Start () 
	{
		success = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(this.transform.position.x > 100)
		{
			if(!success)
			{
				//parent.GetComponent<ReactionGameControllerScript>().points -= 5;
			}
			parent.GetComponent<ReactionGameControllerScript>().spawnedPuddings.Remove(this.gameObject);
			Destroy(transform.gameObject);
		}
	}
}
