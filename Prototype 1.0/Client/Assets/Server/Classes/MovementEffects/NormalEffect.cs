using UnityEngine;
using System.Collections;

public class NormalEffect {

	public float moveTime;
	public float currentTime;
	public float speed;
	public Vector3 direction;
	public float stopTime;
	public float moving;

	public NormalEffect() : base()
	{
		
	}	
	// Update is called once per frame	
	
	public void Initialize()
	{
		
	}
	
	public void Apply(Vector3 position)
	{
		if(moving)
		{
			position += direction
		}
		currentTime -= Time.deltaTime;
		if(currentTime <= 0)
		{
			moving = -moving;
			if(moving)
			{
				currentTime = moveTime;
			}
			else
			{
				currentTime = stopTime;
			}
		}
	}
}
