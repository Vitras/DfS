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
			GameObject.Find("ConnectionManager").GetComponent<Client>().SendCommandToServer(command);
			Debug.Log("sent command: " + command.ToString());
			IdleGameController.GetComponent<IdleGameControllerScript>().Currency -= cost;
		}
		else
		{
			messageBox.color = Color.red;
			messageBox.text = "You don't have enough points!";
		}
	}
}
