using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemAgent : MonoBehaviour {

    public InventoryItem item;
    public bool hasBeenCollected = false;
    public bool shouldBeDestroyed = true;

   
    public void OnTriggerEnter(Collider other)
    {
    
        if (hasBeenCollected||!shouldBeDestroyed){
            return;        
        }
        //Si el objeto que ha colisionado con el ítem a ser recogido, es el Player...
        if(other.gameObject.tag.Equals("Player")){

            hasBeenCollected = true;
            //Hacemos una copia del ítem para enviarla al inventario
            //Y así poder destruir tranquilamente el que hay en escena
            InventoryItem collectedItem = new InventoryItem();
            collectedItem.CopyInventoryItem(item);
            GameMaster.sharedInstance.inventory.AddItem(collectedItem);
            GameMaster.sharedInstance.RpgDestroy(gameObject);
        }
    }

}
