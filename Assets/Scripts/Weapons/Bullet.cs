using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed = 10;
    private Rigidbody2D _body;
    private TrailRenderer _trail;

    private void Awake() 
    {
        _body = GetComponent<Rigidbody2D>();
        _trail = GetComponent<TrailRenderer>();
    }
    private void Start() 
    {
        gameObject.SetActive(false);
    }

    private void OnEnable() 
    {
        if (_trail != null) _trail.Clear();
    }

    public void Shoot() 
    {
        _body.AddForce(transform.right * _speed, ForceMode2D.Impulse);
        Invoke("Kill", 2);
    }

    private void Kill()
    {
        transform.rotation = Quaternion.Euler(0,0,0);
        gameObject.SetActive(false);
    }
}
