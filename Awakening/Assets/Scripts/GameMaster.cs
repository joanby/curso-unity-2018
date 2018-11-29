using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Librería para movernos entre escenas
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public static class SceneName{
    public const string mainMenu = "Main Menu";
    public const string characterSelection = "Character Selection";
    public const string level1 = "Awakening";

}

[RequireComponent(typeof(AudioSource))]
public class GameMaster : MonoBehaviour {

    public static GameMaster sharedInstance;

    public bool showOptions = false;
    public float musicVolume = 0f, sfxVolume = 0f;

    public GameObject player;
    public GameObject startPosition;

    public Scene currentScene;

    public string characterName = "Barbarian";

    public InventorySystem inventory;
    
   
    private void OnLevelWasLoaded(int level)
    {
        currentScene = SceneManager.GetActiveScene();

        startPosition = GameObject.Find("GameStart");

        if(player == null || currentScene.name.Equals(SceneName.mainMenu) 
           || currentScene.name.Equals(SceneName.characterSelection)){
            return;
        }

        player.GetComponent<SelectedCharacter>().enabled =
                  currentScene.name.Equals(SceneName.characterSelection);

        player.GetComponentInChildren<Camera>().enabled = true;

        if (player.GetComponent<BarbarianCharacterController>() != null)
        {
            player.GetComponent<BarbarianCharacterController>().enabled = true;
        }

        if (player.GetComponent<DragonCharacterController>() != null)
        {
            player.GetComponent<DragonCharacterController>().enabled = true;
        }

        player.transform.position = Vector3.zero;

        if (currentScene.name.Equals(SceneName.level1))
        {
            player.transform.position = startPosition.transform.position;
        }

        player.GetComponent<PlayerAgent>().LoadPlayerInfo();

        ForceReloadUI();
    }


    private void Awake()
    {
        if(sharedInstance == null){
            sharedInstance = this;

            inventory = new InventorySystem();

            InventoryItem tempItem = new InventoryItem();
            tempItem.Category = BaseItem.ItemCategory.Clothing;
            tempItem.Name = "Capa de la abuela";
            tempItem.Description = "Capa que perteneció a tu abuela antes de fallecer";
            tempItem.Strength = 0.5f;
            tempItem.Weight = 0.1f;

            inventory.AddItem(tempItem);

        }else if(sharedInstance != this){
            //Si me meto aquí, yo soy un segundo game master
            //diferente al original, al bueno, al sharedInstance
            Destroy(this);
        }

        MasterVolume(sharedInstance.GetComponent<AudioSource>().volume);

        DontDestroyOnLoad(this);

    }

    public void StartGame(){
        SceneManager.LoadScene(SceneName.characterSelection);
    }

    public void LoadLevel(int id){
        switch (id)
        {
            case 1:
                SceneManager.LoadScene(SceneName.level1);
                break;
        }
    }

    public void MasterVolume(float newVolume){
        musicVolume = newVolume;
        sharedInstance.GetComponent<AudioSource>().volume = musicVolume;
    }

    public void SfxVolume(float newVolume){
        sfxVolume = newVolume;
    }

	
	


    public void RpgDestroy(GameObject obj){
        Destroy(obj);
    }

    public void ForceReloadUI(){       
        GameObject canvasHUD = GameObject.Find("CanvasHUD");
        canvasHUD.GetComponent<HUDElementUI>().ReloadUI();
    }

   
}
