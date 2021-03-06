﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VictoryScreen : MonoBehaviour
{
    [SerializeField]
    private Button returnToMainMenuButton;
    [SerializeField]
    private string mainMenuSceneName;
    [SerializeField]
    private RectTransform victoryScreen;
    [SerializeField]
    private Text victoryText;
    [SerializeField]
    private List<BaseTile> bases;

    private void Awake()
    {
        returnToMainMenuButton.onClick.AddListener(ReturnToMainMenu);
        foreach (var baseTile in bases)
        {
            baseTile.baseDied.AddListener(ShowVictoryScreen);
        }
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(mainMenuSceneName);
    }

    public void ShowVictoryScreen()
    {
        foreach (var baseTile in bases)
        {
            if (!baseTile.IsDead())
            {
                victoryText.text = baseTile.alignment.name + " has won!";
                victoryScreen.gameObject.SetActive(true);
                victoryScreen.SetAsFirstSibling();
                return;
            }
        }
    }
}
