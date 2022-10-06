using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Rigidbody2D _body;
    private Transform _player;

    private bool _followingPlayer = true;

    private void Awake() 
    {
        _body = GetComponent<Rigidbody2D>();  
    }

    private void Start() 
    {
        _player = GameManager.GetInstance.GetPlayer;
    }

    private void Update() 
    {
        //Debug.DrawRay(transform.position - new Vector3(0, 0.5f), transform.right * 2, Color.red);
        //Debug.DrawRay(transform.position + new Vector3(0, 0.5f), transform.right * 2, Color.red);
    }

    private void FixedUpdate() 
    {
        if (_followingPlayer)
        {
            Vector3 playerDirection = (_player.position - transform.position).normalized;
            _body.velocity = playerDirection * 5;
            transform.right = playerDirection;
        }
    }
}
