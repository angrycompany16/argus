using UnityEngine;

[CreateAssetMenu(fileName = "Door", menuName = "ScriptableObjects/DoorPropertiesSO", order = 1)]
public class DoorProperties : ScriptableObject
{
    public bool open;
    public DoorProperties otherSide;
}
