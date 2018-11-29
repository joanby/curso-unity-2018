using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MenuManager : MonoBehaviour {

    public RectTransform optionsPanel;
    public Slider mainVolumeSlider;
    public Slider sfxVolumeSlider;

   

	// Use this for initialization
	void Start () {
        mainVolumeSlider.value = GameMaster.sharedInstance.musicVolume;
        sfxVolumeSlider.value = GameMaster.sharedInstance.sfxVolume;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartGame(){
        GameMaster.sharedInstance.StartGame();
    }

    public void ShowOptions(){
        ToggleOptions();
    }

    public void ExitGame(){
    
    }

    public void ExitOptions(){
        ToggleOptions();
    }

    void ToggleOptions(){
        GameMaster.sharedInstance.showOptions = !GameMaster.sharedInstance.showOptions;
        optionsPanel.gameObject.SetActive(GameMaster.sharedInstance.showOptions);
    }

    public void MainVolume(){
        GameMaster.sharedInstance.MasterVolume(mainVolumeSlider.value);
    }
    public void SfxVolume(){
        GameMaster.sharedInstance.SfxVolume(sfxVolumeSlider.value);
    }
}
