using UnityEngine;
using UnityEngine.AI;

public class EnemyAgent : MonoBehaviour
{
    private NavMeshAgent _agent;
    private Transform _player;
    private bool _followingPlayer = false;

    private void Awake() 
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Start() 
    {
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;

        _player = GameManager.GetInstance.GetPlayer;
    }

    private void Update() 
    {
        if (_followingPlayer)
            _agent.SetDestination(_player.position);
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (!other.CompareTag("Player")) return;
        _followingPlayer = true;
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if (!other.CompareTag("Player")) return;
        _followingPlayer = false;
    }
}
