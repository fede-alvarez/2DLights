using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform _player;
    private static GameManager instance;

    private void Awake()
    {
      if (instance != null && instance != this)
      {
        Destroy(this.gameObject);
      }else{
        instance = this;
      }
    }

    private void OnDestroy()
    {
      if (instance != null)
        instance = null;
    }

    public Transform GetPlayer => _player;

    public static GameManager GetInstance
    {
      get { return instance; }
    }
}
