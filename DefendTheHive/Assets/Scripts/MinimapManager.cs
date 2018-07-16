using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinimapManager : MonoBehaviour {

    RawImage minimap;

	private void Awake()
	{
        minimap = GetComponent<RawImage>();
	}

	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.M)){
            minimap.enabled = !minimap.enabled;
        }
	}
}
