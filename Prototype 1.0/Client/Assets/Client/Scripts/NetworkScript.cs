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
	public string myIp;
	public int points;

	// Use this for initialization
	void Awake ()
	{
		serverIP = "127.0.0.1";
		DontDestroyOnLoad (transform.gameObject);
		myIp = Network.player.ipAddress;
		points = 100;
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
		client.RegisterHandler (MsgType.Connect, OnConnected); ;
		client.RegisterHandler (MsgType.Disconnect, OnDisconnect);
		client.Connect (serverIP, port);
		
	}


	public void OnConnected (NetworkMessage netMsg)
	{
		Debug.Log ("Connected to server");
		var msg = new Messages.InitialMessage();
		msg.ip = myIp;
		msg.username = GameObject.Find ("UserNameField").GetComponentsInChildren<Text> () [1].text;
		client.Send(Messages.initialMessageId,msg);
		Application.LoadLevel ("InGame");
	}

	public void OnDisconnect(NetworkMessage netMsg)
	{
		Application.LoadLevel ("Main");
		DestroyImmediate(transform.gameObject);
	}
	

	public void SendCommandToServer (string command)
	{
		var msg = new Messages.CommandMessage();
		msg.command = command;
		msg.ip = myIp;
		client.Send(Messages.commandMessageId,msg);
		Debug.Log ("Command sent: " + command + "with message id: " + Messages.commandMessageId);
	}

}


