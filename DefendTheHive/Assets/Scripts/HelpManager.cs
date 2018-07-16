using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpManager : MonoBehaviour {

    Canvas helpCanvas;
	private void Awake()
	{
        helpCanvas = GetComponent<Canvas>();
	}
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.H))
        {
            helpCanvas.enabled = !helpCanvas.enabled;

            if(helpCanvas.enabled){
                Time.timeScale = 0f;
            } else {
                Time.timeScale = 1f;
            }

         }
	}
}
