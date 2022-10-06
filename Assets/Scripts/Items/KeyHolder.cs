using UnityEngine;
using System.Collections.Generic;

public class KeyHolder : MonoBehaviour
{
    private List<KeyCard.KeyType> _keys = new List<KeyCard.KeyType>();

    public void AddKey(KeyCard.KeyType type)
    {
        _keys.Add(type);
    }

    public void RemoveKey(KeyCard.KeyType type)
    {
        _keys.Remove(type);
    }

    public bool HasKey(KeyCard.KeyType type)
    {
        return _keys.Contains(type);
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        KeyCard key = other.GetComponent<KeyCard>();
        if (key != null)
        {
            AddKey(key.GetKeyType);
            other.gameObject.SetActive(false);
        }
        
        DoorKey door = other.GetComponent<DoorKey>();
        if (door != null)
        {
            if (HasKey(door.GetDoorType))
            {
                door.gameObject.SetActive(false);
                RemoveKey(door.GetDoorType);
            }
        }
    }
}
