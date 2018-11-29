using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIManager : MonoBehaviour {

    public GameObject inventoryPanel;

    public Transform inventoryPanelItem;
    public GameObject inventoryItemElement;


    // Use this for initialization
    void Start () {

        inventoryPanel.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (GameMaster.sharedInstance.currentScene.name != SceneName.mainMenu &
            GameMaster.sharedInstance.currentScene.name != SceneName.characterSelection)
        {
            if (Input.GetKeyUp(KeyCode.I))
            {
                inventoryPanel.SetActive(!inventoryPanel.activeInHierarchy);
                ShowWeapons();
            }
        }


    }

    private List<InventoryItem> selectedItems;

    public void ShowWeapons(){
        selectedItems = GameMaster.sharedInstance.inventory.Weapons;
        RefreshUI();
    }

    public void ShowArmors(){
        selectedItems = GameMaster.sharedInstance.inventory.Armour;
        RefreshUI();
    }

    public void ShowClothing(){
        selectedItems = GameMaster.sharedInstance.inventory.Clothing;
        RefreshUI();
    }

    public void ShowHealth(){
        selectedItems = GameMaster.sharedInstance.inventory.Health;
        RefreshUI();
    }

    public void ShowPotions(){
        selectedItems = GameMaster.sharedInstance.inventory.Potion;
        RefreshUI();
    }

    void RefreshUI(){
        //Esta parte elimina cualquier celda que se estuviese viendo en ese momento...
        foreach(Transform child in inventoryPanelItem.transform){
            Destroy(child.gameObject);
        }


        //Muestro la categoría que el usuario desea ver...
        foreach(InventoryItem item in selectedItems){
            GameObject newButton = Instantiate(inventoryItemElement) as GameObject;
            InventoryItemUI text = newButton.GetComponent<InventoryItemUI>();
            text.textItemElement.text = item.Name + "\n" + item.Description;


            //Triggers de buttons
            text.addButton.GetComponent<Button>().onClick.AddListener(() =>
            {
                //Puedo proceder a equiparle una arma
                if (item.Category == BaseItem.ItemCategory.Weapon
                    ||item.Category == BaseItem.ItemCategory.Armour
                    ||item.Category == BaseItem.ItemCategory.Clothing){

                    if (!GameMaster.sharedInstance.inventory.selectedWeapons.Contains(item))
                    {
                        GameMaster.sharedInstance.inventory.selectedWeapons.Add(item);
                    }
                } 

                if(item.Category == BaseItem.ItemCategory.Potion 
                   || item.Category == BaseItem.ItemCategory.Health){
                    GameMaster.sharedInstance.inventory.selectedItems.Add(item);
                }

                GameMaster.sharedInstance.ForceReloadUI();

            });


            text.deleteButton.GetComponent<Button>().onClick.AddListener(() =>
            {
                Debug.Log(string.Format("Quiero destruir el objeto {0}",
                                        text.textItemElement.text));

                GameMaster.sharedInstance.inventory.RemoveItem(item);

                Destroy(newButton);
            });

            newButton.transform.SetParent(inventoryPanelItem);
        }

    }
}
