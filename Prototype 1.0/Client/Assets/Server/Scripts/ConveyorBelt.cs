using UnityEngine;
using System.Collections;

public class ConveyorBelt : MonoBehaviour {

	public Vector3 direction;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		for(int i = 0; i < transform.childCount; i++)
		{
			if(transform.GetChild (i).tag != "Moving Platform")
				transform.GetChild (i).position += direction * Time.deltaTime;
		}
	}
}
