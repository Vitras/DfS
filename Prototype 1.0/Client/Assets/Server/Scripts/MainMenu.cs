using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{

	private enum MenuState
	{
		None,
		Button,
		Options}
	;
	private MenuState state;
	private float startTimer;
	public bool fadeIn;
	private float fadeSpeed;
	public GameObject optionsArray;
	public GameObject buttonIndicator;
	// Use this for initialization

	void Start ()
	{
		state = MenuState.None;
		fadeSpeed = 0.8f;
		startTimer = 1.5f;		
		optionsArray.GetComponent<CanvasGroup> ().alpha = 0;
		optionsArray.SetActive (false);
		buttonIndicator.SetActive (false);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (state == MenuState.None) {
			startTimer -= Time.deltaTime;
			if (startTimer < 0) {
				state = MenuState.Options;
				//buttonIndicator.SetActive (true);
				fadeIn = true;
				optionsArray.GetComponent<CanvasGroup> ().alpha = 0;
				optionsArray.SetActive (true);				
				EventSystem.current.SetSelectedGameObject (optionsArray.transform.GetChild (0).gameObject);
			}
		} else if (state == MenuState.Button) {
			if (Input.anyKeyDown) {
				state = MenuState.Options;
				optionsArray.SetActive (true);
				buttonIndicator.SetActive (false);
				EventSystem.current.SetSelectedGameObject (optionsArray.transform.GetChild (0).gameObject);
			}
		} else {
			if (fadeIn) {					
				optionsArray.GetComponent<CanvasGroup> ().alpha += fadeSpeed * Time.deltaTime;
				fadeIn = !(optionsArray.GetComponent<CanvasGroup> ().alpha == 1);
				
			} else {					
				optionsArray.GetComponent<CanvasGroup> ().alpha -= fadeSpeed * Time.deltaTime;
				fadeIn = optionsArray.GetComponent<CanvasGroup> ().alpha == 0;
			}
//			if(Input.GetKeyDown(KeyCode.JoystickButton3) || Input.GetKeyDown (KeyCode.Backspace))
//			{
//				state = MenuState.Button;
//				optionsArray.SetActive (false);				
//				buttonIndicator.SetActive(true);
//			}
		}
	}	
	
	public void GoToOptions ()
	{
		Application.LoadLevel ("Options");
	}	
	
	public void StartGame ()
	{
		Application.LoadLevel ("GameSettings");
	}
}
