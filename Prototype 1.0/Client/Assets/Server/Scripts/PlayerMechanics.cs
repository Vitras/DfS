using UnityEngine;
using System.Collections;

public class PlayerMechanics : MonoBehaviour
{
	public bool grounded;
	public float speed;
	public float jumpHeight;
	public Vector3 direction; //degrees, 0 is right (cos 0 = x = 1), counter clockwise
	// Use this for initialization
	void Start ()
	{
		Physics.gravity = new Vector3 (0, -40, 0);
		direction = Vector3.forward;
	}
	
	// Update is called once per frame
	void Update ()
	{
		Rigidbody body = GetComponent<Rigidbody>();
		if (grounded) {			
			if (Mathf.Abs (body.velocity.y) > 0.2f) {
				grounded = false;
				return;
			}
			float jump = Input.GetAxis ("Jump");
			Move();			
			transform.LookAt (transform.position + direction);
				if (jump > 0.1f) {
					grounded = false;
				direction.y = jumpHeight;
			}
			body.velocity = direction;
			}
		else {
			if (Mathf.Abs (body.velocity.y) < 0.5f && CheckGround (body.position))
				grounded = true;
		}	
	}

	void Move()
	{
		GameObject camera = GameObject.Find("Main Camera");
		Vector3 forward = camera.transform.TransformDirection(Vector3.forward);
		forward.y = 0;
		forward = forward.normalized;
		Vector3 right  = new Vector3(forward.z, 0, -forward.x);
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");
		
		direction  = (h * right  + v * forward);
		direction *= speed;
	}
	
	
	void OnCollisionEnter(Collision col)
	{
		if(col.gameObject.name == "Goal")
		{
			GameObject.Find ("EnvironmentManager").GetComponent<Environment>().GoalReached();
		}
		else if(col.gameObject.tag == "Moving Platform")
		{
			transform.SetParent (col.gameObject.transform.parent);
		}
	}

	void OnCollisionExit(Collision col)
	{
		if(col.gameObject.tag == "Moving Platform")
		transform.SetParent(null);
	}


	bool CheckGround (Vector3 origin)
	{
		Vector3 size = this.GetComponent<MeshFilter> ().mesh.bounds.size;
		Debug.DrawRay (origin, Vector3.down * 2.5f, Color.red, 5);
		Debug.DrawRay (origin + new Vector3 (size.x * 2, 0, 0), Vector3.down * 2.5f, Color.red, 5);
		Debug.DrawRay (origin + new Vector3 (-size.x * 2, 0, 0), Vector3.down * 2.5f, Color.red, 5);
		Debug.DrawRay (origin + new Vector3 (0, 0, size.z * 2), Vector3.down * 2.5f, Color.red, 5);
		Debug.DrawRay (origin + new Vector3 (0, 0, -size.z * 2), Vector3.down * 2.5f, Color.red, 5);
		return Physics.Raycast (origin, Vector3.down, 2.5f ) || Physics.Raycast (origin + new Vector3 (size.x * 2, 0, 0), Vector3.down, 2.5f) ||
			Physics.Raycast (origin + new Vector3 (-size.x * 2, 0, 0), Vector3.down, 2.5f) || Physics.Raycast (origin + new Vector3 (0, 0, size.z * 2), Vector3.down, 2.5f)
			|| Physics.Raycast (origin + new Vector3 (0, 0, -size.z * 2), Vector3.down, 2.5f);
	}
}
