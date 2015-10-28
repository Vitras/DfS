using UnityEngine;
using System.Collections;

public class CameraBehaviour : MonoBehaviour
{

	public GameObject followTarget;
	public Vector3 offsetX;
	public Vector3 offsetY;
	public Quaternion rotation;
	public float height;
	public float distance;
	public float turnSpeed;
	// Use this for initialization
	void Start ()
	{
		offsetX = new Vector3(0, height, distance);
		offsetY = new Vector3(0, 0, distance);
	}
	
	// Update is called once per frame
	void Update ()
	{
		float finalAngle = followTarget.transform.eulerAngles.y;
		rotation = Quaternion.Euler (0, finalAngle, 0);
		offsetX = Quaternion.AngleAxis (Input.GetAxis ("CameraHorizontal") * turnSpeed, Vector3.up) * offsetX;
		offsetY = Quaternion.AngleAxis (Input.GetAxis ("CameraVertical") * turnSpeed, Vector3.right) * offsetY;
		//offset += new Vector3 (0, Input.GetAxis ("CameraVertical"), 0) * 0.2f;
		transform.position = followTarget.transform.position + offsetX + offsetY;
		transform.LookAt (followTarget.transform);

	}
}
