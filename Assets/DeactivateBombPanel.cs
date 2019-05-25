using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateBombPanel : MonoBehaviour {

    private void OnEnable()
    {
        if(ScoreManager.Instance.Type != null)
        {
            if (!(ScoreManager.Instance.Type == "Desativar_Bomba"))
            {
                gameObject.SetActive(false);
            }
        }

    }
}
