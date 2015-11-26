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
	public enum Team {Red=0,Blue=1,None=9};
	private Team team;
	public GameObject teamIndicator;
	public Sprite blueTeam,redTeam;

	// Use this for initialization
	void Awake ()
	{
		serverIP = "127.0.0.1";
		DontDestroyOnLoad (transform.gameObject);
		myIp = Network.player.ipAddress;
		points = 100;
		team = Team.None;
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
		client.RegisterHandler (Messages.communicateTeamToClientMessageId,OnReceiveTeamMessage);
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

	public void OnReceiveTeamMessage(NetworkMessage netMsg)
	{
		var msg = netMsg.ReadMessage<Messages.CommunicateTeamToClientMessage>();
		team = (Team)msg.team;
		Debug.Log("joined team: " + msg.team.ToString());
		GameObject.Instantiate(teamIndicator,new Vector3(26,111,-2),new Quaternion(0,0,0,0));
		GameObject.Find("Team Indicator(Clone)").transform.parent = transform;
		if(team == Team.Blue)
			GameObject.Find("Team Indicator(Clone)").GetComponent<SpriteRenderer>().sprite = blueTeam;
		else
			GameObject.Find("Team Indicator(Clone)").GetComponent<SpriteRenderer>().sprite = redTeam;
	}

	void OnApplicationQuit()
	{
		if(client != null)
		{
			Application.CancelQuit();
			Debug.Log ("1 properly disconnected");
			var msg = new Messages.ClientDisconnectMessage();
			msg.ip = myIp;
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


}


