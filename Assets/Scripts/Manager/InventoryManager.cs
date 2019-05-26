using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour {

    public static InventoryManager instance;

    public int hardCurrency;

    public class inventoryConsumablesItens
    {
        public int qtdItem;

        public inventoryConsumablesItens()
        {
            qtdItem = 0;
        }
    }

    public class inventoryBoostsItens
    {
        public int qtdItem;
        public int durationItem;

        public inventoryBoostsItens(int itemLvl)
        {
            qtdItem = 0;
            durationItem = itemLvl;
        }
    }

    public List<inventoryConsumablesItens> listConsumables = new List<inventoryConsumablesItens>();
    public List<inventoryBoostsItens> listBoosts = new List<inventoryBoostsItens>();


    // Use this for initialization
    private void Awake () {
        instance = this;
        createListConsumables();
        createListBoosts();
	}
	
	public void createListConsumables()
    {
        for (int i = 0; i<=3; i++)
        {
            listConsumables.Add(new inventoryConsumablesItens());
        }

    }

    public void createListBoosts()
    {
        for (int i = 0; i <= 8; i++)
        {
            listBoosts.Add(new inventoryBoostsItens(checkItemLvl(i)));
        }

    }

    public int checkItemLvl(int numberToCheck)
    {
        numberToCheck = numberToCheck % 3;
        return numberToCheck;
    }

    public void addItem(int idItem,int quant ,int arrayNumber)
    {
        if (arrayNumber == 1)
        {
            listConsumables[idItem].qtdItem+=quant;
        }

        if (arrayNumber == 2)
        {
            listBoosts[idItem].qtdItem++;
        }

        if (arrayNumber != 1 && arrayNumber != 2)
        {
            print("array item errado");
        }
    }
}
