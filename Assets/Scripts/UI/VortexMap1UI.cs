using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VortexMap1UI : MonoBehaviour
{

    [SerializeField]
    private Button m_ConfigButton;

    [SerializeField]
    private Button m_LabButton;

    [SerializeField]
    private Button m_StoreButton;

    [SerializeField]
    private Button m_RoadMapButton;

    [SerializeField]
    private Button m_ChangeVortexMapButton;

    [SerializeField]
    private Button m_PerfilButton;


    private void Start()
    {
        m_ConfigButton.onClick = new Button.ButtonClickedEvent();
        m_ConfigButton.onClick.AddListener(
            delegate {
                UIManagerBeta.Instance.OpenScreen(UIManagerBeta.BUTTONS.CONFIGURATIONS);
            });

        m_LabButton.onClick = new Button.ButtonClickedEvent();
        m_LabButton.onClick.AddListener(
            delegate {
                UIManagerBeta.Instance.OpenScreen(UIManagerBeta.BUTTONS.LAB);
            });

        m_StoreButton.onClick = new Button.ButtonClickedEvent();
        m_StoreButton.onClick.AddListener(
            delegate {
                UIManagerBeta.Instance.OpenScreen(UIManagerBeta.BUTTONS.STORE);
            });

        m_RoadMapButton.onClick = new Button.ButtonClickedEvent();
        m_RoadMapButton.onClick.AddListener(
            delegate {
                UIManagerBeta.Instance.OpenScreen(UIManagerBeta.BUTTONS.ROADMAP);
                UIManagerBeta.Instance.CloseScreen(UIManagerBeta.BUTTONS.TOVORTEX);
                UIManagerBeta.Instance.CloseScreen(UIManagerBeta.BUTTONS.CHANGEVORTEXMAP);
            });

        m_ChangeVortexMapButton.onClick = new Button.ButtonClickedEvent();
        m_ChangeVortexMapButton.onClick.AddListener(
            delegate {
                UIManagerBeta.Instance.OpenScreen(UIManagerBeta.BUTTONS.CHANGEVORTEXMAP);
                UIManagerBeta.Instance.CloseScreen(UIManagerBeta.BUTTONS.TOVORTEX);
            });


        m_PerfilButton.onClick = new Button.ButtonClickedEvent();
        m_PerfilButton.onClick.AddListener(
            delegate {
                UIManagerBeta.Instance.OpenScreen(UIManagerBeta.BUTTONS.PERFIL);
            });



    }

}