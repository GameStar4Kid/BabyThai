using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class UIManagePhoneDetailScript : MonoBehaviour {
	public AudioClip frog;
	public AudioClip horse;
	public AudioClip sheep;
	public AudioClip cow;
	public AudioClip duck;
	public AudioClip turkey;
	public AudioClip coat;
	public AudioClip chicken;
	public AudioClip dog;
	public Text text; 
	public Button button; 
	public Image image; 
		//songs
	public AudioClip[] clips;
	public string[] strings;
	public string[] images2;
	public AudioSource source;
	private bool isStop=false;
	private bool startMusic=false;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (!startMusic) {
			startMusic=true;
			int index = Random.Range (0, strings.Length - 1);
			AudioClip clip = (AudioClip)Resources.Load("sounds/"+strings[index]);
			source.PlayOneShot(clip);
//			text.text = strings[index];
			button.image.sprite=Resources.Load <Sprite>("images/"+images2[index]);
			//		image.name = images [index];
			Sprite newSprite =  Resources.Load <Sprite>(images2[index]);
			Debug.Log ("load "+images2[index]);
			if (newSprite){
				image.sprite = newSprite;
			} else {
				Debug.LogError("Sprite not found", this);
			}
		}
		if (Input.GetKeyDown(KeyCode.Escape)) {
			source.Stop ();
			isStop=true;
		}
		if (isStop == true) {
			if(Application.CanStreamedLevelBeLoaded(1)){
				isStop=false;
				Application.LoadLevelAdditive(1);
			}
		}
	}
	public void btnEndClicked()
	{
		source.Stop ();
		Handheld.Vibrate();
		isStop = true;
//		Application.LoadLevel("Phone");
	}
	public void btnDetail1Clicked()
	{
		source.PlayOneShot (cow);
		Handheld.Vibrate();
	}
	public void btnDetail2Clicked()
	{
		source.PlayOneShot (dog);
		Handheld.Vibrate();
	}
	public void btnDetail3Clicked()
	{
		source.PlayOneShot (duck);
		Handheld.Vibrate();
	}
	public void btnDetail4Clicked()
	{
		source.PlayOneShot (coat);
		Handheld.Vibrate();
	}
	public void btnDetail5Clicked()
	{
		source.PlayOneShot (horse);
		Handheld.Vibrate();
	}
	public void btnDetail6Clicked()
	{
		source.PlayOneShot (chicken);
		Handheld.Vibrate();
	}
	public void btnDetail7Clicked()
	{
		source.PlayOneShot (sheep);
		Handheld.Vibrate();
	}
	public void btnDetail8Clicked()
	{
		source.PlayOneShot (frog);
		Handheld.Vibrate();
	}
	public void btnDetail9Clicked()
	{
		source.PlayOneShot (turkey);
		Handheld.Vibrate();
	}
}
