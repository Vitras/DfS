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
	public int ScoreRed { get; set; }
	public int ScoreBlue{ get; set; }
	public GameObject canvas;
	public GameObject ScoreRedIndicator;
	public GameObject ScoreBlueIndicator;
	public GameObject IWinIndicator;
	public GameObject ObjectiveIndicator;
	public GameObject[] objectives;

	// Use this for initialization
	void Start ()
	{
		canvas.SetActive (true);
		currentObjective = 0;
		ScoreRed = 0;
		ScoreBlue = 0;
		ScoreRedIndicator.SetActive (true);
		ScoreBlueIndicator.SetActive (true);
		//ActivateObjective ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		ScoreRedIndicator.GetComponent<Text> ().text = "Red team: " + ScoreRed.ToString ();
		ScoreBlueIndicator.GetComponent<Text> ().text = "Blue team: " + ScoreBlue.ToString ();
		if (ScoreRed + ScoreBlue >= GameObject.Find ("Master").GetComponent<Master> ().pointsToWin) {
			EndGame ();
		}
	}

	public void EndGame ()
	{
		Master master = GameObject.Find ("Master").GetComponent<Master> ();
		master.LastRedPoints = ScoreRed;
		master.LastBluePoints = ScoreBlue;
		master.GameEnded = true;
		Application.LoadLevel ("End");
	}
	
	public void CheckObjectives ()
	{
		currentObjective++;
		if (currentObjective >= objectives.Length) {
			//WinTheGame ();
		} else {
			//ActivateObjective ();
		}
	}

	void ActivateObjective ()
	{
		canvas.SetActive (true);
		ObjectiveIndicator.SetActive (true);
		ObjectiveIndicator.GetComponent<Text> ().text = objectives [currentObjective].GetComponent<Leverswitch> ().task; 
		objectives [currentObjective].GetComponent<Leverswitch> ().Active = true;
		print (objectives [currentObjective]);
	}

	void WinTheGame ()
	{
		canvas.SetActive (true);
		IWinIndicator.SetActive (true);
	}

}
