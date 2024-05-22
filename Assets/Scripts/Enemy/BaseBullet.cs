using UnityEngine;

public abstract class BaseBullet : MonoBehaviour, IBullet
{
    protected float _baseSpeed = 10f;
    protected BulletType _bulletType;
    protected Vector2 _direction;

    [SerializeField] protected LayerMask PlayerLayer;

    public float Speed { get; set; }

    public BulletType BulletType
    {
        get => _bulletType;
        set => _bulletType = value;
    }

    protected virtual void Start()
    {
        AdjustDifficulty();
    }

    public abstract void OnHit();

    public void SetDirection(Vector2 direction)
    {
        _direction = direction.normalized;
    }

    protected virtual void Update()
    {
        transform.Translate(_direction * Speed * Time.deltaTime);

        if (!IsVisibleFrom(Camera.main))
        {
            ObjectPoolManager.Instance.ReturnToPool(gameObject.name.Replace("(Clone)", "").Trim(), gameObject);
        }
    }

    protected bool IsVisibleFrom(Camera camera)
    {
        var viewportPoint = camera.WorldToViewportPoint(transform.position);
        return viewportPoint.x > 0 && viewportPoint.x < 1 && viewportPoint.y > 0 && viewportPoint.y < 1;
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & PlayerLayer) != 0)
        {
            OnHit();
        }
    }

    protected void AdjustDifficulty()
    {
        switch (DataManager.Instance.Difficult)
        {
            case 0: 
                Speed = _baseSpeed * 0.5f;
                break;
            case 1: 
                Speed = _baseSpeed * 0.75f;
                break;
            case 2: 
                Speed = _baseSpeed;
                break;
        }
    }
}