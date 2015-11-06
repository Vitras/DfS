using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ReactionGameControllerScript : MonoBehaviour {

	public int points;
	public GameObject pudding;
	public GameObject SpawnPoint;
	public List<GameObject> spawnedPuddings;
	public GameObject Piston;
	public Sprite puddingMonster;
	public Text scoreBox;

	// Use this for initialization
	void Start () 
	{
		points = 0;
		InvokeRepeating("EverySecond",0,1);
		spawnedPuddings = new List<GameObject>();
	}

	private void EverySecond()
	{
		int random = Random.Range(0,3);
		if (random == 2)
			return;
		GameObject onePudding = (GameObject)Instantiate(pudding,SpawnPoint.transform.position,transform.rotation);
		onePudding.GetComponent<PuddingScript>().parent = this.gameObject;
		spawnedPuddings.Add(onePudding);


	}
	
	// Update is called once per frame
	void Update () 
	{
		foreach(GameObject p in spawnedPuddings)
		{
			Vector3 pos = p.transform.position;
			pos.x++;
			p.transform.position = pos;
		}
		if (points < 0)
			points = 0;
		scoreBox.text = points.ToString();

	}

	public void ReturnToIdle()
	{
		GameObject networkManager = GameObject.Find("NetworkManager");
		networkManager.GetComponent<NetworkScript>().points += points;
		Application.LoadLevel("InGame");
	}

	IEnumerator TransformPuddingDelay(GameObject p)
	{
		yield return new WaitForSeconds(0.4f);
		p.GetComponent<SpriteRenderer>().sprite = puddingMonster;
	}
	


	public void	ActivatePiston()
	{
		Piston.GetComponent<Animator>().Play(0);
		foreach(GameObject p in spawnedPuddings)
		{
			if (p.transform.position.x >= 31 && p.transform.position.x <= 35)
			{
				Debug.Log("got 10 pts");
				StartCoroutine(TransformPuddingDelay(p));
				p.GetComponent<PuddingScript>().success = true;
				points += 10;
			}
			else if(p.transform.position.x >= 27 && p.transform.position.x <= 43)
			{
				Debug.Log("got 5 pts");
				StartCoroutine(TransformPuddingDelay(p));
				p.GetComponent<PuddingScript>().success = true;
				points += 5;
			}
			else
			{
				Debug.Log("lost 2 pts");
				points -= 2;
			}
		}
	}
}
