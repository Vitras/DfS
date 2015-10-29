using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ColorSwapButtonScript : MonoBehaviour 
{
	[SerializeField] private Button csb = null;

	void Start() 
	{ 
		csb.onClick.AddListener(() => {ColorSwapCommand();});
	}

	public void ColorSwapCommand()
	{
		GameObject.Find("ConnectionManager").GetComponent<Client>().SendColorSwapCommandToServer();
	}

}
