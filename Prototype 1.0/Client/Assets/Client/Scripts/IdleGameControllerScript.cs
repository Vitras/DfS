using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class IdleGameControllerScript : MonoBehaviour {
	
	public Text currencyCounter;
	public GameObject collectable;
	private GameObject networkManager;
	private NetworkScript network;

	// Use this for initialization
	void Start () 
	{
		InvokeRepeating("EverySecond",0,1);
		networkManager = GameObject.Find("NetworkManager");
		network = networkManager.GetComponent<NetworkScript>();
	}

	void EverySecond()
	{
		network.points += 1;
		currencyCounter.text = network.points.ToString();
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

	public void GoToReactionGame()
	{
		Application.LoadLevel("ReactionGame");
	}

	public void GoToMinimap()
	{
		Application.LoadLevel("Minimap");
	}
	
	public int Currency
	{
		get {return network.points;}
		set
		{
			network.points = value;
			currencyCounter.text = network.points.ToString();
		}
	}
}
