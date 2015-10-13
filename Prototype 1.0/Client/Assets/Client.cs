using UnityEngine;
using System.Collections;

public class Client : MonoBehaviour
{
	
	public string serverIP = "127.0.0.1";
	public int port = 25000;
	private string _messageLog = "";
	string someInfo = "";
	private NetworkPlayer _myNetworkPlayer;
	
	void OnGUI ()
	{
		if (Network.peerType == NetworkPeerType.Disconnected) {
			if (GUI.Button (new Rect (100, 100, 150, 25), "Connect")) {
				Network.Connect (serverIP, port);
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
			}
		}
		
		GUI.TextArea (new Rect (250, 100, 300, 300), _messageLog);
	}
	
	[RPC]
	void SendInfoToServer ()
	{
		someInfo = "Client " + _myNetworkPlayer.guid + ": hello server";
		GetComponent<NetworkView> ().RPC ("ReceiveInfoFromClient", RPCMode.Server, someInfo);
	}

	[RPC]
	void SendCommandToServer (string command)
	{
		someInfo = command;
		GetComponent<NetworkView> ().RPC ("ReceiveInfoFromClient", RPCMode.Server, someInfo);
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
		_messageLog += someInfo + "\n";
	}
	
	void OnConnectedToServer ()
	{
		_messageLog += "Connected to server" + "\n";
	}
	void OnDisconnectedToServer ()
	{
		_messageLog += "Disco from server" + "\n";
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
