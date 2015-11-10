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

		if(this.transform.position.x > 500)
		{
			Destroy(transform.gameObject);
		}
	}

	void OnMouseDown()
	{
		GameObject.Find("IdleGameController").GetComponent<IdleGameControllerScript>().Currency += 20;
		Destroy(this.gameObject);
	}
}
