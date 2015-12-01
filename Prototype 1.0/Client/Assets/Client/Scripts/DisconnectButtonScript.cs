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
		NetworkScript nwm = NetworkScript.instance;
		var msg = new Messages.ClientDisconnectMessage();
		msg.ip = nwm.myIp;
		msg.id = nwm.playerId;
		nwm.client.Send(Messages.clientDisconnectMessageId,msg);
		StartCoroutine(DisconnectDelay(nwm));
		Application.LoadLevel("Main");
		Destroy(nwm.transform.gameObject);
	}

	IEnumerator DisconnectDelay(NetworkScript nwm)
	{
		yield return new WaitForSeconds(0.5f);
		nwm.client.Disconnect();
	}
}

