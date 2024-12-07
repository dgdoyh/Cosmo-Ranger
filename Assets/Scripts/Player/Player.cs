using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IShopCustomer
{
     
    public static Player Instance {  get; private set; }
    public InventoryObject inventory;
    public Item swordItem;
    public int NumberOfDiamonds { get; set; }

    public event EventHandler OnDiamondCollected;


     private void Awake()
     {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            
        }
        else
        {
            Destroy(gameObject);
        }


     }


    public void DiamondCollected()
    {
        NumberOfDiamonds++;
        OnDiamondCollected?.Invoke(this, EventArgs.Empty);
    }

    // this method is where you can change the outfit of the player 
    public void BoughtItem(ItemObject item)
    {
        Debug.Log("Bought item: " + item);
    }
    /**/
    public bool TrySpendDiamondAmount(int amount)
    {
        // Get the diamond quantity from the inventory
        int diamondQuantity = GetDiamondQuantity();

        // Ensure that the diamond quantity is greater than or equal to the specified amount
        if (diamondQuantity >= amount)
        {
            // Deduct the specified amount of diamonds from the inventory
            // Use the AddItem method with a negative amount to subtract diamonds
            inventory.AddItem(GetDiamondItemObject(), -amount);

            // Notify other classes that the diamond amount has changed
            //OnDiamondCollected?.Invoke(this, EventArgs.Empty);

            return true;
        }

        return false;
    }

    public Item GetDiamondItemObject()
    {
        foreach (InventorySlot slot in inventory.Container.items)
        {
            // Check if the item in the inventory slot represents diamonds
            if (slot.item.type == ItemType.Diamonds)
            {
                return slot.item;
            }
        }

        // If no diamond item is found, return null or handle the case accordingly
        return null;
    }

    public int GetDiamondQuantity()
    {
        Item diamondItem = GetDiamondItemObject();

        if (diamondItem != null)
        {
            int diamondQuantity = 0;

            foreach (InventorySlot slot in inventory.Container.items)
            {
                if (slot.item == diamondItem)
                {
                    diamondQuantity += slot.amount;
                }
            }

            return diamondQuantity;
        }
        else
        {
            return 0; // Return 0 if no diamond item is found in the inventory
        }
    }

    private void InitializeSwordItem()
    {
        if (inventory != null && swordItem != null)
        {
            // Add the sword item to the inventory with an initial quantity of 1
            inventory.AddItem(swordItem, 1);
        }
        else
        {
            Debug.LogWarning("Inventory or sword item not assigned.");
        }
    }


    private void OnApplicationQuit()
    {
        inventory.Container.items.Clear();
        InitializeSwordItem();
    }


}
