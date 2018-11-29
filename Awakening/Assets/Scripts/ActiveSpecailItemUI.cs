using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ActiveSpecailItemUI : EventTrigger {

    public override void OnPointerClick(PointerEventData eventData)
    {
    
        InventoryItem item = gameObject.GetComponent<PanelWeaponsItemUI>().item;
        switch(item.Category){
            case BaseItem.ItemCategory.Health:
                //Añadir un objeto al menú lateral...
                Destroy(gameObject);
                break;

            case BaseItem.ItemCategory.Potion:
                //bla bla bla...
                Destroy(gameObject);
                break;

            case BaseItem.ItemCategory.Weapon:
                GameObject player = GameMaster.sharedInstance.player;

                if (player.GetComponent<BarbarianCharacterController>() != null)
                {
                    foreach (GameObject weapon in player.GetComponent<BarbarianCharacterController>().weapons)
                    {
                        weapon.SetActive(weapon.GetComponent<InventoryItemAgent>().item.Name.Equals(item.Name));
                    }
                }
                break;
        }
    }

}
