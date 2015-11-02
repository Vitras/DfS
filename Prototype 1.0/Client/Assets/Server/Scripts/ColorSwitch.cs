using UnityEngine;
using System.Collections;

public class ColorSwitch : MonoBehaviour
{
	Material mat;
	public int setter;
	// Use this for initialization
	void Start ()
	{
		mat = this.GetComponent<Renderer> ().material;
		setter = 2;
	}
	
	// Update is called once per frame
	void Update ()
	{
		switch (setter) {
		case 0:
			mat.color = Color.white;
			break;			
		case 1:
			mat.color = Color.red;
			break;			
		case 2:
			mat.color = Color.blue;
			break;			
		case 3:
			mat.color = Color.black;
			break;

		}
	}
}
