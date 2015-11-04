using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Networking.NetworkSystem;

public class NetworkScript : MonoBehaviour
{

	public string serverIP;
	public int port = 25000;
	public NetworkClient client;

	// Use this for initialization
	void Awake ()
	{
		serverIP = "127.0.0.1";
		DontDestroyOnLoad (transform.gameObject);
	}


	public void ConnectToServer ()
	{
		Debug.Log ("trying to connect...");
		serverIP = GameObject.Find ("InputField").GetComponentsInChildren<Text> () [1].text;
		if (serverIP == "") {
			//connect to localhost by default
			serverIP = "127.0.0.1";
		}
		GameObject.Find ("TitleText").GetComponent<Text> ().text = "Connecting to " + serverIP + "  ........";
		client = new NetworkClient ();
		client.RegisterHandler (MsgType.Connect, OnConnected); 
		//client.RegisterHandler(MsgType.Disconnect, OnDisconnected);
		//client.RegisterHandler(MsgType.Error,OnError);
		client.Connect (serverIP, port);
		
	}


	public void OnConnected (NetworkMessage netMsg)
	{
		Debug.Log ("Connected to server");
		var msg = new Messages.InitialMessage();
		msg.ip = Network.player.ipAddress;
		msg.username = GameObject.Find ("UserNameField").GetComponentsInChildren<Text> () [1].text;
		client.Send(Messages.initialMessageId,msg);
		Application.LoadLevel ("InGame");
	}

	public void SendCommandToServer (string command)
	{
		var msg = new Messages.CommandMessage();
		msg.command = command;
		client.Send(Messages.commandMessageId,msg);
		Debug.Log ("Command sent: " + command + "with message id: " + Messages.commandMessageId);
	}

}


