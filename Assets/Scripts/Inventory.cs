﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour 
{
	private List<Item> inventory = new List<Item>(); //List of Inventory items
    
   void Start()
   {
       inventory.Add(new Item("sword", 0, Item.ItemType.Skin, "It is a sword", 50));
       inventory.Add(new Item("bannana", 1, Item.ItemType.Consumable, "It is a bannan", 100));
   }

   /** @brief Add Item to Inventory
    * @param newItem - specifies the Item to be added
    * @return void
    */
   public void AddItem(Item newItem)
   {
       bool itemExists = false;

       foreach (Item item in inventory)
       {
           if (item.itemName == newItem.itemName)
           {
               item.itemAmount++;
               itemExists = true;
           }
       }

       if (!itemExists)
       {
           newItem.itemAmount++;
           inventory.Add(new Item(newItem));
       }
   }

   /** @brief Add Item to Inventory
    * @param name - specifies the name of the Item
    * @param id - specifies Item's ID number
    * @param type - specifies the type of the Item
    * @param description - specifies the description of the Item
    * @param price - specifies the price of the Item
    * @return void
    */
   public void AddItem(string name, int id, Item.ItemType type, string description, int price)
   {
       bool itemExists = false;

       //Add Item to inventory by increasing it's amount since it already existed
       foreach (Item item in inventory)
       {
           if (item.itemName == name)
           {
               item.itemAmount++;
               itemExists = true;
           }
       }

       //Add Item to inventory
       if (!itemExists)
       {
           inventory.Add(new Item(name, id, type, description, price));
       }
   }

   /** @brief Get Item from Inventory
    * @param name - specifies the name of the Item
    * @return Item
    */
   public Item GetItem(string name)
   {
       foreach (Item item in inventory)
       {
           if (item.itemName == name)
               return item;
       }
       return null;
   }

   public Item GetItem(int index)
   {
       if (index < inventory.Count)
           return inventory[index];
       else
           return null;
   }

   public int GetInventorySize()
   {
       return inventory.Count;
   }
}
