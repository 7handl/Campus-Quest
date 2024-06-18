using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    [Header("Menu Buttons")]
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button continueGameButton;
    
    private void Start()
    {
        if (!DataPersistenceManager.instance.HasGameData())
        {
            continueGameButton.interactable = false;
        }
    }
    
    public void OnNewGameClicked()
    {
        DisableMenuButtons();
        
        DataPersistenceManager.instance.NewGame();

        SceneManager.LoadSceneAsync("Dialog");
    }

    public void OnContinueGameClicked()
    {
        DisableMenuButtons();
        
        SceneManager.LoadSceneAsync("Dialog");
    }

    private void DisableMenuButtons()
    {
        newGameButton.interactable = false;
        continueGameButton.interactable = false;
    }
}
