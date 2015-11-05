using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class IdleGameControllerScript : MonoBehaviour {

	private int currency;
	public Text currencyCounter;
	public GameObject collectable;

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
		int random = Random.Range(0,11);
		if(random == 10)
		{
			GameObject powerUp = Instantiate<GameObject>(collectable);
			powerUp.transform.position = this.transform.position;
		}
	}


	// Update is called once per frame
	void Update () {

	}


	public int Currency
	{
		get {return currency;}
		set
		{
			currency = value;
			currencyCounter.text = currency.ToString();
		}
	}
}
