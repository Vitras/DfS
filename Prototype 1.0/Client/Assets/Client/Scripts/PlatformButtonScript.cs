using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlatformButtonScript : MonoBehaviour {

	public string command;
	public int cost;
	public GameObject IdleGameController;
	public Text messageBox;

	[SerializeField] private Button pfb = null;
	
	void Start() 
	{ 
		pfb.onClick.AddListener(() => {PlatformCommand();});
	}
	
	public void PlatformCommand()
	{
		if(IdleGameController.GetComponent<IdleGameControllerScript>().Currency >= cost)
		{
			GameObject.Find("NetworkManager").GetComponent<NetworkScript>().SendCommandToServer(command);
			IdleGameController.GetComponent<IdleGameControllerScript>().Currency -= cost;
			Debug.Log("sent command: " + command.ToString());
		}
		else
		{
			messageBox.color = Color.red;
			messageBox.text = "You don't have enough points!";
		}
	}
}
