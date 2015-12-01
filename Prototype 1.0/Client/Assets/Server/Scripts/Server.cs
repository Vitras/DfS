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
	private Dictionary<int,ServerSidePlayer> players;
	public Text eventFeed;
	public Text IpIndicator;
	private object thisLock = new object();
	public int idGenerator = 1000;
	public enum Team
	{
		Red=0,
		Blue=1,
		None=9
	}
	
	void Start ()
	{
		//create dictionary for players
		players = new Dictionary<int,ServerSidePlayer> ();
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
		ServerSidePlayer p;
		var msg = netMsg.ReadMessage<Messages.InitialMessage> ();
		Team balancedTeamChoice = BalancedTeamAssign ();
		lock(thisLock)
		{
			p = new ServerSidePlayer (msg.username, balancedTeamChoice,idGenerator,msg.ip);
			idGenerator++;
			players.Add (p.id, p);
		}
		Debug.Log ("Added player: " + msg.username + " with ip: " + msg.ip + " and id: " + p.id.ToString() + " To the playerlog.");
		eventFeed.text = msg.username + " joined the game on team: " + balancedTeamChoice.ToString () + " !";
		//send team message here
		var teamMsg = new Messages.CommunicateTeamToClientMessage ();
		teamMsg.team = (int)balancedTeamChoice;
		teamMsg.id = p.id;
		netMsg.conn.Send (Messages.communicateTeamToClientMessageId, teamMsg);
	}

	public void OnReceiveCommand (NetworkMessage netMsg)
	{
		var msg = netMsg.ReadMessage<Messages.CommandMessage> ();
		string command = msg.command;
		Debug.Log ("received command:" + command);
		eventFeed.text = GetUsernameById (msg.id) + " just activated command " + command;
		if (command.StartsWith ("T")) {
			string[] commands = command.Split ('|');
			int suffixValue = 0;
			switch (commands [2]) {
			case "left":
				break;
			case "right":
				suffixValue = 1;
				break;
			case "top":
				suffixValue = 2;
				break;
			case "bottom":
				suffixValue = 3;
				break;
			default:
				break;
			}
			int triggerID = (int.Parse (commands [1]) - 1) * 4 + suffixValue;
			Debug.Log (triggerID);
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
		string name = GetUsernameById (msg.id);
		players.Remove(msg.id);
		eventFeed.text = name + " has disconnected...";
		Debug.Log(players.ToString());
	}

	//supporting functions
	public string GetUsernameById (int id)
	{
		ServerSidePlayer p;
		Debug.Log(id.ToString());
		players.TryGetValue (id, out p);
		return p.userName;
	}

	public Team BalancedTeamAssign ()
	{
		Dictionary<int,ServerSidePlayer> redPlayers = new Dictionary<int,ServerSidePlayer> ();
		Dictionary<int,ServerSidePlayer> bluePlayers = new Dictionary<int,ServerSidePlayer> ();

		redPlayers = FindTeam (Team.Red);
		bluePlayers = FindTeam (Team.Blue);

		if (redPlayers.Count >= bluePlayers.Count) {
			return Team.Blue;
		} else {
			return Team.Red;
		}
	}

	public Dictionary<int,ServerSidePlayer> FindTeam (Team t)
	{
		Dictionary<int,ServerSidePlayer> filteredDictionary = new Dictionary<int,ServerSidePlayer> ();
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
	public string ip;
	public int id;

	public ServerSidePlayer (string UN, Server.Team team,int identifier,string ip)
	{
		userName = UN;
		teamColor = team;
		id = identifier;

	}
}
