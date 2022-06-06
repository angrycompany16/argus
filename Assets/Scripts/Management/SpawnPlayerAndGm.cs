using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayerAndGm : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject gameManager;

    void Awake()
    {
        GameObject activePlayer = GameObject.FindWithTag("Player");        
        GameObject activeGameManager = GameObject.FindWithTag("GameManager");

        GameObject spawnedPlayer = null;
        GameObject spawnedGM = null;

        if (!activeGameManager)
            spawnedGM = Instantiate(gameManager, transform.position, Quaternion.identity);
        
        if (!activePlayer) {
            spawnedPlayer = Instantiate(player, transform.position, Quaternion.identity);
            Debug.Log(spawnedPlayer);
            if (!activeGameManager) 
                spawnedGM.GetComponent<GameManager>().player = spawnedPlayer;
            else 
                activeGameManager.GetComponent<GameManager>().player = spawnedPlayer;
        } 
       
    }
}
