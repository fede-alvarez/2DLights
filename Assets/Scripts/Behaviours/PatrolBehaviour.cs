using UnityEngine;

public class PatrolBehaviour : MonoBehaviour
{
    [SerializeField] private Transform _routePointsParent;
    [SerializeField] private float _speed = 10;
    [SerializeField] private bool _isPatroling = false;

    private Transform _nextPoint;
    private Rigidbody2D _body;

    private int _currentPointIndex = 0;

    private void Awake() 
    {
        _body = GetComponent<Rigidbody2D>();
    }

    private void Start() 
    {
        foreach(Transform t in _routePointsParent)
        {
            t.GetComponent<SpriteRenderer>().enabled = false;
        }

        _nextPoint = _routePointsParent.GetChild(Random.Range(0, _routePointsParent.childCount));   
    }

    private void FixedUpdate() 
    {
        if (!_isPatroling) return;
        GoToPoint(_nextPoint);
    }

    private void GoToPoint(Transform nextPoint)
    {
        Vector2 movementDirection = (nextPoint.position - transform.position).normalized;
        
        _body.velocity = movementDirection * _speed;

        if (Vector2.Distance(nextPoint.position, transform.position) < 0.5f )
        {
            _nextPoint = _routePointsParent.GetChild(Random.Range(0, _routePointsParent.childCount));   
        }
    }

    private void OnDrawGizmosSelected() 
    {
        Transform prevPoint;
        prevPoint = _routePointsParent.GetChild(0);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, prevPoint.position);
        
        foreach(Transform t in _routePointsParent)
        {
            Gizmos.DrawLine(prevPoint.position, t.position);
            prevPoint = t;
        }

        //Transform lastPoint = _routePointsParent.GetChild(_routePointsParent.childCount - 1);
        Gizmos.DrawLine(prevPoint.position, transform.position);
    }
}
