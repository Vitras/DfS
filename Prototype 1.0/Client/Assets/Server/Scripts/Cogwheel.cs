using UnityEngine;
using System.Collections;

public class Cogwheel : MonoBehaviour {

	public int cogSpeed;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (Vector3.up, cogSpeed);
	}
}
