using UnityEngine;
using System.Collections;

//有複數音效要觸發播放且需要連續播放效果

//放上腳本自動添加音頻播放器
[RequireComponent (typeof (AudioSource))]
public class play1 : MonoBehaviour {

	AudioSource audio;

	public AudioClip clip1;//音效1
	public AudioClip clip2;//音效2

	// Use this for initialization
	void Start () {
	
		audio = GetComponent<AudioSource> ();

		audio.playOnAwake = false;
		audio.loop = true;
		audio.Stop ();
	}

	void OnMouseOver () {//當滑鼠進入物件

		audio.clip = clip1;//告訴程式現在是播放音效1

		if (!audio.isPlaying)//使音效正常播放，避免產生無限播放形成破音的效果
			audio.Play();
	}
	
	void OnMouseExit  (){//當滑鼠離開物件

		audio.clip = clip2;//告訴程式現在是播放音效2

		if (!audio.isPlaying)
			audio.Play(); 
		
	}
}
