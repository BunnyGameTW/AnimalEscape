using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class btnlock : MonoBehaviour {
    float num = 1;
    float times = 0;
    public GameObject door_root;
    public AudioClip wrongSE, BingoSE;
    public AudioClip btnPressSE;
    AudioSource audio;
	// Use this for initialization
	void Start () {
        audio = GetComponent<AudioSource>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LeftBtnClicked()
    {
        num *= 3;
        times++;
        audio.PlayOneShot(btnPressSE);
    }
    public void RightBtnClicked()
    {
        num -= 2;
        times++;
        audio.PlayOneShot(btnPressSE);
    }
    public void EnterBtnClicked()
    {
        if (times == 5)
        {
            if (num == 15)
            {
               Animator ani = door_root.GetComponent<Animator>();

                Debug.Log("yeahhh");
                ani.SetBool("isWin", true);
                audio.PlayOneShot(BingoSE);
            }
            else
            {
                WrongAns();
            }
        }
        else // false
        {
            WrongAns();
        }
    }

    void WrongAns()
    {
        times = 0;
        num = 1;
        Debug.Log("wrong ans");
        audio.PlayOneShot(wrongSE);

    }
}
