using System.Collections.Generic;
using UnityEngine;

public class Shapeshift : MonoBehaviour
{
    public List<GameObject> forms = new List<GameObject>();
    public List<GameObject> enabledForms = new List<GameObject>();
    int currentObject = 0;

    bool isShapeShifting;

    void Start() {
        enabledForms[currentObject].SetActive(true);
    }

    void Update() {
        GetInput();
    }

    void GetInput() {
        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            isShapeShifting = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift)) {
            isShapeShifting = false;
        }

        if (isShapeShifting && Input.GetKeyDown(KeyCode.RightArrow)) {
            ChangeForm(1);
        }
        
        if (isShapeShifting && Input.GetKeyDown(KeyCode.LeftArrow)) {
            ChangeForm(-1);
        }
    }

    public void ChangeForm(int direction) {
        enabledForms[currentObject].SetActive(false);

        int newObject = currentObject + direction;
        if (newObject == -1) {
            newObject = enabledForms.Count - 1;
        } else if(newObject == enabledForms.Count) {
            newObject = 0;
        }

        enabledForms[newObject].SetActive(true);

        if (enabledForms[newObject].TryGetComponent(out ShapeshiftTimer timer)) {
            timer.direction = -direction;
        }

        currentObject = newObject;
    }
}
