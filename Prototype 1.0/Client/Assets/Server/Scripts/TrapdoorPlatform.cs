using UnityEngine;
using System.Collections;

public class TrapdoorPlatform : MonoBehaviour
{

	public bool active;
	public bool opening;
	public float openTime;
	public float closeTime;
	float currentTime;
	public GameObject left;
	public GameObject right;
	Vector3 rightTurningPoint;
	Vector3 leftTurningPoint;
	// Use this for initialization
	void Start ()
	{
		Vector3 size = right.transform.lossyScale;
		rightTurningPoint = transform.localPosition + new Vector3 (size.x, 0, 0);
		leftTurningPoint = transform.localPosition - new Vector3 (size.x, 0, 0);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (active) {
			if (currentTime <= 0) {
				if (opening) {
					right.transform.RotateAround (rightTurningPoint, Vector3.forward, 1);
					left.transform.RotateAround (leftTurningPoint, Vector3.back, 1);
					if (left.transform.eulerAngles.x >= 90) {
						opening = false;
						currentTime = openTime;
					}
				} else {
					right.transform.RotateAround (rightTurningPoint, Vector3.back, 1);
					left.transform.RotateAround (leftTurningPoint, Vector3.forward, 1);
					if (Mathf.Abs (left.transform.eulerAngles.x) < 0.5) {
						opening = true;
						currentTime = closeTime;
					}
				} 
			} else
				currentTime -= Time.deltaTime;
		}
	}
}