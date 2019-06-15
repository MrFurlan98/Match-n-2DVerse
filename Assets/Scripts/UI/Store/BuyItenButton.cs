using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyItenButton : MonoBehaviour {

    [SerializeField]
    private int idItem;

    [SerializeField]
    private int qntItem;

    [SerializeField]
    private int valueItem;

    [SerializeField]
    private int numberArrayToSend;


    InventoryManager invtryInstance;


    public void BuyItem()
    {
        invtryInstance = InventoryManager.instance;
        if (invtryInstance.hardCurrency >= valueItem)
        {
            invtryInstance.addItem(idItem, qntItem, numberArrayToSend);
            invtryInstance.hardCurrency -= valueItem;
            StoreUI.Instance.SetHCText();
        }
        /*else
        {
            invtryInstance.addItem(idItem, qntItem, numberArrayToSend, false);
        }*/
    }
}
