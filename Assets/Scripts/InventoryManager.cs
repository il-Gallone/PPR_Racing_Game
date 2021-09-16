using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public GameObject[] inventoryButtons;
    public GameObject inventoryPopUp;
    public Button popUpButton;
    public Text popUpTitle;
    public Text popUpDescription;
    public int activePopUp = 0;

    public Sprite blueprintIcon;

    //UI fade parameters
    public float fadeSpeed = 10f, popupMoveSpeed = 5f;
    bool fadeIn = false;
    CanvasGroup canvasGroup;

    // Start is called before the first frame update
    void Start()
    {
        canvasGroup = inventoryPopUp.GetComponent<CanvasGroup>();

        for(int i = 0; i<GameManager.instance.stats.inventory.Count; i++)
        {
            if(i < inventoryButtons.Length)
            {
                inventoryButtons[i].GetComponent<Button>().interactable = true;
            }
        }
    }


    private void Update()
    {
        // the following code fades in/out UI
        if (fadeIn)
        {
            canvasGroup.alpha += Time.deltaTime * fadeSpeed;

            inventoryPopUp.transform.position = Vector3.Lerp(inventoryPopUp.transform.position, inventoryButtons[activePopUp].transform.position, popupMoveSpeed * Time.deltaTime);
                //Vector3.MoveTowards(inventoryPopUp.transform.position, inventoryButtons[activePopUp].transform.position, popupMoveSpeed * Time.deltaTime);
        }
        else if (!fadeIn)
        {
            canvasGroup.alpha -= Time.deltaTime * fadeSpeed;
        }
    }

    public void GeneratePopUp(int inventoryID)
    {
        if(!fadeIn || activePopUp != inventoryID)
        {
            fadeIn = true;
            inventoryPopUp.SetActive(true);
            activePopUp = inventoryID;
            //inventoryPopUp.transform.position = inventoryButtons[inventoryID].transform.position;
            popUpTitle.text = GameManager.instance.stats.inventory[inventoryID];
            popUpDescription.text = GetPopUpDescription(GameManager.instance.stats.inventory[inventoryID]);
            popUpButton.gameObject.SetActive(IsItemBlueprint(GameManager.instance.stats.inventory[inventoryID]));
        }
        else if(inventoryPopUp.activeInHierarchy && activePopUp == inventoryID)
        {
            //inventoryPopUp.SetActive(false);
            fadeIn = false;
        }
    }

    string GetPopUpDescription(string itemName)
    {
        string text = "Description not Loaded";
        //for now hard coded, TODO load descriptions from file
        if(itemName == "Shotgun Blueprint")
        {
            text = "A Weapon that Sprays Multiple Bullets \nScrap Cost: 100 \nWeapon Part Cost: 10";
        }
        if (itemName == "Machinegun Blueprint")
        {
            text = "A Weapon that has a high firing rate \nScrap Cost: 100 \nWeapon Part Cost: 10";
        }
        return text;
    }

    bool IsItemBlueprint(string itemName)
    {
        //for now hard coded, TODO determine blueprint from file
        if(itemName == "Shotgun Blueprint" || itemName == "Machinegun Blueprint")
        {
            return true;
        }
        return false;
    }

    public void FabriacteBlueprint()
    {
        //for now hard coded, TODO determine cost of blueprint from file
        if(GameManager.instance.stats.scrap >= 100 && GameManager.instance.stats.weaponParts >= 10)
        {
            GameManager.instance.stats.scrap -= 100;
            GameManager.instance.stats.weaponParts -= 10;
            inventoryPopUp.SetActive(false);
            if (GameManager.instance.stats.inventory[activePopUp] == "Shotgun Blueprint")
            {
                GameManager.instance.stats.unlockedWeaponIDs.Add("Shotgun");
                GameManager.instance.stats.inventory.Remove("Shotgun Blueprint");
            }
            else if (GameManager.instance.stats.inventory[activePopUp] == "Machinegun Blueprint")
            {
                GameManager.instance.stats.unlockedWeaponIDs.Add("Machinegun");
                GameManager.instance.stats.inventory.Remove("Machinegun Blueprint");
            }
            for(int i = 0; i < inventoryButtons.Length; i++)
            {
                inventoryButtons[i].GetComponent<Button>().interactable = false;
            }
            for (int i = 0; i < GameManager.instance.stats.inventory.Count; i++)
            {
                if (i < inventoryButtons.Length)
                {
                    inventoryButtons[i].GetComponent<Button>().interactable = true;
                }
            }
        }
    }
}
