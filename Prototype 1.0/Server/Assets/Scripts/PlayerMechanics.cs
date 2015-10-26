using UnityEngine;
using System.Collections;

public class PlayerMechanics : MonoBehaviour
{
	public bool grounded;
	public float speed;
	public float jumpHeight;
	// Use this for initialization
	void Start ()
	{
		Physics.gravity = new Vector3 (0, -60, 0);
		speed = 20;
	}
	
	// Update is called once per frame
	void Update ()
	{
		Rigidbody body = this.GetComponent<Rigidbody> ();
		Vector3 velocity = body.velocity;
		if (grounded) {			
			if (Mathf.Abs (body.velocity.y) > 0.5f) {
				grounded = false;
				return;
			}
			float jump = Input.GetAxis ("Jump");
			velocity.x = speed * Input.GetAxis ("Horizontal");
			velocity.z = speed * Input.GetAxis ("Vertical");
			if (jump > 0.1f) {
				velocity.y = jumpHeight;
				grounded = false;
			}
			body.velocity = velocity;
		} else {
			if (Mathf.Abs (body.velocity.y) < 0.5f && CheckGround (body.position))
				grounded = true;
		}	
	}

	bool CheckGround (Vector3 origin)
	{
		Vector3 size = this.GetComponent<MeshFilter> ().mesh.bounds.size;
		Debug.DrawRay (origin, Vector3.down * 2, Color.red, 5);
		Debug.DrawRay (origin + new Vector3 (size.x * 2, 0, 0), Vector3.down * 2, Color.red, 5);
		Debug.DrawRay (origin + new Vector3 (-size.x * 2, 0, 0), Vector3.down * 2, Color.red, 5);
		Debug.DrawRay (origin + new Vector3 (0, 0, size.z * 2), Vector3.down * 2, Color.red, 5);
		Debug.DrawRay (origin + new Vector3 (0, 0, -size.z * 2), Vector3.down * 2, Color.red, 5);
		return Physics.Raycast (origin, Vector3.down, 2) || Physics.Raycast (origin + new Vector3 (size.x * 2, 0, 0), Vector3.down, 2) ||
			Physics.Raycast (origin + new Vector3 (-size.x * 2, 0, 0), Vector3.down, 2) || Physics.Raycast (origin + new Vector3 (0, 0, size.z * 2), Vector3.down, 2)
			|| Physics.Raycast (origin + new Vector3 (0, 0, -size.z * 2), Vector3.down, 2);
	}
}
