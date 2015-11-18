using UnityEngine;
using System.Collections;

public class MonsterSpawnTrigger : MonoBehaviour {

	public TriggerScript trigger;
	public bool debugTrigger;
	public GameObject[] spawnedSlimes;
	public int current;
	// Use this for initialization
	void Start () {
		spawnedSlimes = new GameObject[5];
		current = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if(debugTrigger)
		{
			trigger.Triggered = debugTrigger;
			debugTrigger = false;
		}
		if(trigger.Triggered)
		{
			GameObject slime = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Slime"));
			trigger.Triggered = false;
			slime.transform.position = transform.position + new Vector3(0, 1.2f, -6);
			spawnedSlimes[current] = slime;
			slime.GetComponent<SlimeAI>().goal = GameObject.Find ("Player").transform;
			current++;
			if (current > 4)
				current = 0;
		}
	}
}
