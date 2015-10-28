using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class Server : MonoBehaviour
{
	private int port = 25000;
	private int playerCount = 0;
	private string _messageLog = "";
	
	void Start ()
	{
		if (Network.peerType == NetworkPeerType.Disconnected)
			Network.InitializeServer (10, port, false);
	}
	
	void Update ()
	{

	}

	public void OnGUI ()
	{
//		if (Network.peerType == NetworkPeerType.Server) {
//			GUI.Label (new Rect (100, 100, 150, 25), "Server");
//			GUI.Label (new Rect (100, 125, 150, 25), "Clients attached: " + Network.connections.Length);
//			
//			if (GUI.Button (new Rect (100, 150, 150, 25), "Quit server")) {
//				Network.Disconnect ();
//				Application.Quit ();
//			}
//			if (GUI.Button (new Rect (100, 175, 150, 25), "Send hi to client"))
//				SendInfoToClient ();
//		}
//		GUI.TextArea (new Rect (275, 100, 300, 300), _messageLog);
	}
	
	void OnPlayerConnected (NetworkPlayer player)
	{
		AskClientForInfo (player);
		SendTriggersToClient ();
	}
	
	void AskClientForInfo (NetworkPlayer player)
	{
		GetComponent<NetworkView> ().RPC ("SetPlayerInfo", player, player);
	}
	
	[RPC]
	void ReceiveInfoFromClient (string someInfo)
	{
		if (someInfo.StartsWith ("T")) {
			int trigger = Convert.ToInt32 (someInfo.Substring (1));
			GameObject.Find ("EnvironmentManager").GetComponent<EnvironmentTriggers> ().Trigger (trigger);
			return;
		}
		if (someInfo == "0" || someInfo == "1" || someInfo == "2" || someInfo == "3") {
			GameObject.Find ("Player").GetComponent<ColorSwitch> ().setter = int.Parse (someInfo);
		} 
		//_messageLog += someInfo + "\n";
		Debug.Log (someInfo);
	}
	
	string someInfo = "Server: hello client";
	[RPC]
	void SendInfoToClient ()
	{
		GetComponent<NetworkView> ().RPC ("ReceiveInfoFromServer", RPCMode.Others, someInfo);
	}

	[RPC]
	void SendTriggersToClient ()
	{
		GameObject[] triggers = GameObject.Find ("EnvironmentManager").GetComponent<EnvironmentTriggers> ().networkTriggers;
		//GetComponent<NetworkView> ().RPC ("ReceiveInfoFromServer", RPCMode.Others, "TSTART");
		for (int x = 0; x < triggers.Length; x++) {
			GetComponent<NetworkView> ().RPC ("ReceiveInfoFromServer", RPCMode.Others, "T" + x.ToString ());
		}		
		//GetComponent<NetworkView> ().RPC ("ReceiveInfoFromServer", RPCMode.Others, "TEND");
	}
	
	// fix RPC errors
	[RPC]
	void SendInfoToServer ()
	{
	}
	[RPC]
	void SetPlayerInfo (NetworkPlayer player)
	{
	}
	[RPC]
	void ReceiveInfoFromServer (string someInfo)
	{
	}
}
