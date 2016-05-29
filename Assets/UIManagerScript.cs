using UnityEngine;
using System.Collections;
public class UIManagerScript : MonoBehaviour {
	public Animator startButton;
	public Animator settingsButton;
	private bool isLoaded=false;
	public void StartGame()
	{
		Debug.Log ("isPlay");
		Application.LoadLevel("Phone");
	}
	public void OpenSettings()
	{
		startButton.SetBool("isHidden", true);
		settingsButton.SetBool("isHidden", true);
	}
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			Application.Quit();
		}
//		if (isLoaded==false && Application.CanStreamedLevelBeLoaded(2)) {
//			Application.LoadLevelAdditive(2);
//			isLoaded=true;
//		}
	}
}
