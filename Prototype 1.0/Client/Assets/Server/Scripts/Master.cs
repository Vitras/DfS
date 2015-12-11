using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Master : MonoBehaviour
{

	public int timer;
	public int pointsToWin;
	public int handicap;
	public int LastBluePoints;
	public int LastRedPoints;
	public bool GameEnded;
	public AudioSource source;
	// Use this for initialization
	void Start ()
	{
		timer = 1 * 60;
		pointsToWin = 1;
		handicap = 50;
		DontDestroyOnLoad (this);
	}
	
	// Update is called once per frame
	void Update ()
	{	
		if (GameObject.Find ("Main Camera") != null) {
			transform.position = GameObject.Find ("Main Camera").transform.position;
		}
		if (Application.loadedLevelName == "MainMenu") {
			source.enabled = true;
		}
		if (GameEnded && Application.loadedLevelName == "End") {
			GameEnded = false;
			EndGame ();
		}
	}

	public void SetTimer (int time)
	{
		timer = time * 60;
	}

	public void SetPoints (int points)
	{
		pointsToWin = points;
	}
	public void SetHandicap (int handicap)
	{
		this.handicap = handicap;
	}
	public void EndGame ()
	{
		if (LastBluePoints + LastRedPoints >= pointsToWin) {
			GameObject.Find ("FinishText").GetComponent<Text> ().text = "You made it! Well done!";
			//you won
		} else {
			GameObject.Find ("FinishText").GetComponent<Text> ().text = "You ran out of time...";
			//you lost
		}
		if (LastRedPoints > LastBluePoints) {			
			GameObject.Find ("WinnerIndicator").GetComponent<Image> ().color = Color.red;

			//red won
		} else if (LastRedPoints < LastBluePoints) {
			GameObject.Find ("WinnerIndicator").GetComponent<Image> ().color = Color.blue;
		} else {			
			GameObject.Find ("WinnerIndicator").GetComponent<Image> ().color = Color.green;
			//tie
		}
	}

}
