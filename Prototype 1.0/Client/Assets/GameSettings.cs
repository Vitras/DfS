using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameSettings : MonoBehaviour
{

	public Stack<Menu> menus;
	// Use this for initialization
	void Start ()
	{
		menus = new Stack<Menu> ();
		menus.Push (new ChoiceMenu (CreateInitial ()));
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (menus.Count != 0) {
			Menu currentMenu = menus.Peek ();			
			if (Input.GetKeyDown (KeyCode.JoystickButton1)) {
				if (menus.Count != 1) {
					currentMenu.Done = true;
				}
			}
			currentMenu.Update ();
			if (currentMenu.Done) {
				currentMenu.Exit ();
				menus.Pop ();
				menus.Peek ().Continue ();
			}
		}
	}

	GameObject[,] CreateInitial ()
	{
		GameObject[,] initial = new GameObject[2, 2];		
		Master master = GameObject.Find ("Master").GetComponent<Master> ();
		initial [0, 0] = Instantiate (Resources.Load<GameObject> ("Prefabs/ChoiceButton")) as GameObject;
		initial [0, 0].GetComponentInChildren <Text> ().text = "Set the timer to 5 minutes.";	
		initial [0, 0].GetComponent <Button> ().onClick.AddListener (() => master.SetTimer (5));
		initial [1, 0] = Instantiate (Resources.Load<GameObject> ("Prefabs/ChoiceButton")) as GameObject;
		initial [1, 0].GetComponentInChildren <Text> ().text = "Set the timer to 10 minutes.";		
		initial [1, 0].GetComponent <Button> ().onClick.AddListener (() => master.SetTimer (10));
		initial [0, 1] = Instantiate (Resources.Load<GameObject> ("Prefabs/ChoiceButton")) as GameObject;
		initial [0, 1].GetComponentInChildren <Text> ().text = "Set the timer to 15 minutes.";
		initial [0, 1].GetComponent <Button> ().onClick.AddListener (() => master.SetTimer (15));	
		initial [1, 1] = Instantiate (Resources.Load<GameObject> ("Prefabs/ChoiceButton")) as GameObject;
		initial [1, 1].GetComponentInChildren <Text> ().text = "Set the timer to 20 minutes.";
		initial [1, 1].GetComponent <Button> ().onClick.AddListener (() => master.SetTimer (20));
		SetChoice (initial, 1);
		return initial;
	}

	void CreatePointSelection ()
	{
		GameObject[,] initial = new GameObject[2, 2];
		Master master = GameObject.Find ("Master").GetComponent<Master> ();
		initial [0, 0] = Instantiate (Resources.Load<GameObject> ("Prefabs/ChoiceButton")) as GameObject;
		initial [0, 0].GetComponentInChildren <Text> ().text = "Set the points required to win to 5.";	
		initial [0, 0].GetComponent <Button> ().onClick.AddListener (() => master.SetPoints (5));
		initial [1, 0] = Instantiate (Resources.Load<GameObject> ("Prefabs/ChoiceButton")) as GameObject;
		initial [1, 0].GetComponentInChildren <Text> ().text = "Set the points required to win to 10.";		
		initial [1, 0].GetComponent <Button> ().onClick.AddListener (() => master.SetPoints (10));	
		initial [0, 1] = Instantiate (Resources.Load<GameObject> ("Prefabs/ChoiceButton")) as GameObject;
		initial [0, 1].GetComponentInChildren <Text> ().text = "Set the points required to win to 15.";
		initial [0, 1].GetComponent <Button> ().onClick.AddListener (() => master.SetPoints (15));	
		initial [1, 1] = Instantiate (Resources.Load<GameObject> ("Prefabs/ChoiceButton")) as GameObject;
		initial [1, 1].GetComponentInChildren <Text> ().text = "Set the points required to win to 20.";
		initial [1, 1].GetComponent <Button> ().onClick.AddListener (() => master.SetPoints (20));	
		SetChoice (initial, 2);		
		menus.Peek ().Suspend ();
		menus.Push (new ChoiceMenu (initial));
	}

	void CreateHandicap ()
	{
		GameObject[,] initial = new GameObject[2, 2];
		Master master = GameObject.Find ("Master").GetComponent<Master> ();
		initial [0, 0] = Instantiate (Resources.Load<GameObject> ("Prefabs/ChoiceButton")) as GameObject;
		initial [0, 0].GetComponentInChildren <Text> ().text = "Give all mobile players 50 points on start.";	
		initial [0, 0].GetComponent <Button> ().onClick.AddListener (() => master.SetHandicap (50));
		initial [1, 0] = Instantiate (Resources.Load<GameObject> ("Prefabs/ChoiceButton")) as GameObject;
		initial [1, 0].GetComponentInChildren <Text> ().text = "Give all mobile players 100 points on start.";		
		initial [1, 0].GetComponent <Button> ().onClick.AddListener (() => master.SetHandicap (100));	
		initial [0, 1] = Instantiate (Resources.Load<GameObject> ("Prefabs/ChoiceButton")) as GameObject;
		initial [0, 1].GetComponentInChildren <Text> ().text = "Give all mobile players 150 points on start.";
		initial [0, 1].GetComponent <Button> ().onClick.AddListener (() => master.SetHandicap (250));	
		initial [1, 1] = Instantiate (Resources.Load<GameObject> ("Prefabs/ChoiceButton")) as GameObject;
		initial [1, 1].GetComponentInChildren <Text> ().text = "Give all mobile players 200 points on start.";
		initial [1, 1].GetComponent <Button> ().onClick.AddListener (() => master.SetHandicap (500));	
		SetChoice (initial, 3);
		menus.Peek ().Suspend ();
		menus.Push (new ChoiceMenu (initial));
	}

	void SetChoice (GameObject[,] objects, int choice)
	{
		for (int x = 0; x < objects.GetLength (0); x++)
			for (int y = 0; y < objects.GetLength (1); y++) {			
				objects [x, y].transform.SetParent (this.transform, false);
				objects [x, y].GetComponent<Button> ().onClick.AddListener (() => GoToChoice (choice));
			}
	}
	public void GoToChoice (int state)
	{
		Debug.Log (state);
		switch (state) {
		//Timer selected
		case 1:
			CreatePointSelection ();
			break;
		//Points selected
		case 2:
			CreateHandicap ();
			break;
		//Handicap selected
		case 3:
			break;
		default:
			break;
		}
	}
}
