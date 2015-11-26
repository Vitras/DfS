using UnityEngine;
using System.Collections;

public class Spiral : MonoBehaviour {

	
	public int triggerID;
	public bool moving;
	public Vector3 originalRotation;
	public Vector3 turningPoint;
	public float frequency;

	// Use this for initialization
	void Start () {		
		originalRotation = transform.localRotation.eulerAngles;
		turningPoint = transform.position;	
	}
	
	// Update is called once per frame
	void Update () {		
		if (moving && frequency != 0) {
			transform.RotateAround(turningPoint, Vector3.down, frequency * Time.deltaTime);
		}
	}
}
