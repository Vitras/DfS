using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class Server : MonoBehaviour
{
	private int port = 25000;
	private int playerCount = 0;
	
	void Start ()
	{
		NetworkServer.Listen(port);
		NetworkServer.RegisterHandler(MsgType.Connect, OnConnected);
		Debug.Log("Server started");
	}
	
	void Update ()
	{

	}

	void OnConnected(NetworkMessage netMsg)
	{
		//AskClientForInfo (player);
		//SendTriggersToClient ();
		//
		Debug.Log("Player connected" + netMsg.conn);
	}



}
