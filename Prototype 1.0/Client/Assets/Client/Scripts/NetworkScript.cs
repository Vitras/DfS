using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class NetworkScript : MonoBehaviour {

	public string serverIP;
	public int port = 25000;
	NetworkClient client;

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
		client.Connect(serverIP, port);
		
	}

	public void OnConnected(NetworkMessage netMsg)
	{
		Debug.Log("Connected to server");
		Application.LoadLevel("InGame");

	}
}
