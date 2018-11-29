using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCStatusUI : MonoBehaviour {

    public Slider healthBar;

    private void Start()
    {
        healthBar.value = GetComponent<NPCAgent>().npcData.health;
    }

    private void Update()
    {
        healthBar.value = GetComponent<NPCAgent>().npcData.health;

    }

}
