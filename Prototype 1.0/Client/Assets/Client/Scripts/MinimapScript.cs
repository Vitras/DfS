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
	public Text notificationIndicator,pointsIndicator;

	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		pointsIndicator.text = NetworkScript.instance.Points.ToString() + " Points";
		//
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
			return;
		}

		if(NetworkScript.instance.Points >= 100)
		{
			NetworkScript.instance.SendCommandToServer("T|" + lastOpenedPanel.ToString() + "|" + direction);
			Debug.Log("command sent: " +  "T|" + lastOpenedPanel.ToString() + "|" + direction);
			NetworkScript.instance.Points -= 100;
		}
		else
		{
			notificationIndicator.text = "Not enough points!";
			StartCoroutine(ClearNotification(3.0f));
		}

		panelOpen = false;
		GameObject.Destroy(lastCreatedPanel);

	}
	

	IEnumerator ClearNotification(float time)
	{
		yield return new WaitForSeconds(time);
		notificationIndicator.text = "Activate level objects. this costs 100 points each time!";
	}


}
