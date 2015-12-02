using UnityEngine;
using System.Collections;

public class RotatingPlatform : MonoBehaviour
{
	public int triggerID;
	public bool moving;
	public Vector3 originalRotation;
	public float frequency;
	public TriggerScript trigger;
	// Use this for initialization
	void Start ()
	{
		moving = false;
		//originalRotation = transform.localRotation.eulerAngles;
		//transform.localRotation = Quaternion.Euler (new Vector3(90, 0, 0));

	}
	
	// Update is called once per frame
	void Update ()
	{
		if (((trigger != null && trigger.Triggered) || moving)&& frequency != 0) {
			transform.Rotate (Vector3.up, 360 * frequency * Time.deltaTime);

			//if (t > Mathf.PI * 2 * period)
			//t -= Mathf.PI * 2 * period;
		}
	}
}