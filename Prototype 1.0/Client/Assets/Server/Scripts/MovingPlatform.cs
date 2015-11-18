using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour
{
	public int triggerID;
	public bool moving;
	public Vector3 originalPosition;
	public float t;
	public float amplitude;
	public float period;
	public TriggerScript trigger;
	// Use this for initialization
	void Start ()
	{
		moving = false;
		originalPosition = transform.position - SineModifier ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (trigger.Triggered || moving) {
			t += Time.deltaTime;
			//if (t > Mathf.PI * 2 * period)
			//t -= Mathf.PI * 2 * period;
			transform.position = originalPosition + SineModifier ();
		}
	}

	Vector3 SineModifier ()
	{
		//print (amplitude * (Mathf.Sin (period * t)));
		return new Vector3 (amplitude * (Mathf.Sin (period * t)), 0, 0);
	}
}
