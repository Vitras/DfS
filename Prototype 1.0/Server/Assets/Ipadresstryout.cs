using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Net;

public class Ipadresstryout : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		string hostName = Dns.GetHostName ();
		IPAddress[] ipA = Dns.GetHostAddresses(hostName);
		for(int i = 0; i < ipA.Length; i++)
		{

		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
