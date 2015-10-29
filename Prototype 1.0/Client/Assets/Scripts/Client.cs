using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Client : MonoBehaviour
{
	
	public string serverIP = "127.0.0.1";
	public int port = 25000;
	private string _messageLog = "";
	string someInfo = "";
	private NetworkPlayer _myNetworkPlayer;	
	public List<int> triggers;

	//temp testing variables
	int colorSwap = 0;

	//Do not get rid of the networking when changing scene
	void Awake()
	{
		DontDestroyOnLoad(transform.gameObject);
	}


	public void ConnectToServer()
	{
		Debug.Log("trying to connect...");
		serverIP = GameObject.Find("InputField").GetComponentsInChildren<Text>()[1].text;
		Network.Connect(serverIP,port);
		GameObject.Find("TitleText").GetComponent<Text>().text = "Connecting to " + serverIP +"  ........";

	}
	

	/*
	void OnGUI ()
	{
		if (Network.peerType == NetworkPeerType.Disconnected) {
			serverIP = GUI.TextArea(new Rect(100, 125, 150, 25), serverIP);
			if (GUI.Button (new Rect (100, 100, 150, 25), "Connect")) {
				Network.Connect (serverIP ,port);
			}
		} else {
			if (Network.peerType == NetworkPeerType.Client) {
				GUI.Label (new Rect (100, 100, 150, 25), "client");
				
				if (GUI.Button (new Rect (100, 125, 150, 25), "Logut"))
					Network.Disconnect ();
				
				if (GUI.Button (new Rect (100, 150, 150, 25), "SendHello to server"))
					SendInfoToServer ();
				
				if (GUI.Button (new Rect (100, 175, 150, 25), "Make cube white"))
					SendCommandToServer ("0");

				if (GUI.Button (new Rect (100, 200, 150, 25), "Make cube red"))
					SendCommandToServer ("1");

				if (GUI.Button (new Rect (100, 225, 150, 25), "Make cube blue"))
					SendCommandToServer ("2");

				if (GUI.Button (new Rect (100, 250, 150, 25), "Make cube black"))
					SendCommandToServer ("3");
				foreach (int i in triggers) {
					if (GUI.Button (new Rect (100, 275 + 25 * i, 150, 25), "Activate trigger " + i.ToString ()))
						SendCommandToServer ("T" + i.ToString ());
				}
			}
		}
		
		GUI.TextArea (new Rect (250, 100, 300, 300), _messageLog);
	}
	*/

	void UpdateTriggerList (int triggerID)
	{
		triggers.Add (triggerID);
	}

	
	[RPC]
	void SendInfoToServer ()
	{
		someInfo = "Client " + _myNetworkPlayer.guid + ": hello server";
		GetComponent<NetworkView> ().RPC ("ReceiveInfoFromClient", RPCMode.Server, someInfo);
	}

	[RPC]
	public void SendCommandToServer (string command)
	{
		someInfo = command;
		GetComponent<NetworkView> ().RPC ("ReceiveInfoFromClient", RPCMode.Server, someInfo);
	}

	[RPC]
	public void SendColorSwapCommandToServer ()
	{
		someInfo = colorSwap.ToString();
		GetComponent<NetworkView> ().RPC ("ReceiveInfoFromClient", RPCMode.Server, someInfo);
		colorSwap++;
		if (colorSwap == 4)
			colorSwap = 0;
	}

	[RPC]
	void SetPlayerInfo (NetworkPlayer player)
	{
		_myNetworkPlayer = player;
		someInfo = "Player setted";
		GetComponent<NetworkView> ().RPC ("ReceiveInfoFromClient", RPCMode.Server, someInfo);
	}

	[RPC]
	void ReceiveInfoFromServer (string someInfo)
	{
		if (someInfo.StartsWith ("T")) {
			UpdateTriggerList ((Convert.ToInt32 (someInfo.Substring (1))));
		}

		_messageLog += someInfo + "\n";
	}
	
	void OnConnectedToServer ()
	{
		_messageLog += "Connected to server" + "\n";
		Debug.Log("Connected to server");
		Application.LoadLevel("InGame");
	}
	void OnDisconnectedToServer ()
	{
		Application.LoadLevel("Main");
		_messageLog += "Disconnected from server" + "\n";
	}

	void OnDisconnectedFromServer(NetworkDisconnection info) 
	{
		if (info == NetworkDisconnection.LostConnection)
		{
				Debug.Log("Lost connection to the server");
				Application.LoadLevel("Main");
		}
		else
		{
			Debug.Log("Successfully disconnected from the server");
			Application.LoadLevel("Main");
		}
	} 
	
	// fix RPC errors
	[RPC]
	void ReceiveInfoFromClient (string someInfo)
	{
	}
	[RPC]
	void SendInfoToClient (string someInfo)
	{
	}
}
