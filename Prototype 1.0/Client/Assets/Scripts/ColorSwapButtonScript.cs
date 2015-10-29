using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ColorSwapButtonScript : MonoBehaviour 
{
	[SerializeField] private Button csb = null;
	public int cost;
	public GameObject IdleGameController;
	public Text messageBox;
	
	void Start() 
	{ 
		csb.onClick.AddListener(() => {ColorSwapCommand();});
	}

	public void ColorSwapCommand()
	{

		if(IdleGameController.GetComponent<IdleGameControllerScript>().Currency >= cost)
		{
			GameObject.Find("ConnectionManager").GetComponent<Client>().SendColorSwapCommandToServer();
			IdleGameController.GetComponent<IdleGameControllerScript>().Currency -= cost;
		}
		else
		{
			messageBox.color = Color.red;
			messageBox.text = "You don't have enough points!";
		}
	}

}
