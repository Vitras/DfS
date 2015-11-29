using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MovingPlatform : MonoBehaviour
{
	public NormalEffect[] normalEffects;
	public SineEffect[] sineEffects;
	public bool moving;
	public Vector3 originalPosition;
	public TriggerScript trigger;
	// Use this for initialization
	void Start ()
	{
		originalPosition = transform.position;
//		moving = false;
		FillEffects ();
		InitializeEffects ();
		ApplyEffects ();
	}

	// Update is called once per frame
	void Update ()
	{
		if (trigger.Triggered || moving) {
			ApplyEffects ();
		}
	}

	void InitializeEffects ()
	{
		for (int x = 0; x < normalEffects.Length; x++) {
			normalEffects [x].Initialize (transform);
		}
		for (int x = 0; x < sineEffects.Length; x++) {			
			sineEffects [x].Initialize (transform);
		}
	}
	void FillEffects ()
	{
		normalEffects = gameObject.GetComponents<NormalEffect> ();
		sineEffects = gameObject.GetComponents<SineEffect> ();
	}

	void ApplyEffects ()
	{
		transform.position = originalPosition;
		for (int x = 0; x < normalEffects.Length; x++) {
			transform.position += normalEffects [x].Apply ();
		}
		for (int x = 0; x < sineEffects.Length; x++) {
			transform.position += sineEffects [x].Apply ();
		}
	}
}
