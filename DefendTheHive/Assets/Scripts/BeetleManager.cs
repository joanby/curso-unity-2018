using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeetleManager : MonoBehaviour {
    
    private string m_Tag = "Beetle";
    public static int currentBeetleCount = 0;
    Text beetleTextCount;
    public GameObject[] beetles;

    // Use this for initialization
    void Awake()
    {
        beetleTextCount = GetComponent<Text>();
        currentBeetleCount = 1;
        RecalculateBeetles();
    }


    public void RecalculateBeetles(){
        beetles = GameObject.FindGameObjectsWithTag(m_Tag);
        currentBeetleCount = beetles.Length;
        beetleTextCount.text = currentBeetleCount.ToString();
    }
}
