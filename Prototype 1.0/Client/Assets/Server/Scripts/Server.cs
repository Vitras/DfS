using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using UnityEngine.UI;


public class Server : MonoBehaviour
{
	private int port = 25000;
	private Dictionary<string,ServerSidePlayer> players;
	public Text eventFeed;
	public Text IpIndicator;
	public enum Team
	{
		Red=0,
		Blue=1,
		None=9
	}
	
	void Start ()
	{
		//create dictionary for players
		players = new Dictionary<string,ServerSidePlayer> ();
		//Start the server
		NetworkServer.Listen (port);
		Debug.Log ("Server started");
		//Add listener for onconnect messages from clients
		NetworkServer.RegisterHandler (MsgType.Connect, OnConnected);
		//listener for commands from clients
		NetworkServer.RegisterHandler (Messages.commandMessageId, OnReceiveCommand);
		//listener for initial client messages with their data
		NetworkServer.RegisterHandler (Messages.initialMessageId, OnReceiveInitialMessage);
		//listener for disconnectmessages from client
		NetworkServer.RegisterHandler (Messages.clientDisconnectMessageId, OnReceiveClientDisconnectMessage);
		//Populate the ip address indicator and event feed
		IpIndicator.text = Network.player.ipAddress;
		eventFeed.text = "Important updates will appear here!";
	}

	void OnApplicationQuit ()
	{
		NetworkServer.DisconnectAll ();
	}

	void OnConnected (NetworkMessage netMsg)
	{
		Debug.Log ("A Player connected");
	}

	void OnReceiveInitialMessage (NetworkMessage netMsg)
	{
		var msg = netMsg.ReadMessage<Messages.InitialMessage> ();
		Team balancedTeamChoice = BalancedTeamAssign ();
		ServerSidePlayer p = new ServerSidePlayer (msg.username, balancedTeamChoice);
		players.Add (msg.ip, p);
		Debug.Log ("Added player: " + msg.username + "with ip: " + msg.ip + " To the playerlog.");
		eventFeed.text = msg.username + " joined the game on team: " + balancedTeamChoice.ToString () + " !";
		//send team message here
		var teamMsg = new Messages.CommunicateTeamToClientMessage ();
		teamMsg.team = (int)balancedTeamChoice;
		netMsg.conn.Send (Messages.communicateTeamToClientMessageId, teamMsg);
	}

	public void OnReceiveCommand (NetworkMessage netMsg)
	{
		var msg = netMsg.ReadMessage<Messages.CommandMessage> ();
		string command = msg.command;
		Debug.Log ("received command:" + command);
		eventFeed.text = GetUsernameByIp (msg.ip) + " just activated command " + command;
		if (command.StartsWith ("T")) {
			string[] commands = command.Split ('|');
			int suffixValue = 0;
			switch (commands [2]) {
			case "left":
				suffixValue = 1;
				break;
			case "right":
				suffixValue = 2;
				break;
			case "top":
				suffixValue = 3;
				break;
			case "bottom":
				suffixValue = 4;
				break;
			default:
				break;
			}
			int triggerID = int.Parse (commands [1]) * 4 + suffixValue;
			GameObject.Find ("EnvironmentManager").GetComponent<EnvironmentTriggers> ().Trigger (triggerID);
			return;
		}
		if (command == "0") {
			GameObject.Find ("Player").GetComponent<ColorSwitch> ().increment ();
		} 
		//
	}

	public void OnReceiveClientDisconnectMessage (NetworkMessage netMsg)
	{
		var msg = netMsg.ReadMessage<Messages.ClientDisconnectMessage> ();
		string name = GetUsernameByIp (msg.ip);
		players.Remove (msg.ip);
		eventFeed.text = name + " has disconnected...";
	}

	//supporting functions
	public string GetUsernameByIp (string ip)
	{
		ServerSidePlayer p;
		players.TryGetValue (ip, out p);
		return p.userName;
	}

	public Team BalancedTeamAssign ()
	{
		Dictionary<string,ServerSidePlayer> redPlayers = new Dictionary<string,ServerSidePlayer> ();
		Dictionary<string,ServerSidePlayer> bluePlayers = new Dictionary<string,ServerSidePlayer> ();

		redPlayers = FindTeam (Team.Red);
		bluePlayers = FindTeam (Team.Blue);

		if (redPlayers.Count >= bluePlayers.Count) {
			return Team.Blue;
		} else {
			return Team.Red;
		}
	}

	public Dictionary<string,ServerSidePlayer> FindTeam (Team t)
	{
		Dictionary<string,ServerSidePlayer> filteredDictionary = new Dictionary<string,ServerSidePlayer> ();
		filteredDictionary = players;
		filteredDictionary = filteredDictionary.Where (p => p.Value.teamColor == t).ToDictionary (p => p.Key, p => p.Value);
		return filteredDictionary;
	}


}

//supporting classes

public class ServerSidePlayer
{
	public string userName;
	//room for more here
	public Server.Team teamColor;

	public ServerSidePlayer (string UN, Server.Team team)
	{
		userName = UN;
		teamColor = team;

	}
}
