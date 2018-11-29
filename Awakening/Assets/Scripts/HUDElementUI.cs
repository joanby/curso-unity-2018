using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDElementUI : MonoBehaviour {

    public Slider healthBar, manaBar, energyBar;

    public GameObject activeInventoyItem, activeSpecialItem;

    public Transform panelActiveInventoryItem, panelActiveSpecialItem;

    public void ReloadUI(){

        EmptyUI();

        foreach(InventoryItem item in GameMaster.sharedInstance.inventory.selectedWeapons){
            GameObject newItem = Instantiate(activeInventoyItem, panelActiveInventoryItem);
            newItem.GetComponent<PanelWeaponsItemUI>().item = item;
            newItem.GetComponent<PanelWeaponsItemUI>().textItem.text = item.Name.Substring(0,3).ToUpper();
        }
        //Faltaría el otro menú, el panel active special item...
    }

    public void EmptyUI(){

        foreach(Transform child in panelActiveInventoryItem){
            Destroy(child.gameObject);
        }
        //Faltaría el otro menú
    }

}
