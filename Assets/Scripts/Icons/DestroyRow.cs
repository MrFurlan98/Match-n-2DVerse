using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyRow : MonoBehaviour, IDestroyAction
{
    public void Invoke(int pPositionX, int pPositionY)
    {
        GameManager.Instance.PBoard.DestroyCollum(pPositionY);
        GameManager.Instance.PBoard.DestroyMatch();
    }
}
