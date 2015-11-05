using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Environment : MonoBehaviour
{

	/*
	Clear the slimes near the lever!
	Shut the production down!
	Make your way to the elevator!
	 */

	int currentObjective;
	public GameObject canvas;
	public GameObject IWinIndicator;
	public GameObject ObjectiveIndicator;
	public GameObject[] objectives;

	// Use this for initialization
	void Start ()
	{
		canvas.SetActive (true);
		currentObjective = 0;
		ActivateObjective ();
	}
	
	// Update is called once per frame
	void Update ()
	{
	}

	public void CheckObjectives ()
	{
			currentObjective++;
			if (currentObjective >= objectives.Length) {
				WinTheGame ();
			} else {
				ActivateObjective ();
			}
	}

	void ActivateObjective ()
	{
		canvas.SetActive (true);
		ObjectiveIndicator.SetActive (true);
		ObjectiveIndicator.GetComponent<Text> ().text = objectives [currentObjective].GetComponent<Leverswitch>().task; 
		objectives[currentObjective].GetComponent<Leverswitch>().Active = true;
		print(objectives[currentObjective]);
	}

	void WinTheGame ()
	{
		canvas.SetActive (true);
		IWinIndicator.SetActive (true);
	}

}
