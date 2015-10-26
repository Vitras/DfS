using UnityEngine;
using System.Collections;

public class CameraBehaviour : MonoBehaviour
{

	public GameObject followTarget;
	public Vector3 offset;
	public Quaternion rotation;
	// Use this for initialization
	void Start ()
	{
		offset = followTarget.transform.position - transform.position;
	}
	
	// Update is called once per frame
	void Update ()
	{
		float finalAngle = followTarget.transform.eulerAngles.y;
		rotation = Quaternion.Euler (0, finalAngle, 0);
		offset += new Vector3 (0, Input.GetAxis ("CameraVertical"), 0) * 0.2f;
		transform.position = followTarget.transform.position - (rotation * offset);
		transform.LookAt (followTarget.transform);

	}
}
