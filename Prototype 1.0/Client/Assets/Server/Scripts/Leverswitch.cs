using UnityEngine;
using System.Collections;

public class Leverswitch : MonoBehaviour
{

	public GameObject handle;
	public bool Switched {get; set;}
	public string task;
	public bool Active { get; set; }
	// Use this for initialization
	void Start ()
	{
		handle.transform.RotateAround (transform.position, Vector3.right, 30);
		Active = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
			if (Switched) {
				if (handle.transform.eulerAngles.x >= 330 || handle.transform.eulerAngles.x <= 31)
					handle.transform.RotateAround (transform.position, Vector3.left, 40 * Time.deltaTime);
			} else if (handle.transform.eulerAngles.x >= 325 || handle.transform.eulerAngles.x <= 30) {
				handle.transform.RotateAround (transform.position, Vector3.right, 40 * Time.deltaTime);
			}
	}
}
