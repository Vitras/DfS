using UnityEngine;
using System.Collections;

public class MovementEffect : MonoBehaviour{
	
	public MovementEffect()
	{
	}

	public virtual void Initialize(Transform transform)
	{
	}

	public virtual Vector3 Apply()
	{
		return Vector3.zero;
	}
}
