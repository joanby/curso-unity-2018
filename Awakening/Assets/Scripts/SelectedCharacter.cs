using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCharacter : MonoBehaviour {
    public Light playerLight;

    void Start()
    {
        GameObject tempLight = GameObject.Find("Spot Light");
        if(tempLight!=null){
            playerLight = tempLight.GetComponent<Light>();
            playerLight.enabled = false;
        }
    }

    void OnMouseEnter()
    {
        if (playerLight != null)
        {
            playerLight.enabled = true;
        }
    }

    void OnMouseOver()
    {
      
    }

    void OnMouseExit()
    {
        if (playerLight != null)
        {
            playerLight.enabled = false;
        }
    }

    private void OnMouseDown()
    {
        /*GameMaster.sharedInstance.characterName = GameObject.Find("CharacterSelectionManager").
            GetComponent<CharacterSelectionManager>().
            selectedCharacter.name.Replace("(Clone)","");*/

        GameObject.Find("CharacterSelectionManager").
                  GetComponent<CharacterSelectionManager>().selectedCharacter.transform.parent = null;

        GameMaster.sharedInstance.player = GameObject.Find("CharacterSelectionManager").
            GetComponent<CharacterSelectionManager>().selectedCharacter;

        GameMaster.sharedInstance.LoadLevel(1);
    }
}
