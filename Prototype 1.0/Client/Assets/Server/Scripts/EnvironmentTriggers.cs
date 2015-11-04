using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnvironmentTriggers : MonoBehaviour
{
	public GameObject[] networkTriggers;
	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	public void Trigger (int index)
	{
		MovingPlatform trigger = networkTriggers [index].GetComponent<MovingPlatform> ();
		trigger.moving = !trigger.moving;
	}
}
