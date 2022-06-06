using UnityEngine;

[CreateAssetMenu(fileName = "Key", menuName = "ScriptableObjects/KeyItem", order = 1)]
public class KeyItem : ScriptableObject
{
    public DoorProperties opens;
}
