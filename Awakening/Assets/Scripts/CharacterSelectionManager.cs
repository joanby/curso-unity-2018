using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectionManager : MonoBehaviour {

    public GameObject[] availableCharacters;
    private Transform spawnPoint;

    int characterId = 0;
    public GameObject selectedCharacter;

    private void Awake()
    {
        spawnPoint = GameObject.Find("SpawnPoint").transform;
        foreach(GameObject character in availableCharacters){

            character.GetComponentInChildren<Camera>().enabled = false;

            if(character.GetComponent<BarbarianCharacterController>()!=null){
                character.GetComponent<BarbarianCharacterController>().enabled = false;
            }

            if(character.GetComponent<DragonCharacterController>()!=null){
                character.GetComponent<DragonCharacterController>().enabled = false;
            }
        }
    }
    
    void SpawnCharacter(){
        if(selectedCharacter != null){
            Destroy(selectedCharacter);
        }
        selectedCharacter = Instantiate(availableCharacters[characterId], spawnPoint);
        selectedCharacter.transform.parent = null;
        BarbarianCharacterCustomization.sharedInstance.ToggleCanvas();
    }

    // Use this for initialization
    void Start () {
        SpawnCharacter();
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyUp(KeyCode.LeftArrow)){
            characterId = Mathf.Clamp(characterId-1, 0, availableCharacters.Length - 1);
            SpawnCharacter();
        }

        if(Input.GetKeyUp(KeyCode.RightArrow)){
            characterId = Mathf.Clamp(characterId + 1, 0, availableCharacters.Length - 1);
            SpawnCharacter();
        }

        if (Input.GetKey(KeyCode.UpArrow)){
            spawnPoint.transform.Rotate(new Vector3(0, 1, 0), 90.0f * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            spawnPoint.transform.Rotate(new Vector3(0, -1, 0), 90.0f * Time.deltaTime);
        }
    }
}
