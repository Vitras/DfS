using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

public class Player : NetworkBehaviour
{
	
	public GameObject colorObject;
	ColorSwitch colorSwitch;
	// Use this for initialization
	void Start ()
	{
		colorSwitch = colorObject.GetComponent<ColorSwitch> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		//if (!isLocalPlayer)
		//return;
		int i;
		if (Input.GetKeyDown (KeyCode.A))
			i = 0;//CmdChangeColor (0);
		else if (Input.GetKeyDown (KeyCode.A))
			i = 1;//CmdChangeColor (1);
		else if (Input.GetKeyDown (KeyCode.A))
			i = 2;//CmdChangeColor (2);
		else if (Input.GetKeyDown (KeyCode.A))
			i = 3;//CmdChangeColor (3);

	}
}
