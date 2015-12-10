using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SplashScreen : MonoBehaviour
{

	Sprite[] splash;
	Image image;
	int imageCounter;
	bool done;
	float frameTime, currentTime;
	// Use this for initialization
	void Start ()
	{
		imageCounter = 0;
		currentTime = 0;
		frameTime = 1 / 24f;
		done = false;
		splash = new Sprite[90];
		image = GetComponent<Image> ();
		FillSplashAnimation();
		image.sprite = splash [0];
	}

	void FillSplashAnimation()
	{
		for(int x = 1; x <= splash.Length; ++x)
		{
			Debug.Log (x);
			splash[x-1] = Resources.Load<Sprite>("AnimationTexture/Splash/Logo" + x.ToString());
		}
	}

	// Update is called once per frame
	void Update ()
	{
		currentTime += Time.deltaTime;
		if (!done && currentTime > frameTime) {
			currentTime = 0;
			imageCounter++;
			if (imageCounter == splash.Length)
			{
				done = true;
				Application.LoadLevel ("MainMenu");
			}
			else
				image.sprite = splash [imageCounter];
		}
	}
}
