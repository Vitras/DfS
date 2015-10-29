using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class IdleGameControllerScript : MonoBehaviour {

	private int currency;
	public Text currencyCounter;

	// Use this for initialization
	void Start () 
	{
		currency = 100;
		InvokeRepeating("EverySecond",0,1);

	}

	void EverySecond()
	{
		currency += 1;
		currencyCounter.text = currency.ToString();
	}


	// Update is called once per frame
	void Update () {

	}
}
