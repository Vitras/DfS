using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Networking.NetworkSystem;

public class NetworkScript : MonoBehaviour
{

	public static NetworkScript instance;

	public string serverIP = "127.0.0.1";
	public int port = 25000;
	public NetworkClient client;
	public string myIp;
	public int points;
	public enum Team {Red=0,Blue=1,None=9};
	private Team team;
	public int playerId;
	public int disconnectCounter = 0;
	public bool hasReconnected = false;
	public string lastConnectedIp;
	public GameObject listItem;
	private string userName;

	// Use this for initialization
	void Awake ()
	{
		if (instance != null) {
			DestroyImmediate(gameObject);
			return;
		}
		instance = this;
		DontDestroyOnLoad(gameObject);
	}
	
	public void Resetter()
	{
		//foreach(Transform child in transform)
		//{
			//Destroy(child.gameObject);
		//}
		instance = new NetworkScript();
		Destroy (gameObject);
	}

	void Start()
	{
		myIp = Network.player.ipAddress;
		points = 100;
		team = Team.None;
		//InvokeRepeating("CheckServerStatus",15.0f,5.0f);
		//disconnectCounter = 0;
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
		client.RegisterHandler (MsgType.Disconnect, OnDisconnect);
		client.RegisterHandler (Messages.communicateTeamToClientMessageId,OnReceiveTeamMessage);
		client.Connect (serverIP, port);
		lastConnectedIp = serverIP;
		
	}

	public void ReconnectToServer(string ip)
	{
		Debug.Log ("trying to connect...");
		serverIP = ip;
		if (serverIP == "") {
			//connect to localhost by default
			serverIP = "127.0.0.1";
		}
		//GameObject.Find ("TitleText").GetComponent<Text> ().text = "Connecting to " + serverIP + "  ........";
		client = new NetworkClient ();
		client.RegisterHandler (MsgType.Connect, OnConnected);
		client.RegisterHandler (MsgType.Disconnect, OnDisconnect);
		client.RegisterHandler (Messages.communicateTeamToClientMessageId,OnReceiveTeamMessage);
		client.Connect (serverIP, port);
		lastConnectedIp = serverIP;
	}


	public void OnConnected (NetworkMessage netMsg)
	{
		if(hasReconnected)
		{
			Debug.Log ("reConnected to server");
			//var msg = new Messages.InitialMessage();
			//msg.ip = myIp;
			//msg.username = GameObject.Find ("UserNameField").GetComponentsInChildren<Text> () [1].text;
			//client.Send(Messages.initialMessageId,msg);
			//Application.LoadLevel ("Minimap");
			//disconnectCounter = 0;
			//TODO post message abou this in app event log
			PostInUpdateLog("Reconnected! All is good", Color.black);
		}
		else
		{
			Debug.Log ("Connected to server");
			var msg = new Messages.InitialMessage();
			msg.ip = myIp;
			msg.username = GameObject.Find ("UserNameField").GetComponentsInChildren<Text> () [1].text;
			client.Send(Messages.initialMessageId,msg);
			userName = GameObject.Find ("UserNameField").GetComponentsInChildren<Text> () [1].text;
			Application.LoadLevel ("Minimap");
			disconnectCounter = 0;
			PostInUpdateLog("Connected to server! Enjoy!", Color.black);
		}
	}

	public void OnDisconnect(NetworkMessage netMsg)
	{
		//Debug.Log("disconnected by server...");
		//TODO implement wait for reconnect scene
		//Application.LoadLevel ("Main");
		//DestroyImmediate(transform.gameObject);

		disconnectCounter++;
		if(disconnectCounter <= 3)
		{
			ReconnectToServer(lastConnectedIp);
			hasReconnected = true;
			PostInUpdateLog("Connection lost... trying to reconnect!", Color.black);
		}
		else
		{
			PostInUpdateLog("Cannot reconnect :( is the server still running?", Color.black);
		}
	}

	public void PostInUpdateLog(string text, Color color)
	{
		if(GameObject.Find("MinimapController").GetComponent<MinimapScript>() != null)
		{
			var item = Instantiate(listItem) as GameObject;
			item.GetComponent<Text>().text = text;
			item.GetComponent<Text>().color = color;
			item.transform.SetParent(GameObject.Find("MinimapController").GetComponent<MinimapScript>().updateLog.transform,false);
		}
	}
	

	public void SendCommandToServer (string command)
	{
		var msg = new Messages.CommandMessage();
		msg.command = command;
		msg.ip = myIp;
		msg.id = playerId;
		client.Send(Messages.commandMessageId,msg);
		Debug.Log ("Command sent: " + command + "with message id: " + Messages.commandMessageId);
	}

	public void OnReceiveTeamMessage(NetworkMessage netMsg)
	{
		var msg = netMsg.ReadMessage<Messages.CommunicateTeamToClientMessage>();
		team = (Team)msg.team;
		playerId = msg.id;
		Debug.Log("player id set to: " + msg.id.ToString()); 
		Debug.Log("joined team: " + msg.team.ToString() + "with player id: " + playerId.ToString());
		GameObject.Find("Username").GetComponent<Text>().text = userName;
		if(team == Team.Blue)
		{
			PostInUpdateLog("Joined team Blue!", Color.blue);
			GameObject.Find("Username").GetComponent<Text>().color = Color.blue;

		}
		else
		{
			PostInUpdateLog("Joined team Red!", Color.red);
			GameObject.Find("Username").GetComponent<Text>().color = Color.red;
		}

	}

	void OnApplicationQuit()
	{
		if(client != null)
		{
			Application.CancelQuit();
			Debug.Log ("1 properly disconnected");
			var msg = new Messages.ClientDisconnectMessage();
			msg.ip = myIp;
			msg.id = playerId;
			client.Send(Messages.clientDisconnectMessageId,msg);
			StartCoroutine(CloseConnection());
			Debug.Log ("2 disconnected client");
			Debug.Log ("3 quit app");

		}

	}

	IEnumerator CloseConnection()
	{
		yield return new WaitForSeconds(0.5f);
		client.Disconnect();
		client = null;
		Application.Quit();
	}

	public int Points
	{
		get{return points;}
		set
		{
			points = value;
			if(points < 0)
				points = 0;
		}
	}

}


