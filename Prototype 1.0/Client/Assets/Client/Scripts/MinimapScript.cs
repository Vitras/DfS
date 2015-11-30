using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MinimapScript : MonoBehaviour 
{

	public GameObject smallPanel;
	public Canvas canvas;
	private bool panelOpen = false;
	private int lastOpenedPanel = 0;
	private GameObject lastCreatedPanel;
	public Text pointIndicator,notificationIndicator;
	private int points;

	// Use this for initialization
	void Start () 
	{
		GameObject networkManager = GameObject.Find("NetworkManager");
		NetworkScript script = networkManager.GetComponent<NetworkScript>();
		pointIndicator.text = script.points.ToString();
		points = script.points;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
	}


	public void ButtonClick(int button)
	{
		if(!panelOpen)
		{
			GameObject panel = Instantiate(smallPanel) as GameObject;
			panel.transform.SetParent(canvas.transform,false);
			panelOpen = true;
			lastOpenedPanel = button;
			lastCreatedPanel = panel;

		}
	}

	public void PanelButtonOnClick(string direction)
	{

		if(direction == "return")
		{
			panelOpen = false;
			GameObject.Destroy(lastCreatedPanel);
			pointIndicator.text = points.ToString();
			return;
		}

		if(points >= 100)
		{
			GameObject networkManager = GameObject.Find("NetworkManager");
			NetworkScript script = networkManager.GetComponent<NetworkScript>();
			script.SendCommandToServer("T|" + lastOpenedPanel.ToString() + "|" + direction);
			Debug.Log("command sent: " +  "T|" + lastOpenedPanel.ToString() + "|" + direction);
			points -= 100;
		}
		else
		{
			notificationIndicator.text = "Not enough points!";
			StartCoroutine(ClearNotification(3.0f));
		}

		panelOpen = false;
		GameObject.Destroy(lastCreatedPanel);
		pointIndicator.text = points.ToString();

	}

	public void ReturnButton()
	{
		GameObject networkManager = GameObject.Find("NetworkManager");
		networkManager.GetComponent<NetworkScript>().points = points;
		Application.LoadLevel("InGame");
	}

	IEnumerator ClearNotification(float time)
	{
		yield return new WaitForSeconds(time);
		notificationIndicator.text = "Activate level objects. this costs 100 points each time!";
	}


}
