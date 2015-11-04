using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Messages
{
	public const short commandMessageId = 1000;
	public const short initialMessageId = 1001;

	public class InitialMessage : MessageBase
	{
		public string ip;
		public string username;
	}

	public class CommandMessage : MessageBase
	{
		public string command;
	}

}
