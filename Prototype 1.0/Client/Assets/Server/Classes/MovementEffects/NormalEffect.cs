﻿using UnityEngine;
using System.Collections;

public class NormalEffect : MovementEffect {

	public float moveTime1;
	//public float moveTime2;
	public float currentTime;
	public float speed1;
	//public float speed2;
	public Vector3 direction;
	public float stopTime1;
	public float stopTime2;
	public bool moving;
	public bool oppositeDirection;
	public Vector3 translation;

	public NormalEffect() : base()
	{
		
	}	
	// Update is called once per frame	
	
	public override void Initialize(Transform transform)
	{
		
	}
	
	public override Vector3 Apply()
	{
		currentTime -= Time.deltaTime;		
		if(currentTime <= 0)
		{
			moving = !moving;
			if(moving)
			{
				currentTime = moveTime1;
			}
			else
			{
				oppositeDirection = !oppositeDirection;
				if(oppositeDirection)
					currentTime = stopTime2;
				else
					currentTime = stopTime1;
			}
		}
		if(moving)
		{
			if(oppositeDirection)
				translation = direction * speed1 * (currentTime);
			else				
				translation = direction * speed1 * (moveTime1 - currentTime);
		}
		return translation;
	}
}