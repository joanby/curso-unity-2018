using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager sharedInstance;
    public bool gameStarted = false;
    public bool gamePaused = false;

    public float invincibleTime = 0.0f;

    public AudioClip pauseAudio;

	// Use this for initialization
	void Awake () {
        if(sharedInstance == null){
            sharedInstance = this;
        }

        StartCoroutine("StartGame");
	}


    public void RestartGame(){

        SceneManager.LoadScene("MainMapScene");

    }
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.P)){
            gamePaused = !gamePaused;
            if(gamePaused){
                PlayPauseMusic();
            } else{
                StopPauseMusic();
            }
        }


        if(invincibleTime > 0){
            invincibleTime -= Time.deltaTime;
        }
	}

    void PlayPauseMusic(){
        AudioSource source = GetComponent<AudioSource>();
        source.clip = pauseAudio;
        source.loop = true;
        source.Play();
    }

    void StopPauseMusic(){
        GetComponent<AudioSource>().Stop();
    }


    IEnumerator StartGame(){
        yield return new WaitForSecondsRealtime(4.0f);
        gameStarted = true;
    }



    public void MakeInvincibleFor(float numberOfSeconds){
        this.invincibleTime += numberOfSeconds;
    }
}
