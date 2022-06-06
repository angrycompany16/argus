// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    int enterID;
    public GameObject player;

    void OnLevelWasLoaded(int level) {
        PlacePlayer();
    }

    void PlacePlayer() {
        enterID = SceneManagerSystem.enterID;

        GameObject enterPos = GameObject.Find(enterID.ToString());
        if (enterPos) {
            Debug.Log("Placed");
            player.transform.position = enterPos.transform.position;
        }
    }
}

public static class SceneManagerSystem {
    public static int enterID = 0;
    
    public static void ChangeScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }
}