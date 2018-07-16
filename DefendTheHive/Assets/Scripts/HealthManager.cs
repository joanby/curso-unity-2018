using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthManager : MonoBehaviour {

    private Slider healthBar;
    public static int currentHealth;


	private void Awake()
	{
        healthBar = GetComponent<Slider>();
        currentHealth = 100;
	}

    public void ReduceHealth(){
        currentHealth -= 30;
        healthBar.value = currentHealth;
    }

	private void Update()
	{
        healthBar.value = currentHealth;
	}

}
