using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PipeGame : MonoBehaviour
{

	/*
	 * 830/3
276 * 2 voor speelveld, 92 hoog voor segment, 106 breed voor segmemnt
276 voor 6 knoppen
138 hoog voor segment, 213 breed voor segment 
	 */

	private PipeField field;
	private int[] PipeAmount;

	void Start ()
	{
		field = new PipeField (6, 6, 6);
		field.startPos = Vector2.zero;
		for (int x = 0; x < 6/*field.field.Length*/; x++) {
			GameObject button = Instantiate<GameObject> (Resources.Load<GameObject> ("Prefabs/PuzzleGame/Horizontal"));			
			button.transform.position += new Vector3 (x  % 3 * 213, x / 3 * -138, 0);
			button.transform.SetParent (transform, false);
			button.GetComponent<Button> ().onClick.AddListener (() => Highlight ("Horizontal"));
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
	}

	public void Highlight (string name)
	{
		Debug.Log ("derp");
	}
}
