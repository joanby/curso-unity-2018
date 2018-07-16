using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CherryManager : MonoBehaviour {
    

    Text cherriesTextCount;

    // Use this for initialization
    void Start()
    {
        cherriesTextCount = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        cherriesTextCount.text = PlayerManager.currentCherryCount.ToString();
    }
}
