using UnityEngine;

public class DoorKey : MonoBehaviour
{
    [SerializeField] private KeyCard.KeyType _doorType;

    public KeyCard.KeyType GetDoorType {
        get { return _doorType; }
    }
}
