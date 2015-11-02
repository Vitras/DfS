using UnityEngine;
using System.Collections;

public class PowerupScript : MonoBehaviour {
	


	// Use this for initialization
	void Start () {
		//
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 pos = this.transform.position;
		pos.x++;
		this.transform.position = pos;
	}

	void OnMouseDown()
	{
		GameObject.Find("IdleGameController").GetComponent<IdleGameControllerScript>().Currency += 50;
		Destroy(this.gameObject);
	}
}
