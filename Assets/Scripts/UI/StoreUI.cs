using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreUI : MonoBehaviour
{
    private static StoreUI m_Instance;

    public Text m_HCText;

    [SerializeField]
    private Button m_ExitButton;

    [SerializeField]
    private Button m_InGameScreenButton;

    [SerializeField]
    private Button m_HardCurrencyScreenButton;

    [SerializeField]
    private Button m_MembersItensButton;

    [SerializeField]
    private GameObject HardCurrencyItensScreen;

    [SerializeField]
    private GameObject MemberItensScreen;

    [SerializeField]
    private GameObject InGameItensScreen;

    [SerializeField]
    private Button m_addHCButton;

    private GameObject AtualScreen;

    public static StoreUI Instance
    {
        get
        {
            return m_Instance;
        }

        set
        {
            m_Instance = value;
        }
    }

    public void Awake()
    {
        m_Instance = this;
    }

    private void Start()
    {
        AtualScreen = InGameItensScreen;

        m_ExitButton.onClick = new Button.ButtonClickedEvent();
        m_ExitButton.onClick.AddListener(
            delegate
            {
                UIManagerBeta.Instance.CloseScreen(UIManagerBeta.SCREENS.STORE);
            });

        m_InGameScreenButton.onClick = new Button.ButtonClickedEvent();
        m_InGameScreenButton.onClick.AddListener(
            delegate
            {
                AtualScreen.SetActive(false);
                AtualScreen = InGameItensScreen;
                AtualScreen.SetActive(true);

            });

        m_HardCurrencyScreenButton.onClick = new Button.ButtonClickedEvent();
        m_HardCurrencyScreenButton.onClick.AddListener(
            delegate
            {
                AtualScreen.SetActive(false);
                AtualScreen = HardCurrencyItensScreen;
                AtualScreen.SetActive(true);
            });

        m_MembersItensButton.onClick = new Button.ButtonClickedEvent();
        m_MembersItensButton.onClick.AddListener(
            delegate
            {
                AtualScreen.SetActive(false);
                AtualScreen = MemberItensScreen;
                AtualScreen.SetActive(true);
            });

        m_addHCButton.onClick = new Button.ButtonClickedEvent();
        m_addHCButton.onClick.AddListener(
            delegate
            {
                InventoryManager.instance.hardCurrency += 1000;
                SetHCText();
            });

    }

    public void SetHCText()
    {
        m_HCText.text = "Gold: " + InventoryManager.instance.hardCurrency;
    }
}