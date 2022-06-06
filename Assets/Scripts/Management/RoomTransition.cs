// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;

public class RoomTransition : MonoBehaviour
{
    [SerializeField] int ID;
    [SerializeField] int exitID;
    [SerializeField] string targetScene;

    void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")) {
            SceneManagerSystem.enterID = exitID;
            SceneManagerSystem.ChangeScene(targetScene);
        }
    }
}
