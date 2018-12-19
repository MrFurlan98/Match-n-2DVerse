using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MenuUI : MonoBehaviour
{
    private ScenaryItemUI[] m_pScenarys;

    [SerializeField]
    private Transform m_pRootView;

    private void Start()
    {
        m_pScenarys = m_pRootView.GetComponentsInChildren<ScenaryItemUI>(false);
        foreach(ScenaryItemUI tScenary in m_pScenarys)
        {
            tScenary.UpdateSelectButton();
        }
    }

}
