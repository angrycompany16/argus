using UnityEngine;

public class ShapeshiftTimer : MonoBehaviour
{
    float timeSinceShapeShifted;
    [SerializeField] float maxTime;
    public int direction;
    [SerializeField] Shapeshift shapeshift;

    void Update() {
        timeSinceShapeShifted += Time.deltaTime;
    
        if (timeSinceShapeShifted >= maxTime) {
            ReturnToPreviousShape();
        }
    }

    void ReturnToPreviousShape() {
        shapeshift.ChangeForm(direction);
    }
}
