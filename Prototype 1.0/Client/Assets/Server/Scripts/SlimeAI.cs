using UnityEngine;
using System.Collections;

public class SlimeAI : MonoBehaviour {
	
	public Transform goal;	
	public float actionTime;
	public bool wandering;
	NavMeshAgent agent;
	public Vector3 direction;
	public float speed;
	public float maxChaseDistance;

	void Start () {
		agent = GetComponent<NavMeshAgent>();
		agent.destination = goal.position; 
		agent.speed = speed;
		agent.enabled = false;
		actionTime = Random.Range(0f, 3f);
	}

	void Update()
	{
		if(agent.enabled)
		{
			if(agent.remainingDistance > maxChaseDistance)
			{
				agent.enabled = false;
				actionTime = Random.Range (0f, 3f);
			}
			else
				agent.destination = goal.position;
		}
		else
		{
			RaycastHit hit = new RaycastHit();
			if(Physics.Raycast (transform.position, direction, out hit, 15))
			{
				if(hit.collider.name == "Player")
				{
				wandering = false;
				agent.enabled = true;
				actionTime = Random.Range (0f, 3f);
				}
			}
			else if(actionTime >= 0)
			{
				if(wandering)
				{
					Vector3 newPos = transform.position + direction * Time.deltaTime * speed;
					transform.LookAt(newPos);
					transform.position = newPos;
				}
				actionTime -= Time.deltaTime;
			}
			else
			{
				actionTime = Random.Range (0f, 3f);
				wandering = !wandering;
				if(wandering)
				{
					direction = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
					direction.Normalize ();
				}
			}
		}
	}
}
