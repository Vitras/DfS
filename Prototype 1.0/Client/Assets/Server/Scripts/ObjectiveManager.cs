using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectiveManager : MonoBehaviour
{
	private const int MAX_DIFFERENCE = 1;
	private Transform[,] objectives;
	public GameObject server;
	// Use this for initialization
	void Start ()
	{
		objectives = new Transform[6, 3];
		for (int x = 0; x < objectives.Length; x++) {
			objectives [x / 3, x % 3] = this.transform.GetChild (x);
			objectives [x / 3, x % 3].gameObject.SetActive (false);
		}
		SetNewObjectives (objectives [5, 1]);
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void Reset ()
	{
		for (int x = 0; x < objectives.GetLength (0); x++) 
			for (int y = 0; y < objectives.GetLength (1); y++) {
				objectives [x, y].gameObject.SetActive (false);
			}

	}

	public void SetNewObjectives (Transform objective)
	{
		if (objective.tag == "Objective A") 
			GameObject.Find ("EnvironmentManager").GetComponent<Environment> ().ScoreRed++;
		else 
			GameObject.Find ("EnvironmentManager").GetComponent<Environment> ().ScoreBlue++;
		Reset ();
		int number = int.Parse (objective.name.Split ('|') [1]) - 1;
		int x = number / 3;
		int y = number % 3;
		List<Transform> eligible = EligibleObjectives (x, y);
		int random = Random.Range (0, eligible.Count);
		int red = int.Parse (eligible [random].name.Split ('|') [1]);
		Transform obA = eligible [random];
		eligible.RemoveAt (random);
		random = Random.Range (0, eligible.Count);
		int blue = int.Parse (eligible [random].name.Split ('|') [1]);
		Transform obB = eligible [random];
		obA.gameObject.SetActive (true);
		obA.tag = "Objective A";
		obB.gameObject.SetActive (true);
		obB.tag = "Objective B";

		//server.GetComponent<Server>().SendObjectivesToAllClients(blue,red);
		//server.GetComponent<Server>().redObjective = red;
		//server.GetComponent<Server>().blueObjective = blue;
	}

	List<Transform> EligibleObjectives (int xPos, int yPos)
	{
		List<Transform> result = new List<Transform> ();
		int maxDistance = Random.Range (3, 7);
		for (int x = 0; x < objectives.GetLength (0); x++)
			for (int y = 0; y < objectives.GetLength (1); y++) {
				int distance = Mathf.Abs (xPos - x) + Mathf.Abs (yPos - y);
				if (distance <= maxDistance && distance >= maxDistance - MAX_DIFFERENCE) {
					result.Add (objectives [x, y]);
				}
			}
		return result;
	}
}
