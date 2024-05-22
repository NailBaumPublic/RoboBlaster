using System.Collections;
using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour, IEnemy
{
    [SerializeField] protected GameObject BulletPrefab;
    [SerializeField] protected Transform FirePoint;
    [SerializeField] protected float BaseAttackInterval = 1.0f;
    [SerializeField] protected LayerMask PlayerBulletLayer;
    [SerializeField] protected LayerMask PlayerLayer;

    protected Rigidbody2D _rigidbody;
    protected Animator _animator;
    protected MainSceneManager _mainSceneManager;
    protected float _attackTimer;
    protected bool _isDead = false;
    protected bool _isHit = false;

    public int Hp { get; set; }
    public float Speed { get; set; }
    public BulletType BulletType { get; set; }

    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    protected virtual void Start()
    {
        _mainSceneManager = FindObjectOfType<MainSceneManager>();
        if (_mainSceneManager == null)
        {
            Debug.LogError("MainSceneManager not found!");
        }
    }

    protected virtual void OnEnable()
    {
        Hp = 1;
        Speed = 5.0f;
        _attackTimer = 0f;
        _isDead = false;
        _isHit = false;
        _animator.SetBool("isDestroyed", false);
        _rigidbody.velocity = Vector2.left.normalized;
        AdjustDifficulty();
    }

    protected virtual void FixedUpdate()
    {
        _attackTimer += Time.fixedDeltaTime;
        if (_attackTimer >= BaseAttackInterval)
        {
            Attack();
            _attackTimer = 0f;
        }
        UpdateAnimator();
    }

    public abstract void Attack();

    protected void UpdateAnimator()
    {
        bool isMoving = _rigidbody.velocity.magnitude > 0.1f;
        _animator.SetBool("isMoving", isMoving);

        if (_isHit)
        {
            Debug.Log("Setting isHit to true in animator");
            _animator.SetBool("isHit", true);
        }
        else
        {
            Debug.Log("Setting isHit to false in animator");
            _animator.SetBool("isHit", false);
        }

        // Debug 현재 상태
        var currentState = _animator.GetCurrentAnimatorStateInfo(0);
        Debug.Log("Current Animator State: " + currentState.fullPathHash + " (" + currentState.IsName("boss_hit") + ")");
    }

    public bool OnDead()
    {
        if (_isDead) return false;

        _isDead = true;
        AudioManager.Instance.PlayExplosionSound();
        _mainSceneManager.AddScore(100);
        _animator.SetBool("isDestroyed", true);
        StartCoroutine(DestroyAfterAnimation());
        return true;
    }

    protected IEnumerator ResetHitState()
    {
        yield return new WaitForSeconds(1f);
        _isHit = false;
        _animator.SetBool("isHit", false);
    }

    protected IEnumerator DestroyAfterAnimation()
    {
        yield return new WaitForSeconds(_animator.GetCurrentAnimatorStateInfo(0).length);
        ObjectPoolManager.Instance.ReturnToPool(gameObject.name.Replace("(Clone)", "").Trim(), gameObject);
        Debug.Log(gameObject.name);
    }

    public void OnHit()
    {
        Hp--;
        _isHit = true;
        if (Hp < 0)
        {
            OnDead();
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & PlayerBulletLayer) != 0)
        {
            OnHit();
            ObjectPoolManager.Instance.ReturnToPool(other.tag, other.gameObject);
        }
        else if (((1 << other.gameObject.layer) & PlayerLayer) != 0)
        {
            OnDead();
        }
    }

    protected void AdjustDifficulty()
    {
        switch (DataManager.Instance.Difficult)
        {
            case 0:
                BaseAttackInterval *= 1.5f;
                Speed *= 0.6f;
                break;
            case 1:
                break;
            case 2:
                BaseAttackInterval *= 0.5f;
                Speed *= 1.4f;
                break;
        }
    }
}