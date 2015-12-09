using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ReactionGameControllerScript : MonoBehaviour {
	
	public GameObject pudding;
	public GameObject SpawnPoint;
	public List<GameObject> spawnedPuddings;
	public GameObject Piston;
	public Sprite puddingMonster;
	public Text scoreBox;
	public Text hitIndicator;
	public GameObject panel;

	//audio stuff
	private AudioSource source;
	public AudioClip pistonSfx;
	public AudioClip pistonMissedSfx;

	// Use this for initialization
	void Start () 
	{
		InvokeRepeating("EverySecond",0,1.0f);
		spawnedPuddings = new List<GameObject>();
		//
	}

	void Awake () 
	{
		source = GetComponent<AudioSource>();
	}
	
	private void EverySecond()
	{
		int random = Random.Range(0,3);
		if (random == 2)
			return;
		GameObject onePudding = (GameObject)Instantiate(pudding,SpawnPoint.transform.position,transform.rotation);
		onePudding.GetComponent<PuddingScript>().transform.SetParent(panel.transform,true);

		spawnedPuddings.Add(onePudding);


	}
	
	// Update is called once per frame
	void Update () 
	{
		foreach(GameObject p in spawnedPuddings)
		{
			Vector3 pos = p.transform.position;
			pos.x += 3;
			p.transform.position = pos;
		}

		Color hitIndicatorColor = hitIndicator.color;
		float hitIndicatorAlpha = hitIndicatorColor.a;
		hitIndicatorAlpha -= 0.01f;
		Color colorDecrement = new Color(hitIndicatorColor.r,hitIndicatorColor.g, hitIndicatorColor.b,hitIndicatorAlpha);
		if(hitIndicator.color.a > 0.0f)
		{
			hitIndicator.color = colorDecrement;
		}

	}
	

	IEnumerator TransformPuddingDelay(GameObject p)
	{
		yield return new WaitForSeconds(0.4f);
		p.GetComponent<Image>().sprite = puddingMonster;
	}
	

	IEnumerator ChangeHitIndicatorText(int i)
	{
		yield return new WaitForSeconds(0.4f);
		switch(i)
		{
		case 0:
			NetworkScript.instance.Points += 10;
			hitIndicator.color = Color.cyan;
			hitIndicator.text = "Perfect! +10";
			break;
		case 1:
			NetworkScript.instance.Points += 5;
			hitIndicator.color = Color.green;
			hitIndicator.text = "Good! +5";
			break;
		default:
			NetworkScript.instance.Points -= 5;
			hitIndicator.color = Color.red;
			hitIndicator.text = "Miss! -5";
			break;
		}
	}
	


	public void	ActivatePiston()
	{
		bool missed = false;
		Piston.GetComponent<Animator>().Play(0);
		foreach(GameObject p in spawnedPuddings)
		{
			PuddingScript script = p.GetComponent<PuddingScript>();
			if (p.transform.localPosition.x >= 100 && p.transform.localPosition.x <= 125 && script.success == false)
			{
				//Debug.Log("got 10 pts for hitting " + spawnedPuddings.IndexOf(p).ToString() + " perfectly");
				StartCoroutine(TransformPuddingDelay(p));
				script.success = true;
				StartCoroutine(ChangeHitIndicatorText(0));
				source.PlayOneShot(pistonSfx);
				return;
			}
			else if(p.transform.localPosition.x >= 75 && p.transform.localPosition.x <= 175 && script.success == false)
			{
				//Debug.Log("got 5 pts for hitting " + spawnedPuddings.IndexOf(p).ToString());
				StartCoroutine(TransformPuddingDelay(p));
				script.success = true;
				StartCoroutine(ChangeHitIndicatorText(1));
				source.PlayOneShot(pistonSfx);
				return;
			}
			else
			{
				//Debug.Log("potential miss detected");
				missed = true;
			}
		}
		if (missed)
		{
			//Debug.Log("Deducted 5 points for not hitting anything!");
			StartCoroutine(ChangeHitIndicatorText(2));
			source.PlayOneShot(pistonMissedSfx);
			return;
		}

		NetworkScript.instance.points -= 5;
		//Debug.Log("5 points deducted for using the piston when there was nothing to hit");
		StartCoroutine(ChangeHitIndicatorText(2));
		source.PlayOneShot(pistonMissedSfx);
	}
}
