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
                ScreenManager.Instance.OpenScreen(ScreenManager.SCREEN.CONFIG);
            });

        m_LabButton.onClick = new Button.ButtonClickedEvent();
        m_LabButton.onClick.AddListener(
            delegate {
                ScreenManager.Instance.OpenScreen(ScreenManager.SCREEN.LAB);
            });

        m_StoreButton.onClick = new Button.ButtonClickedEvent();
        m_StoreButton.onClick.AddListener(
            delegate {
                ScreenManager.Instance.OpenScreen(ScreenManager.SCREEN.STORE);
            });

        m_RoadMapButton.onClick = new Button.ButtonClickedEvent();
        m_RoadMapButton.onClick.AddListener(
            delegate {
                ScreenManager.Instance.OpenScreen(ScreenManager.SCREEN.ROADMAP);
                ScreenManager.Instance.CloseScreen(ScreenManager.SCREEN.ROADMAP_VORTEX);
            });

        m_PerfilButton.onClick = new Button.ButtonClickedEvent();
        m_PerfilButton.onClick.AddListener(
            delegate {
                ScreenManager.Instance.OpenScreen(ScreenManager.SCREEN.PERFIL);
            });



    }

}