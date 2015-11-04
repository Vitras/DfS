using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DisconnectButtonScript : MonoBehaviour
{

	[SerializeField]
	private Button disconnect = null;

	// Use this for initialization
	void Start ()
	{
		disconnect.onClick.AddListener (() => {
			Disconnect ();});
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public void Disconnect ()
	{
		NetworkScript nwm = GameObject.Find("NetworkManager").GetComponent<NetworkScript>();
		nwm.client.Disconnect();
		Application.LoadLevel("Main");
		Destroy(nwm.transform.gameObject);
	}
}

