using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarbarianCharacterCustomization : MonoBehaviour {

    public static BarbarianCharacterCustomization sharedInstance;

    CharacterSelectionManager manager;
    Canvas configCanvas;

    private void Awake()
    {
        if(sharedInstance == null){
            sharedInstance = this;
        }

        manager = GameObject.Find("CharacterSelectionManager").GetComponent<CharacterSelectionManager>();
        configCanvas = GameObject.Find("Canvas").GetComponent<Canvas>();
    }

    public void ToggleCanvas(){
        if(manager.selectedCharacter.GetComponent<BarbarianCharacterController>()){
            configCanvas.enabled = true;
        } else {
            configCanvas.enabled = false;
        }
    }


    public Hashtable mapping = new Hashtable();

    // Use this for initialization
    void Start () {
        mapping.Add("ToggleHair", "Geometry/model:geo/model:Hair");
        mapping.Add("ToggleBand", "Geometry/model:geo/model:Strap");
        mapping.Add("ToggleSkull", "Geometry/model:geo/model:Skull");


        mapping.Add("ToggleBracelet", "Geometry/model:geo/model:Bracer");
        mapping.Add("ToggleBelt", "Geometry/model:geo/model:Belt");
        mapping.Add("ToggleNecklace", "Geometry/model:geo/model:Neckless");


        mapping.Add("ToggleLeftFoot", "Geometry/model:geo/model:SandelL");
        mapping.Add("ToggleRightFoot", "Geometry/model:geo/model:SandalR");


        mapping.Add("ToggleBandages", "Geometry/model:geo/model:Bandages");
        mapping.Add("ToggleLeftAnkle", "Geometry/model:geo/model:AnkletL");
        mapping.Add("ToggleRightAnkle", "Geometry/model:geo/model:AnkletR");
    }


    public void SetVisiblePad(Toggle toggle){


        foreach ( DictionaryEntry map in mapping){
            if (map.Key.Equals(toggle.name)){
                manager.selectedCharacter.transform.Find(map.Value as string).gameObject.SetActive(
                    !manager.selectedCharacter.transform.Find(map.Value as string).gameObject.activeInHierarchy);
            }
        }
    }


    float red = 1, green = 1, blue = 1, 
          xs = 1, ys = 1, zs = 1;

    public void ChangeSlideValue(Slider slider){
        switch(slider.name){
            case "SliderRed":
                red = slider.value;
                break;
            case "SliderGreen":
                green = slider.value;
                break;
            case "SliderBlue":
                blue = slider.value;
                break;


            case "SliderX":
                xs = slider.value;
                break;
            case "SliderY":
                ys = slider.value;
                break;
            case "SliderZ":
                zs = slider.value;
                break;
        }

        manager.selectedCharacter.transform.localScale = new Vector3(xs, ys, zs);

        foreach(Renderer rend in manager.selectedCharacter.GetComponentsInChildren<Renderer>()){
            rend.material.color = new Color(red, green, blue);
        }

    }

    public void ChangeDropdownValue(Dropdown dropdown){
        Debug.Log(dropdown.name +"-"+ dropdown.value);
    }


}
