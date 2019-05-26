using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyMemberButtonUI : MonoBehaviour {
    
    [SerializeField]
    private int numberChestButton;
    [SerializeField]
    private int valueItem;
    InventoryManager invtryInstance;
    [SerializeField]
    private bool canBuy = true;



    public void BuyMember()
    {
        invtryInstance = InventoryManager.instance;
        if (invtryInstance.hardCurrency >= valueItem) { 
            PopUpStoreMemberItem.Instance.OpenPopUp(numberChestButton,canBuy);
            invtryInstance.hardCurrency -= valueItem;
            StoreUI.Instance.SetHCText();
        }
    }
}
