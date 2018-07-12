using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeetleManager : MonoBehaviour {
    
    private string m_Tag = "Beetle";
    public int currentBeetleCount = 0;
    Text beetleTextCount;
    public GameObject[] beetles;

    // Use this for initialization
    void Start()
    {
        beetleTextCount = GetComponent<Text>();
        currentBeetleCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        beetles = GameObject.FindGameObjectsWithTag(m_Tag);
        currentBeetleCount = beetles.Length;
        beetleTextCount.text = currentBeetleCount.ToString();
    }
}
