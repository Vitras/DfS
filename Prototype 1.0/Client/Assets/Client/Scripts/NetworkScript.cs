using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Networking.NetworkSystem;

public class NetworkScript : MonoBehaviour {

	public string serverIP;
	public int port = 25000;
	public NetworkClient client;
	public short commandMessageId = 1000;

	// Use this for initialization
	void Awake () 
	{
		serverIP = "127.0.0.1";
		DontDestroyOnLoad(transform.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ConnectToServer()
	{
		Debug.Log("trying to connect...");
		serverIP = GameObject.Find("InputField").GetComponentsInChildren<Text>()[1].text;
		if(serverIP == "")
		{
			//connect to localhost by default
			serverIP = "127.0.0.1";
		}
		GameObject.Find("TitleText").GetComponent<Text>().text = "Connecting to " + serverIP +"  ........";
		client = new NetworkClient();
		client.RegisterHandler(MsgType.Connect, OnConnected); 
		//client.RegisterHandler(MsgType.Disconnect, OnDisconnected);
		//client.RegisterHandler(MsgType.Error,OnError);
		client.Connect(serverIP, port);
		
	}

	public void DisconnectFromServer()
	{
		client.Disconnect();
		Debug.Log("Disconnected from server.");
		Application.LoadLevel("Main");
		Destroy(transform.gameObject);
	}

	public void OnConnected(NetworkMessage netMsg)
	{
		Debug.Log("Connected to server");
		Application.LoadLevel("InGame");
	}

	public void OnDisconnectedFromServer(NetworkDisconnection info)
	{
		Debug.Log("Connection Lost..." + info);
		Application.LoadLevel("Main");
		Destroy(transform.gameObject);
	} 

	public void OnError(NetworkMessage netMsg)
	{
		//doNothing
	}

	public void SendCommandToServer(string command)
	{
		var msg = new StringMessage(command);
		client.Send(commandMessageId,msg);
		Debug.Log("Command sent: " + command + "with message id: " + commandMessageId.ToString());
	}

}
