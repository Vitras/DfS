using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlatformButtonScript : MonoBehaviour {

	public int command;

	[SerializeField] private Button pfb = null;
	
	void Start() 
	{ 
		pfb.onClick.AddListener(() => {PlatformCommand();});
	}
	
	public void PlatformCommand()
	{
		GameObject.Find("ConnectionManager").GetComponent<Client>().SendCommandToServer(command.ToString());
		Debug.Log("sent command: " + command.ToString());
	}
}
