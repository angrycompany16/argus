using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gamemanager : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}

public static class SceneManagerSystem {
    public static int exitID;
    public static int enterID;
    
    public static void ChangeScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }

    
}