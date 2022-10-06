using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public GameObject GetPoolObject()
    {
        foreach(Transform child in transform)
        {
            if (!child.gameObject.activeInHierarchy)
            {
                return child.gameObject;
            }
        }

        return null;
    }
}
