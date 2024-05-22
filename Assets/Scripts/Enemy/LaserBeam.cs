using System.Collections;
using UnityEngine;

public class LaserBeam : MonoBehaviour, IBullet
{
    [SerializeField] private float LaserDuration = 2.0f;
    [SerializeField] private LayerMask PlayerLayer;

    private BoxCollider2D _boxCollider;
    private SpriteRenderer _spriteRenderer;
    private float _speed = 0f;
    private BulletType _bulletType;

    public float Speed
    {
        get => _speed;
        set => _speed = value;
    }

    public BulletType BulletType
    {
        get => _bulletType;
        set => _bulletType = value;
    }

    private void Start()
    {
        BulletType = BulletType.LaserBeam;
    }

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        StartCoroutine(LaserRoutine());
    }

    private void OnDisable()
    {
        _boxCollider.enabled = false;
        _spriteRenderer.enabled = false;
    }

    private IEnumerator LaserRoutine()
    {
        _boxCollider.enabled = true;
        _spriteRenderer.enabled = true;
        yield return new WaitForSeconds(LaserDuration);
        gameObject.SetActive(false);
    }

    public void OnHit()
    {
    }

    public void SetDirection(Vector2 direction) { }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & PlayerLayer) != 0)
        {
            OnHit();
        }
    }
}