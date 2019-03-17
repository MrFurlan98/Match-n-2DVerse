using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyArea : MonoBehaviour, IDestroyAction
{
    public void Invoke(int pPositionX, int pPositionY)
    {
        //GameManager.Instance.PBoard.DestroyArea(pPositionX, pPositionY, 5);
        //GameManager.Instance.PBoard.DestroyMatch();
    }
}
