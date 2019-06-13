using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoButton : MonoBehaviour {

    [SerializeField]
    private Image spriteToSend;

    [SerializeField]
    private string itenTitle;

    [SerializeField]
    private string itenDescriptiu;


    StoreUI storeInstance;


    public void openPopUp()
    {
        storeInstance = StoreUI.instance;
        storeInstance.attPopUp(spriteToSend,itenTitle, itenDescriptiu);
        /*else
        {
            invtryInstance.addItem(idItem, qntItem, numberArrayToSend, false);
        }*/
    }
}