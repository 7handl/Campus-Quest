using UnityEngine;
using UnityEngine.SceneManagement;

public class SetPlayerPositionOnFirstLoad : MonoBehaviour
{
    // Specify the position you want the player to be set to
    public Vector3 playerPosition = new Vector3(0, 0, 0);

    // Reference to the player GameObject
    public GameObject player;

    // Boolean to track if the scene has been loaded for the first time
    private static bool isFirstLoad = true;

    // Register the callback when the scene is loaded
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Unregister the callback when the script is disabled
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Callback function that is called when the scene is loaded
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Ensure the player GameObject is assigned and this is the first load
        if (isFirstLoad)
        {
            if (player != null)
            {
                player.transform.position = playerPosition;
                isFirstLoad = false; // Set the flag to false after setting the position
            }
            else
            {
                Debug.LogError("Player GameObject is not assigned!");
            }
        }
    }
}