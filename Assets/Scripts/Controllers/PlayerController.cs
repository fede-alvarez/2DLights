using UnityEngine;
using UnityEngine.Rendering.Universal;
using DG.Tweening;
using Cinemachine;
public class PlayerController : MonoBehaviour
{
    [Header("Bullets")]
    [SerializeField] private Transform _bulletSpawner;
    [SerializeField] private PoolManager _bulletsPool;

    [Header("Rotation")]
    [SerializeField] private Transform _arm;

    [Header("FX")]
    [SerializeField] private Light2D _muzzleFlashLight;
    //[SerializeField] private AnimationCurve _easeCurve;

    private CinemachineImpulseSource _impulseSource;

    private Rigidbody2D _body;
    private Vector2 _movement;
    private Camera _mainCam;
    private Animator _anim;
    private int _currentScale = -1;
    private bool _recoiling = false;

    private void Awake() 
    {
        _anim = GetComponent<Animator>();
        _body = GetComponent<Rigidbody2D>();  
        _impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    private void Start() 
    {
        _mainCam = Camera.main;
    }

    private void Update() 
    {
        InputManagement();
        SetAnimations();

        Vector2 mousePosition = _mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mouseDir = (mousePosition - (Vector2) _arm.position).normalized;
        _arm.right = mouseDir * _currentScale;

        FlipSprite(mousePosition);

        if (Input.GetMouseButtonDown(0) && !_recoiling)
        {
            _recoiling = true;

            _arm.DOLocalMove(_arm.localPosition + _arm.right * _currentScale * 0.2f, 0.08f)
                .SetLoops(2, LoopType.Yoyo)
                .SetEase(Ease.OutQuint);

            DOVirtual.Float(0,6, 0.08f,
                (value) => {
                    _muzzleFlashLight.intensity = value;
                }
            ).SetLoops(2, LoopType.Yoyo)
            .SetEase(Ease.OutQuint);

            _impulseSource.GenerateImpulse();


            /**
             * Shotgun spread pattern
             */
            int[] spreadPattern = new int[] {-10, 0, 10};

            for(int i = 0; i < spreadPattern.Length; i++)
            {
                CreateBullet(spreadPattern[i]);
            }

            Invoke("Recoil", 1.0f);
        }
    }

    private void InputManagement()
    {
        _movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
    }

    private void SetAnimations()
    {
        if (_body.velocity.magnitude > 0.01f)
            _anim.Play("Run");
        else
            _anim.Play("Idle");
    }

    private void Recoil()
    {
        _recoiling = false;
    }

    private void CreateBullet(float spreadAngle = 0)
    {
        GameObject go = _bulletsPool.GetPoolObject();
        if (go == null) return;
        
        //Bullet bullet = Instantiate(_bulletPrefab, _bulletSpawner.position, Quaternion.identity);
        Bullet bullet = go.GetComponent<Bullet>();

        if (bullet)
        {
            float x = _bulletSpawner.position.x - _arm.transform.position.x;
            float y = _bulletSpawner.position.y - _arm.transform.position.y;

            float rotateAngle = spreadAngle + (Mathf.Atan2(y, x) * Mathf.Rad2Deg);

            bullet.transform.position = _bulletSpawner.position;
            bullet.transform.Rotate(new Vector3(0,0,rotateAngle));
            bullet.gameObject.SetActive(true);
            
            bullet.Shoot();
        }
    }

    private void FlipSprite(Vector2 mousePosition)
    {
        _currentScale = -1;

        if (mousePosition.x > transform.position.x) 
            _currentScale = 1;

        transform.localScale = new Vector2(_currentScale,1);
    }

    private void FixedUpdate() 
    {
        _body.velocity = _movement * 8;
    }
}
