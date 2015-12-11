using UnityEngine;
using System.Collections;

public class EndGame : MonoBehaviour
{

	
	public void GoToMain ()
	{
		Application.LoadLevel ("MainMenu");
	}
	
	public void GoToNewYork ()
	{
		Application.LoadLevel ("NewYork");
	}
}
