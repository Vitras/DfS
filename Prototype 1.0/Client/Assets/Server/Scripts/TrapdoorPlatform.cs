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
	public bool lengthWise;
	Vector3 rightTurningPoint;
	Vector3 leftTurningPoint;
	// Use this for initialization
	void Start ()
	{
		Vector3 size = right.transform.lossyScale;
		lengthWise = Mathf.Abs (transform.eulerAngles.y - 90) <= 1;
		if (lengthWise) {
			rightTurningPoint = transform.localPosition - new Vector3 (0, 0, size.z);
			leftTurningPoint = transform.localPosition + new Vector3 (0, 0, size.z);
		} else {
			rightTurningPoint = transform.localPosition + new Vector3 (size.x, 0, 0);
			leftTurningPoint = transform.localPosition - new Vector3 (size.x, 0, 0);
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		
		Debug.DrawRay (rightTurningPoint, Vector3.down * 2, Color.magenta, 60);
		Debug.DrawRay (leftTurningPoint, Vector3.down * 2, Color.magenta, 60);
		if (active) {
			if (lengthWise) {
				if (currentTime <= 0) {
					if (opening) {
						right.transform.RotateAround (rightTurningPoint, Vector3.right, 1);
						left.transform.RotateAround (leftTurningPoint, Vector3.left, 1);
						if (Mathf.Abs (right.transform.eulerAngles.z) >= 90) {
							opening = false;
							currentTime = openTime;
						}
					} else {
						right.transform.RotateAround (rightTurningPoint, Vector3.left, 1);
						left.transform.RotateAround (leftTurningPoint, Vector3.right, 1);
						if (Mathf.Abs (left.transform.eulerAngles.z) < 0.5) {
							opening = true;
							currentTime = closeTime;
						}
					} 
				} else
					currentTime -= Time.deltaTime;
			} else {
				if (currentTime <= 0) {
					if (opening) {
						right.transform.RotateAround (rightTurningPoint, Vector3.forward, 1);
						left.transform.RotateAround (leftTurningPoint, Vector3.back, 1);
						if (Mathf.Abs (right.transform.eulerAngles.z) >= 90) {
							opening = false;
							currentTime = openTime;
						}
					} else {
						right.transform.RotateAround (rightTurningPoint, Vector3.back, 1);
						left.transform.RotateAround (leftTurningPoint, Vector3.forward, 1);
						if (Mathf.Abs (left.transform.eulerAngles.z) < 0.5) {
							opening = true;
							currentTime = closeTime;
						}
					} 
				} else
					currentTime -= Time.deltaTime;
			}
		}
	}
}