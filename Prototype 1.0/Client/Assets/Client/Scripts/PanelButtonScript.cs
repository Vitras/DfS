using UnityEngine;
using System.Collections;

public class PanelButtonScript : MonoBehaviour 
{
	public void ButtonOnClick(string direction)
	{
		Debug.Log("button clicked:" + direction);
		GameObject controller = GameObject.Find("MinimapController");
		controller.GetComponent<MinimapScript>().PanelButtonOnClick(direction);
	}
}
