using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GetTime : MonoBehaviour
{

	public float timeRemaining;
	// Use this for initialization
	void Start ()
	{
		timeRemaining = (float)GameObject.Find ("Master").GetComponent<Master> ().timer;
	}
	
	// Update is called once per frame
	void Update ()
	{
		timeRemaining = Mathf.Max (timeRemaining - Time.deltaTime, 0);
		if (timeRemaining <= 0) {
			GameObject.Find ("EnvironmentManager").GetComponent<Environment> ().EndGame ();
		}
		this.GetComponent<Text> ().text = ((int)timeRemaining).ToString ();
	

	}
}
