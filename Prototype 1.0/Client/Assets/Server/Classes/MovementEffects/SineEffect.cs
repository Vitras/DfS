using UnityEngine;
using System.Collections;

public class SineEffect : MovementEffect {

	public float t;
	public float amplitude;
	public float frequency;
	public bool cosine;
	public int direction; //0 = x, 1 = y, 2 = z;
	// Use this for initialization
	public SineEffect() : base()
	{

	}	
	// Update is called once per frame	
	
	public override void Initialize(Transform transform)
	{
	}

	public override Vector3 Apply()
	{
		t += Time.deltaTime;
	switch(direction){
		case 0: return new Vector3(SineModifier(), 0, 0); 
		case 1: return new Vector3(0, SineModifier(), 0); 
		case 2: return new Vector3(0, 0, SineModifier()); 
		default: return Vector3.zero;
		}
	}

	public float SineModifier()
	{
		if(cosine)
			return amplitude * Mathf.Cos(frequency * t);
			else
		return amplitude * Mathf.Sin(frequency * t);
	}
}
