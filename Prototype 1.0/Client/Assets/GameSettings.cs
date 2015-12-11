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
		SetImages (initial, false);
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
		SetImages (initial, false);
		menus.Peek ().Suspend ();
		menus.Push (new ChoiceMenu (initial));
	}

	void CreateHandicap ()
	{
		GameObject[,] initial = new GameObject[2, 2];
		Master master = GameObject.Find ("Master").GetComponent<Master> ();
		initial [0, 0] = Instantiate (Resources.Load<GameObject> ("Prefabs/ChoiceButton")) as GameObject;
		initial [0, 0].GetComponentInChildren <Text> ().text = "Give all mobile players 25 points on start.";	
		initial [0, 0].GetComponent <Button> ().onClick.AddListener (() => master.SetHandicap (25));
		initial [1, 0] = Instantiate (Resources.Load<GameObject> ("Prefabs/ChoiceButton")) as GameObject;
		initial [1, 0].GetComponentInChildren <Text> ().text = "Give all mobile players 50 points on start.";		
		initial [1, 0].GetComponent <Button> ().onClick.AddListener (() => master.SetHandicap (50));	
		initial [0, 1] = Instantiate (Resources.Load<GameObject> ("Prefabs/ChoiceButton")) as GameObject;
		initial [0, 1].GetComponentInChildren <Text> ().text = "Give all mobile players 100 points on start.";
		initial [0, 1].GetComponent <Button> ().onClick.AddListener (() => master.SetHandicap (100));	
		initial [1, 1] = Instantiate (Resources.Load<GameObject> ("Prefabs/ChoiceButton")) as GameObject;
		initial [1, 1].GetComponentInChildren <Text> ().text = "Give all mobile players 150 points on start.";
		initial [1, 1].GetComponent <Button> ().onClick.AddListener (() => master.SetHandicap (150));	
		SetChoice (initial, 3);
		SetImages (initial, true);
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

	void SetImages (GameObject[,] objects, bool handicap)
	{
		if (!handicap) {
			SpriteState st = new SpriteState ();
			st.highlightedSprite = Resources.Load<Sprite> ("Textures/5HL");
			objects [0, 0].GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Textures/5Base");
			objects [0, 0].GetComponent<Button> ().spriteState = st;
			st = new SpriteState ();
			st.highlightedSprite = Resources.Load<Sprite> ("Textures/10HL");
			objects [1, 0].GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Textures/10Base");
			objects [1, 0].GetComponent<Button> ().spriteState = st;
			st = new SpriteState ();
			st.highlightedSprite = Resources.Load<Sprite> ("Textures/15HL");
			objects [0, 1].GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Textures/15Base");
			objects [0, 1].GetComponent<Button> ().spriteState = st;
			st = new SpriteState ();
			st.highlightedSprite = Resources.Load<Sprite> ("Textures/20HL");
			objects [1, 1].GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Textures/20Base");
			objects [1, 1].GetComponent<Button> ().spriteState = st;
		} else {			
			SpriteState st = new SpriteState ();
			st.highlightedSprite = Resources.Load<Sprite> ("Textures/25HL");
			objects [0, 0].GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Textures/25Base");
			objects [0, 0].GetComponent<Button> ().spriteState = st;
			st = new SpriteState ();
			st.highlightedSprite = Resources.Load<Sprite> ("Textures/50HL");
			objects [1, 0].GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Textures/50Base");
			objects [1, 0].GetComponent<Button> ().spriteState = st;
			st = new SpriteState ();
			st.highlightedSprite = Resources.Load<Sprite> ("Textures/100HL");
			objects [0, 1].GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Textures/100Base");
			objects [0, 1].GetComponent<Button> ().spriteState = st;
			st = new SpriteState ();
			st.highlightedSprite = Resources.Load<Sprite> ("Textures/150HL");
			objects [1, 1].GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Textures/150Base");
			objects [1, 1].GetComponent<Button> ().spriteState = st;
		}
	}

	public void GoToNewYork ()
	{
		Application.LoadLevel ("NewYork");
	}

	public void ManualPop ()
	{
		//Werkt niet. Dunno why.
		menus.Peek ().Done = true;
	}
	void CreateEnd ()
	{
		GameObject[,] objects = new GameObject[2, 1];		
		objects [0, 0] = Instantiate (Resources.Load<GameObject> ("Prefabs/ChoiceButton")) as GameObject;		
		objects [1, 0] = Instantiate (Resources.Load<GameObject> ("Prefabs/ChoiceButton")) as GameObject;
		SpriteState st = new SpriteState ();		
		st.highlightedSprite = Resources.Load<Sprite> ("Textures/YesHL");		
		objects [0, 0].GetComponent<Button> ().spriteState = st;
		objects [0, 0].GetComponent<Image> ().sprite = Resources.Load <Sprite> ("Textures/YesBase");		
		st = new SpriteState ();	
		st.highlightedSprite = Resources.Load<Sprite> ("Textures/NoHL");
		objects [1, 0].GetComponent<Button> ().spriteState = st;
		objects [1, 0].GetComponent<Image> ().sprite = Resources.Load <Sprite> ("Textures/NoBase");
		objects [0, 0].GetComponent<Button> ().onClick.AddListener (GoToNewYork);
		objects [0, 0].GetComponent<Button> ().onClick.AddListener (() => ManualPop ());		
		Master master = GameObject.Find ("Master").GetComponent<Master> ();
		objects [0, 0].GetComponentInChildren <Text> ().text = "Start a game of " + (master.timer / 60).ToString () + "minutes. Required objectives: " +
			master.pointsToWin.ToString () + ". The teams will start with " + master.handicap.ToString () + " points. Is this okay?";		
		objects [1, 0].GetComponentInChildren <Text> ().text = "Start a game of " + (master.timer / 60).ToString () + "minutes. Required objectives: " +
			master.pointsToWin.ToString () + ". The teams will start with " + master.handicap.ToString () + " points. Is this okay?";		
		objects [0, 0].transform.SetParent (this.transform, false);
		objects [1, 0].transform.SetParent (this.transform, false);
		menus.Peek ().Suspend ();
		menus.Push (new ChoiceMenu (objects));
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
			CreateEnd ();
			break;
		default:
			break;
		}
	}
}
