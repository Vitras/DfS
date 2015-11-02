using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using UnityEngine.UI;

public class Server : MonoBehaviour
{
	private int port = 25000;
	public short commandMessageId = 1000;
	
	void Start ()
	{
		NetworkServer.Listen (port);
		NetworkServer.RegisterHandler (MsgType.Connect, OnConnected);
		NetworkServer.RegisterHandler (commandMessageId, OnReceiveCommand);
		Debug.Log ("Server started");
		GameObject.Find ("Ipaddress Indicator").GetComponent<Text> ().text = Network.player.ipAddress;
	}
	
	void Update ()
	{

	}

	void OnConnected (NetworkMessage netMsg)
	{
		//AskClientForInfo (player);
		//SendTriggersToClient ();
		//
		Debug.Log ("Player connected" + netMsg.conn);
	}

	public void OnReceiveCommand (NetworkMessage msg)
	{
		var netMsg = msg.ReadMessage<StringMessage> ();
		string command = netMsg.value;
		Debug.Log ("received command:" + command);

		if (command.StartsWith ("T")) {
			int trigger = Convert.ToInt32 (command.Substring (1));
			GameObject.Find ("EnvironmentManager").GetComponent<EnvironmentTriggers> ().Trigger (trigger);
			return;
		}
		if (command == "0") {
			GameObject.Find ("Player").GetComponent<ColorSwitch> ().increment ();
		}
	}



}
