using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DialogueTrigger : MonoBehaviour, IDataPersistence
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;


    [SerializeField] private string miniGameName;


    private bool playerInRange;

    private void Awake() 
    { 
        playerInRange = false;
        visualCue.SetActive(false);
    }

    public void LoadData(GameData data)
    {
        
    }

    public void SaveData(ref GameData data)
    {
        
    }

    private void Update() 
    {
        if (playerInRange && !DialogueManager.GetInstance().dialogueIsPlaying) 
        {
            visualCue.SetActive(true);
            if (InputManager.GetInstance().GetInteractPressed()) 
            {
                StartCoroutine(waiter());
                IEnumerator waiter()
                {
                    yield return new WaitForSeconds(0.2f);
                    DialogueManager.GetInstance().EnterDialogueMode(inkJSON, miniGameName);
                }
                
            }
        }
        else 
        {
            visualCue.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider) 
    {
        if (collider.gameObject.tag == "Player")
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider) 
    {
        if (collider.gameObject.tag == "Player")
        {
            playerInRange = false;
        }
    }
}
