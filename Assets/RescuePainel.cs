using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RescuePainel : MonoBehaviour {

    private void OnEnable()
    {
        if (ScoreManager.Instance.Type != null)
        {
            if (!(ScoreManager.Instance.Type == "Resgate"))
            {
                gameObject.SetActive(false);
            }
        }
    }
}
