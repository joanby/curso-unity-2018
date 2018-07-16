using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CucumberManager : MonoBehaviour {

    private string m_Tag = "Cucumber";
    public static int currentCucumberCount = 0;
    Text cucumberTextCount;
    public GameObject[] cucumbers;

	// Use this for initialization
	void Awake () {
        cucumberTextCount = GetComponent<Text>();
        currentCucumberCount = 1;
	}
	
	// Update is called once per frame
	void Update () {
        cucumbers = GameObject.FindGameObjectsWithTag(m_Tag);
        currentCucumberCount = cucumbers.Length;
        cucumberTextCount.text = currentCucumberCount.ToString();
	}
}
