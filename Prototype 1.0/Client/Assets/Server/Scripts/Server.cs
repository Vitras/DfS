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
	private Dictionary<string,ServerSidePlayer> players;
	public Text eventFeed;
	public Text IpIndicator;
	
	void Start ()
	{
		//create dictionary for players
		players = new Dictionary<string,ServerSidePlayer>();
		//Start the server
		NetworkServer.Listen (port);
		//Add listener for onconnect messages from clients
		NetworkServer.RegisterHandler (MsgType.Connect, OnConnected);
		//listener for commands from clients
		NetworkServer.RegisterHandler (Messages.commandMessageId, OnReceiveCommand);
		//listener for initial client messages with their data
		NetworkServer.RegisterHandler (Messages.initialMessageId,OnReceiveInitialMessage);
		Debug.Log ("Server started");
		//Populate the ip address indicator and event feed
		IpIndicator.text = Network.player.ipAddress;
		eventFeed.text = "Important updates will appear here!";
	}

	void OnApplicationQuit()
	{
		NetworkServer.DisconnectAll();
	}

	void OnConnected (NetworkMessage netMsg)
	{
		Debug.Log ("A Player connected");
	}

	void OnReceiveInitialMessage(NetworkMessage netMsg)
	{
		var msg = netMsg.ReadMessage<Messages.InitialMessage>();
		ServerSidePlayer p = new ServerSidePlayer(msg.username);
		players.Add(msg.ip,p);
		Debug.Log("Added player: " + msg.username + "with ip: " + msg.ip + " To the playerlog.");
		eventFeed.text = msg.username + " joined the game!";
	}

	public void OnReceiveCommand (NetworkMessage netMsg)
	{
		var msg = netMsg.ReadMessage<Messages.CommandMessage>();
		string command = msg.command;
		Debug.Log ("received command:" + command);
		eventFeed.text = GetUsernameByIp(msg.ip) + " just activated command " + command;
		if (command.StartsWith ("T")) {
			int trigger = Convert.ToInt32 (command.Substring (1));
			GameObject.Find ("EnvironmentManager").GetComponent<EnvironmentTriggers> ().Trigger (trigger);
			return;
		}
		if (command == "0") 
		{
			GameObject.Find ("Player").GetComponent<ColorSwitch> ().increment ();
		} 
		//
	}

	//supporting functions
	public string GetUsernameByIp(string ip)
	{
		ServerSidePlayer p;
		players.TryGetValue(ip,out p);
		return p.userName;
	}


}

//supporting classes

public class ServerSidePlayer
{
	public string userName;
	//room for more here

	public ServerSidePlayer(string UN)
	{
		userName = UN;
	}
}
