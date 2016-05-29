using UnityEngine;
using System.Collections;
public class UIPhoneManagerScript : MonoBehaviour {
	public Animator btn1;
	public Animator btn2;
	public Animator btn3;
	public Animator btn4;
	public Animator btn5;
	public Animator btn6;
	public Animator btn7;
	public Animator btn8;
	public Animator btn9;
	public Animator btn_custom1;
	public Animator btn_custom2;
	public Animator btn_custom3;
	public AudioClip[] clips;
	public AudioClip callRing;
	// Use this for initialization
	public float timeRemaining = 60f;

	public AudioSource source;
	private bool playNow = false;
	private bool playCall = false;
	private int playCallFinish = 0;
	private int cnt =0;
	private int scene =1;
	private bool isLoaded =false;
	void Start () {
		InvokeRepeating ("increaseTimeRemaining", 1.0f, 1.0f);
//		RequestBanner ();
		UM_AdManager.instance.CreateAdBanner(TextAnchor.UpperCenter);
		isLoaded = false;
	}
	void Awake () {
		Debug.Log ("Awake UIPhoneManage");
		UM_AdManager.instance.Init ();
	}
	// Update is called once per frame
	void Update () {
		if (!isLoaded &&Input.GetKeyDown(KeyCode.Escape)) {
			Application.Quit();
			return;
		}
		if (timeRemaining % 3 == 0)
		{
			wiggleButton();
		}
		if (playNow == true) {
			PlaySounds();
		}
		if(!isLoaded && Application.loadedLevel != scene){
			
			if (Application.CanStreamedLevelBeLoaded(scene)) {
				
				Application.LoadLevelAdditiveAsync (scene);
				
			}
			isLoaded=true;
		}
	}
	void PlaySounds(){
		if(!source.isPlaying && cnt < clips.Length){
			int index = Random.Range(0,8);
			source.clip = clips[index];
			wiggleBtn(index);
			source.Play();
			cnt = cnt + 1;
		}
		if (cnt == clips.Length) {
			cnt = 0;
			playNow=false;
			btnCallClicked();
		}
	}
	void increaseTimeRemaining()
	{
		timeRemaining ++;
	}
	public void btnCallClicked()
	{
		scene = 2;
		source.PlayOneShot(callRing);
//		AsyncOperation async= Application.LoadLevelAdditiveAsync ("PhoneDetail");
//		yield async;
//		playCall = true;
//		source.clip = callRing;
//		source.Play ();

	}
	public void wiggleButton()
	{
		for (int i=0; i<9; i++) {
//			wiggleBtn(i);
		}
	}
	private void wiggleBtn(int i){
		if (i == 0) {
			bool isHidden = btn1.GetBool ("isPlay");
			btn1.SetBool ("isPlay", !isHidden);
		} else if (i == 1) {
			bool isHidden = btn2.GetBool ("isPlay");
			btn2.SetBool ("isPlay", !isHidden);
		} else if (i == 2) {
			bool isHidden = btn3.GetBool ("isPlay");
			btn3.SetBool ("isPlay", !isHidden);
		} else if (i == 3) {
			bool isHidden = btn4.GetBool ("isPlay");
			btn4.SetBool ("isPlay", !isHidden);
		} else if (i == 4) {
			bool isHidden = btn5.GetBool ("isPlay");
			btn5.SetBool ("isPlay", !isHidden);
		} else if (i == 5) {
			bool isHidden = btn6.GetBool ("isPlay");
			btn6.SetBool ("isPlay", !isHidden);
		} else if (i == 6) {
			bool isHidden = btn7.GetBool ("isPlay");
			btn7.SetBool ("isPlay", !isHidden);
		} else if (i == 7) {
			bool isHidden = btn8.GetBool ("isPlay");
			btn8.SetBool ("isPlay", !isHidden);
		} else if (i == 8) {
			bool isHidden = btn9.GetBool ("isPlay");
			btn9.SetBool ("isPlay", !isHidden);
		} else if (i == 9) {
			bool isHidden = btn_custom1.GetBool ("isPlay");
			btn_custom1.SetBool ("isPlay", !isHidden);
		} else if (i == 10) {
			bool isHidden = btn_custom2.GetBool ("isPlay");
			btn_custom2.SetBool ("isPlay", !isHidden);
		} else if (i == 11) {
			bool isHidden = btn_custom3.GetBool ("isPlay");
			btn_custom3.SetBool ("isPlay", !isHidden);
		}
	}
	public void btn2Clicked()
	{
		source.PlayOneShot (clips[1]);
		Handheld.Vibrate();
	}
	public void btn3Clicked()
	{
		source.PlayOneShot (clips[2]);
		Handheld.Vibrate();
	}
	public void btn4Clicked()
	{
		source.PlayOneShot (clips[3]);
		Handheld.Vibrate();
	}
	public void btn5Clicked()
	{
		source.PlayOneShot (clips[4]);
		Handheld.Vibrate();
	}
	public void btn6Clicked()
	{
		source.PlayOneShot (clips[5]);
		Handheld.Vibrate();
	}
	public void btn7Clicked()
	{
		source.PlayOneShot (clips[6]);
		Handheld.Vibrate();
	}
	public void btn8Clicked()
	{
		source.PlayOneShot (clips[7]);
		Handheld.Vibrate();
	}
	public void btn9Clicked()
	{
		source.PlayOneShot (clips[8]);
		Handheld.Vibrate();
	}
	public void btn1Clicked()
	{
		source.volume = 1;
		source.PlayOneShot (clips[0]);
		Handheld.Vibrate();
	}
	public void btnCustom1Clicked()
	{
		playNow = true;
		Handheld.Vibrate();
	}
	public void btnCustom2Clicked()
	{
		playNow = true;
		Handheld.Vibrate();
	}
	public void btnCustom3Clicked()
	{
		playNow = true;
		Handheld.Vibrate();
	}

}
