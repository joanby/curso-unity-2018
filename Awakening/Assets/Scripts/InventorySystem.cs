using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventorySystem {

    [SerializeField]
    private List<InventoryItem> weapons = new List<InventoryItem>();
    public List<InventoryItem> Weapons{
        get { return weapons; }
    }

    [SerializeField]
    private List<InventoryItem> armour = new List<InventoryItem>();
    public List<InventoryItem> Armour
    {
        get { return armour; }
    }

    [SerializeField]
    private List<InventoryItem> clothing = new List<InventoryItem>();
    public List<InventoryItem> Clothing
    {
        get { return clothing; }
    }

    [SerializeField]
    private List<InventoryItem> health = new List<InventoryItem>();
    public List<InventoryItem> Health
    {
        get { return health; }
    }

    [SerializeField]
    private List<InventoryItem> potion = new List<InventoryItem>();
    public List<InventoryItem> Potion
    {
        get { return potion; }
    }

    public List<InventoryItem> selectedWeapons = new List<InventoryItem>(); //incluirá armas y armaduras
    public List<InventoryItem> selectedItems = new List<InventoryItem>(); //incluirá los consumibles

    public InventorySystem(){
        ClearInventory();
    }

    public void ClearInventory(){
        weapons.Clear();
        armour.Clear();
        clothing.Clear();
        health.Clear();
        potion.Clear();
    }


    public void AddItem(InventoryItem item){
        switch(item.Category){
            case BaseItem.ItemCategory.Weapon:
                weapons.Add(item);
                break;

            case BaseItem.ItemCategory.Armour:
                armour.Add(item);
                break;

            case BaseItem.ItemCategory.Clothing:
                clothing.Add(item);
                break;

            case BaseItem.ItemCategory.Health:
                health.Add(item);
                break;

            case BaseItem.ItemCategory.Potion:
                potion.Add(item);
                break;
        }
    }

    public void RemoveItem(InventoryItem item){
        switch(item.Category){
            case BaseItem.ItemCategory.Weapon:
                weapons.Remove(item);
                break;

            case BaseItem.ItemCategory.Armour:
                armour.Remove(item);
                break;

            case BaseItem.ItemCategory.Clothing:
                clothing.Remove(item);
                break;

            case BaseItem.ItemCategory.Health:
                health.Remove(item);
                break;

            case BaseItem.ItemCategory.Potion:
                potion.Remove(item);
                break;
        }
    }

}
