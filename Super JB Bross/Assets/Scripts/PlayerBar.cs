using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum BarType{
    health,
    mana
}

public class PlayerBar : MonoBehaviour {

    private Slider slider;

    public BarType type;

	// Use this for initialization
	void Start () {
        this.slider = GetComponent<Slider>();

        switch(this.type){
            case BarType.health:
                this.slider.maxValue = PlayerController.MAX_HEALTH;
                break;
            case BarType.mana:
                this.slider.maxValue = PlayerController.MAX_MANA;
                break;
        }
	}
	
	// Update is called once per frame
	void Update () {
        switch(this.type){
            case BarType.health:
                this.slider.value = PlayerController.sharedInstance.GetHealth();
                break;
            case BarType.mana:
                this.slider.value = PlayerController.sharedInstance.GetMana();
                break;
        }

	}
}
