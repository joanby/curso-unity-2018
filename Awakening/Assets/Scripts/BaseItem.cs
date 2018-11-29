using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseItem {

    public enum ItemCategory{
        Weapon,
        Armour,
        Clothing,
        Health,
        Potion
    }

    [SerializeField]
    private string name;

    [SerializeField]
    private string description;
    
    public string Name{
        get { return name; }
        set { name = value; }
    }

    public string Description {
        get { return description;  }
        set { description = value; }
    }

}
