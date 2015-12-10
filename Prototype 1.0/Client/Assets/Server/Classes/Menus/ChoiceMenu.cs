using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.UI;

public class ChoiceMenu : Menu
{
	public Text message;
	public GameObject[,] choices;
	// Use this for initialization
	public ChoiceMenu (GameObject[,] choices) : base()
	{
		message = GameObject.Find ("ChoiceMessage").GetComponent<Text> ();
		this.choices = choices;
		EventSystem.current.SetSelectedGameObject (choices [0, 0]);
		for (int x = 0; x < choices.GetLength (0); x++)
			for (int y = 0; y < choices.GetLength (1); y++) {
				choices [x, y].transform.position += new Vector3 (x * 400, y * -150, 0);
			}
	}
	
	// Update is called once per frame
	public override void Update ()
	{
		GameObject current = EventSystem.current.currentSelectedGameObject;
		if (current == null)
			EventSystem.current.SetSelectedGameObject (choices [0, 0]);
		message.text = current.GetComponentInChildren<Text> ().text;
	}

	public override void Suspend ()
	{
		foreach (GameObject g in choices) {
			g.SetActive (false);
		}
	}

	public override void Continue ()
	{
		foreach (GameObject g in choices) {
			g.SetActive (true);
		}
		EventSystem.current.SetSelectedGameObject (choices [0, 0]);
	}
	public override void Exit ()
	{
		for (int x = 0; x < choices.GetLength (0); x++)
			for (int y = 0; y < choices.GetLength (1); y++)
				GameObject.Destroy ((choices [x, y]));
	}
}
