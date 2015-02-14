using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OutputInventory : MonoBehaviour 
{
    //Other classes
    private Inventory inventory;

    //Child objects
    private Text[] invText;
    private Button[] invButton;

    //Tracking current Items listed
    private int InventoryIndex;

	// Use this for initialization
	void Start () 
    {
        InventoryIndex = 0; //Set Default Index
	    inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        invText = GetComponentsInChildren<Text>(); //Get Reference to the text component of the inventory tab
        invButton = GetComponentsInChildren<Button>(); //Get Reference to the buttons of the inventory tab
	}
    
    //Output item information depending on which one is currently selected
    public void GetItemSelected(int index)
    {
        if (index < invText.Length && index < inventory.GetInventorySize())
        {
            invText[index].text = "Name: " + inventory.GetItem(index).itemName + "\nPrice: " + inventory.GetItem(index).itemPrice; //Other information can be added
        }
    }

    //Display the Item Sprites according to the Item Type being displayed
    public void UpdateItemSprites()
    {
        //Set each item's image according to the type of the item
        for (int i = InventoryIndex; i < (InventoryIndex + 3) && i < invText.Length; i++)
        {
            switch(inventory.GetItem(i).itemType)
            {
                case Item.ItemType.Skin:
                break;

                case Item.ItemType.Consumable:
                break;

                case Item.ItemType.Achievement:
                break;

                default:
                    Debug.Log("ItemType could not be found.");
                break;
            }
        }
    }

    public void ScrollRight()
    {
        //As long as the Index has not reached the end of the array
        if(InventoryIndex < inventory.GetInventorySize())
            InventoryIndex++;

    }
}
