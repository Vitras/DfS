using UnityEngine;
using System.Collections;

public class PlayerMechanics2 : MonoBehaviour
{
	public bool grounded;
	public float speed;
	public float jumpHeight;
	public Vector3 direction;	
	private Vector3 correction;
// Use this for initialization
	void Start ()
	{
		Physics.gravity = new Vector3 (0, -40, 0);
		direction = Vector3.forward;
		correction = new Vector3 (0, 0.46f, 0);
	}

// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.JoystickButton0)) {
			RaycastHit hit = new RaycastHit ();
			if (Physics.Raycast (transform.position, direction, out hit, 15)) {
				if (hit.collider.tag == "Interactable" && hit.collider.gameObject.GetComponent<Leverswitch> ().Active) {
					hit.collider.gameObject.GetComponent<Leverswitch> ().Switched = !hit.collider.gameObject.GetComponent<Leverswitch> ().Switched;
					hit.collider.gameObject.GetComponent<Leverswitch> ().Active = false;
					GameObject.Find ("EnvironmentManager").GetComponent<Environment> ().CheckObjectives ();
				}
			}
		}
		Rigidbody body = GetComponent<Rigidbody> ();
		if (Mathf.Abs (body.velocity.y) > 0.2f) {
			grounded = false;
		}
		float jump = Input.GetAxis ("Jump");
		Move ();
		transform.LookAt (transform.position - direction);
		direction.y = body.velocity.y;
		if (jump > 0.1f && grounded) {
			grounded = false;
			direction.y = jumpHeight;
		}
		body.velocity = direction;
		if (Mathf.Abs (body.velocity.y) < 0.5f && CheckGround (body.position + correction))
			grounded = true;
	}	

	void Move ()
	{
		GameObject camera = GameObject.Find ("Main Camera");
		Vector3 forward = camera.transform.TransformDirection (Vector3.forward);
		forward.y = 0;
		forward = forward.normalized;
		Vector3 right = new Vector3 (forward.z, 0, -forward.x);
		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");
	
		direction = (h * right + v * forward);
		direction *= speed;
	}


	void OnCollisionEnter (Collision col)
	{
		if (col.gameObject.name == "Goal") {
			GameObject.Find ("EnvironmentManager").GetComponent<Environment> ().CheckObjectives ();
		} else if (col.gameObject.tag == "Moving Platform") {
			RaycastHit hit;
			if(Physics.Raycast (GetComponent<Rigidbody>().position + correction, Vector3.down, out hit, 0.5f) && hit.transform.gameObject == col.gameObject)
				transform.SetParent (col.gameObject.transform.parent);
		}
	}

	void OnTriggerEnter (Collider col)
	{
		
		if (col.gameObject.tag == "Objective A") {
			GameObject.Find ("EnvironmentManager").GetComponent<Environment> ().ScoreRed++;
			col.gameObject.GetComponent<SphereCollider> ().enabled = false;

		} else if (col.gameObject.tag == "Objective B") {
			GameObject.Find ("EnvironmentManager").GetComponent<Environment> ().ScoreBlue++;
			col.gameObject.GetComponent<SphereCollider> ().enabled = false;
		}
	}
	void OnCollisionExit (Collision col)
	{
		if (col.gameObject.tag == "Moving Platform"){
			RaycastHit hit;
		if(!Physics.Raycast (GetComponent<Rigidbody>().position + correction, Vector3.down, out hit, 0.5f))
			transform.SetParent (null);
			   }
	}


	bool CheckGround (Vector3 origin)
	{
		Vector3 size = this.GetComponent<MeshFilter> ().mesh.bounds.size;
		Debug.DrawRay (origin, Vector3.down * 2.5f, Color.red, 5);
		Debug.DrawRay (origin + new Vector3 (size.x * 2, 0, 0), Vector3.down * 2.5f, Color.red, 5);
		Debug.DrawRay (origin + new Vector3 (-size.x * 2, 0, 0), Vector3.down * 2.5f, Color.red, 5);
		Debug.DrawRay (origin + new Vector3 (0, 0, size.z * 2), Vector3.down * 2.5f, Color.red, 5);
		Debug.DrawRay (origin + new Vector3 (0, 0, -size.z * 2), Vector3.down * 2.5f, Color.red, 5);
		return Physics.Raycast (origin, Vector3.down, 2.5f) || Physics.Raycast (origin + new Vector3 (size.x * 2, 0, 0), Vector3.down, 2.5f) ||
			Physics.Raycast (origin + new Vector3 (-size.x * 2, 0, 0), Vector3.down, 2.5f) || Physics.Raycast (origin + new Vector3 (0, 0, size.z * 2), Vector3.down, 2.5f)
			|| Physics.Raycast (origin + new Vector3 (0, 0, -size.z * 2), Vector3.down, 2.5f);
	}
}

