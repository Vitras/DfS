using UnityEngine;
using System.Collections;

public class Environment : MonoBehaviour {

	public GameObject canvas;
	// Use this for initialization
	void Start () {
		canvas.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void GoalReached()
	{
		canvas.SetActive (true);
	}
}
