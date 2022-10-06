using UnityEngine;

public class KeyCard : MonoBehaviour
{
    public enum KeyType {
      Red,
      Green,
      Blue
    }

    [SerializeField] private KeyType _keyType;

    public KeyType GetKeyType {
        get { return _keyType; }
    }
}
