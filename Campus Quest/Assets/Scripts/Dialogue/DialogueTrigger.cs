using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class DialogueTrigger : MonoBehaviour, IDataPersistence
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    private bool playerInRange;

    public bool completed = false;

    [SerializeField] private string miniGameName;

    [SerializeField] private string id;

    [ContextMenu("Generate guid for id")]
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }

    private void Awake()
    {
        playerInRange = false;
        visualCue.SetActive(false);
    }

    public void LoadData(GameData data)
    {
        data.miniGameCompleted.TryGetValue(id, out completed);
        if (completed)
        {
            gameObject.SetActive(false);
        }
    }

    public void SaveData(ref GameData data)
    {
        if (data.miniGameCompleted.ContainsKey(id))
        {
            data.miniGameCompleted.Remove(id);
        }
        data.miniGameCompleted.Add(id, completed);
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
