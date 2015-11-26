using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MovingPlatform : MonoBehaviour
{
	public int triggerID;
	public List<MovementEffect> effects;
	public bool moving;
	public TriggerScript trigger;
	// Use this for initialization
	void Start ()
	{
		moving = false;
		effects = new List<MovementEffect>();
		ApplyEffects ();
	}

	// Update is called once per frame
	void Update ()
	{
		if (trigger.Triggered || moving) {
			ApplyEffects ();
		}
	}

	void FillEffects()
	{
		NormalEffect[] nEffects = gameObject.GetComponents<NormalEffect>();
		SineEffect[] sEffects = gameObject.GetComponents<SineEffect>();
		for(int x = 0; x < nEffects.Length; x++)
		{
			effects.Add (nEffects[x]);
		}
		for(int x = 0; x < sEffects.Length; x++)
		{
			effects.Add (sEffects[x]);
		}

	}

	void ApplyEffects()
	{		
		for(int x = 0; x < effects.Count; x++)
		{
			effects[x].Apply(transform.position);
		}
	}
}
