using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MinimapScript : MonoBehaviour 
{
	
	public Canvas canvas;
	public Text notificationIndicator,pointsIndicator;
	public GameObject mappanel, controlPanel;
	public GameObject leftButton,rightButton,topButton,bottomButton;
	public GameObject colorIndicator1,colorIndicator2;
	public int activeColor = 1;
	public GameObject updateLog;

	// Use this for initialization
	void Start () 
	{
		NetworkScript.instance.AskForObjectives();
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		pointsIndicator.text = NetworkScript.instance.Points.ToString() + " Points";
	}



	public void MapButtonClick(int button)
	{
		switch(button)
		{
		case 1:
			configureMapButtons(button,"Colors/App_red_light","","Buttons/App_walls","","Buttons/App_doors",false,true,false,true);
			break;
		case 2:
			configureMapButtons(button,"Colors/App_red_medium","Buttons/App_piston","Buttons/App_walls","","Buttons/App_movingplatform",true,true,false,true);
			break;
		case 3:
			configureMapButtons(button,"Colors/App_red_dark","Buttons/App_cog","","","Buttons/App_zigzag",true,false,false,true);
			break;
		case 4:
			configureMapButtons(button,"Colors/App_orange_light","","Buttons/App_walls","Buttons/App_zigzag","Buttons/App_vertmoving",false,true,true,true);
			break;
		case 5:
			configureMapButtons(button,"Colors/App_orange_medium","Buttons/App_vertmoving","Buttons/App_doors","Buttons/App_cog","Buttons/App_zigzag",true,true,true,true);
			break;
		case 6:
			configureMapButtons(button,"Colors/App_orange_dark","Buttons/App_walls","","Buttons/App_movingplatform","Buttons/App_trapdoor", true,false,true,true);
			break;
		case 7:
			configureMapButtons(button,"Colors/App_yellow_light","","Buttons/App_pushblock","Buttons/App_cog","Buttons/App_piston",false,true,true,true);
			break;
		case 8:
			configureMapButtons(button,"Colors/App_yellow_medium","Buttons/App_cog", "Buttons/App_movingplatform", "Buttons/App_trapdoor", "Buttons/App_vertmoving",true,true,true,true);
			break;
		case 9:
			configureMapButtons(button,"Colors/App_yellow_dark","Buttons/App_pushblock", "", "Buttons/App_doors", "Buttons/App_piston",true,false,true,true);
			break;
		case 10:
			configureMapButtons(button,"Colors/App_green_light","", "Buttons/App_doors", "Buttons/App_zigzag", "Buttons/App_trapdoor",false,true,true,true);
			break;
		case 11:
			configureMapButtons(button,"Colors/App_green_medium","Buttons/App_walls", "Buttons/App_piston", "Buttons/App_movingplatform", "Buttons/App_zigzag",true,true,true,true);
			break;
		case 12:
			configureMapButtons(button,"Colors/App_green_dark","Buttons/App_pushblock", "", "Buttons/App_cog", "Buttons/App_trapdoor",true,false,true,true);
			break;
		case 13:
			configureMapButtons(button,"Colors/App_blue_light","", "Buttons/App_piston", "Buttons/App_doors", "Buttons/App_cog",false,true,true,true);
			break;
		case 14:
			configureMapButtons(button,"Colors/App_blue_medium","Buttons/App_movingplatform", "Buttons/App_pushblock", "Buttons/App_cog", "Buttons/App_vertmoving",true,true,true,true);
			break;
		case 15:
			configureMapButtons(button,"Colors/App_blue_dark","Buttons/App_walls", "", "Buttons/App_vertmoving", "Buttons/App_doors",true,false,true,true);
			break;
		case 16:
			configureMapButtons(button,"Colors/App_purple_light","", "Buttons/App_movingplatform", "Buttons/App_vertmoving", "",false,true,true,false);
			break;
		case 17:
			configureMapButtons(button,"Colors/App_purple_medium","Buttons/App_cog", "Buttons/App_piston", "Buttons/App_trapdoor", "",true,true,true,false);
			break;
		case 18:
			configureMapButtons(button,"Colors/App_purple_dark","Buttons/App_doors", "", "Buttons/App_movingplatform", "",true,false,true,false);
			break;

		}
	}

	public void configureMapButtons(int button,string colorPath ,string leftPath, string rightPath, string topPath, string bottomPath,bool leftButtonActive,bool rightButtonActive, bool topButtonActive, bool bottomButtonActive)
	{
		if(leftButtonActive)
			leftButton.SetActive(true);
		else
			leftButton.SetActive(false);

		if(rightButtonActive)
			rightButton.SetActive(true);
		else
			rightButton.SetActive(false);;

		if(topButtonActive)
			topButton.SetActive(true);
		else
			topButton.SetActive(false);

		if(bottomButtonActive)
			bottomButton.SetActive(true);
		else
			bottomButton.SetActive(false);

		

		colorIndicator1.GetComponent<Image>().sprite = Resources.Load<Sprite>(colorPath);
		colorIndicator2.GetComponent<Image>().sprite = Resources.Load<Sprite>(colorPath);
		rightButton.GetComponent<Image>().sprite = Resources.Load<Sprite>(rightPath);
		bottomButton.GetComponent<Image>().sprite = Resources.Load<Sprite>(bottomPath);
		topButton.GetComponent<Image>().sprite = Resources.Load<Sprite>(topPath);
		leftButton.GetComponent<Image>().sprite = Resources.Load<Sprite>(leftPath);
		activeColor = button;
	}



	public void PanelButtonOnClick(string direction)
	{


		if(NetworkScript.instance.Points >= 50)
		{
			NetworkScript.instance.SendCommandToServer("T|" + activeColor.ToString() + "|" + direction);
			Debug.Log("command sent: " +  "T|" + activeColor.ToString() + "|" + direction);
			NetworkScript.instance.Points -= 50;
		}
		else
		{
			notificationIndicator.text = "Not enough points!";
			StartCoroutine(ClearNotification(3.0f));
		}

	}

	public void SwitchToMap()
	{
		NetworkScript.instance.AskForObjectives();
		mappanel.SetActive(true);
		controlPanel.SetActive(false);
	}

	public void SwitchToControl()
	{
		mappanel.SetActive(false);
		controlPanel.SetActive(true);
	}
	

	IEnumerator ClearNotification(float time)
	{
		yield return new WaitForSeconds(time);
		notificationIndicator.text = "Activate level objects. this costs 50 points each time!";
	}


}
