using UnityEngine;
using System.Collections;

public class Master : MonoBehaviour
{

	public int timer;
	public int pointsToWin;
	public int handicap;
	// Use this for initialization
	void Start ()
	{
		timer = 5 * 60;
		pointsToWin = 5;
		handicap = 50;
		DontDestroyOnLoad (this);
	}
	
	// Update is called once per frame
	void Update ()
	{	
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
}
