using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Messages
{
	public const short commandMessageId = 1000;
	public const short initialMessageId = 1001;
	public const short clientDisconnectMessageId = 1002;
	public const short respondAliveMessageId = 1003;
	public const short communicateTeamToClientMessageId = 2000;
	public const short checkAliveMessageId = 2001;


	public class InitialMessage : MessageBase
	{
		public string ip;
		public string username;
	}

	public class CommandMessage : MessageBase
	{
		public string ip;
		public int id;
		public string command;
	}

	public class ClientDisconnectMessage : MessageBase
	{
		public string ip;
		public int id;
	}

	public class CommunicateTeamToClientMessage : MessageBase
	{
		public int team;
		public int id;
	}

	public class CheckAliveMessage : MessageBase
	{

	}

	public class RespondAliveMessage : MessageBase
	{
		public int id;
	}
}
