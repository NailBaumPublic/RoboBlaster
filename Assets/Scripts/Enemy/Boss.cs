using System.Collections;
using UnityEngine;

public class Boss : BaseEnemy
{
    // SerializeField 멤버 변수 - 파스칼 사용
    [SerializeField] private GameObject HomingBulletPrefab;
    [SerializeField] private GameObject RegularBulletPrefab;
    [SerializeField] private GameObject LaserBeam;
    [SerializeField] private Transform[] FirePoints;
    [SerializeField] private float RegularSpreadAttackInterval = 3.0f;
    [SerializeField] private float HomingSpreadAttackInterval = 4.0f;
    [SerializeField] private float LaserAttackInterval = 6.0f;
    [SerializeField] private float BurstInterval = 0.2f;
    [SerializeField] private int BurstCount = 3;
    [SerializeField] private float MultipleProjectilesAngle = 15f;
    [SerializeField] private int NumberOfProjectilesPerShot = 3;
    [SerializeField] private float MoveDuration = 1f;

    // Private 멤버 변수 - _(언더 스코어) + 카멜 사용
    private int _currentBurstCount;

    private float _minX = 5f;
    private float _maxX = 9f;
    private float _minY = -5f;
    private float _maxY = 5f;

    private Vector2 _targetPosition;
    private bool _isMoving = false;

    protected override void Awake()
    {
        base.Awake();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        Hp = 30;
        _isDead = false;
        _isHit = false;
        _attackTimer = 0f;
        _currentBurstCount = BurstCount;
        _animator.SetBool("isDestroyed", false);
        LaserBeam.SetActive(false);

        StartCoroutine(MovePattern());
        StartCoroutine(HomingSpreadAttackRoutine());
        StartCoroutine(RegularSpreadAttackRoutine());
        StartCoroutine(LaserAttackRoutine());
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (_isMoving)
        {
            Vector2 newPosition = Vector2.MoveTowards(_rigidbody.position, _targetPosition, Speed * Time.fixedDeltaTime);
            newPosition.x = Mathf.Clamp(newPosition.x, _minX, _maxX);
            newPosition.y = Mathf.Clamp(newPosition.y, _minY, _maxY);

            _rigidbody.MovePosition(newPosition);

            if (Vector2.Distance(_rigidbody.position, _targetPosition) < 0.1f)
            {
                _isMoving = false;
                _rigidbody.velocity = Vector2.zero;
            }
        }
    }

    private IEnumerator MovePattern()
    {
        Vector2[] directions = new Vector2[] {
            Vector2.up, Vector2.down, Vector2.left, Vector2.right
        };

        while (!_isDead)
        {
            Vector2 moveDirection = directions[Random.Range(0, directions.Length)];
            _targetPosition = (Vector2)transform.position + moveDirection * Speed * MoveDuration;

            _targetPosition.x = Mathf.Clamp(_targetPosition.x, _minX, _maxX);
            _targetPosition.y = Mathf.Clamp(_targetPosition.y, _minY, _maxY);

            _isMoving = true;
            yield return new WaitForSeconds(MoveDuration + BaseAttackInterval);
        }
    }

    private IEnumerator HomingSpreadAttackRoutine()
    {
        while (!_isDead)
        {
            yield return new WaitForSeconds(HomingSpreadAttackInterval);
            if (!_isDead)
            {
                StartCoroutine(HomingSpreadAttack());
            }
        }
    }

    private IEnumerator RegularSpreadAttackRoutine()
    {
        while (!_isDead)
        {
            yield return new WaitForSeconds(RegularSpreadAttackInterval);
            if (!_isDead)
            {
                StartCoroutine(RegularSpreadAttack());
            }
        }
    }

    private IEnumerator LaserAttackRoutine()
    {
        while (!_isDead)
        {
            yield return new WaitForSeconds(LaserAttackInterval);
            if (!_isDead)
            {
                StartCoroutine(LaserAttack());
            }
        }
    }

    private IEnumerator HomingSpreadAttack()
    {
        for (int j = 0; j < BurstCount; j++)
        {
            if (_isDead) yield break;

            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player == null) yield break;

            Vector2 playerDirection = (player.transform.position - FirePoints[0].position).normalized;

            float minAngle = -(NumberOfProjectilesPerShot / 2f) * MultipleProjectilesAngle + 0.5f * MultipleProjectilesAngle;
            for (int i = 0; i < NumberOfProjectilesPerShot; i++)
            {
                if (_isDead) yield break;

                float angle = minAngle + MultipleProjectilesAngle * i;
                Vector2 spreadDirection = Quaternion.Euler(0, 0, angle) * playerDirection;
                ShootHomingBullet(spreadDirection);
            }
            yield return new WaitForSeconds(BurstInterval);
        }
    }

    private IEnumerator RegularSpreadAttack()
    {
        for (int j = 0; j < BurstCount; j++)
        {
            if (_isDead) yield break;

            float minAngle = -(NumberOfProjectilesPerShot / 2f) * MultipleProjectilesAngle + 0.5f * MultipleProjectilesAngle;

            for (int i = 0; i < NumberOfProjectilesPerShot; i++)
            {
                if (_isDead) yield break;

                float angle = minAngle + MultipleProjectilesAngle * i;
                ShootRegularBullet(angle);
            }
            yield return new WaitForSeconds(BurstInterval);
        }
    }

    private IEnumerator LaserAttack()
    {
        if (_isDead) yield break;

        LaserBeam.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        LaserBeam.SetActive(false);
    }

    private void ShootHomingBullet(Vector2 direction)
    {
        GameObject bullet = ObjectPoolManager.Instance.SpawnFromPool("Homing", FirePoints[0].position, FirePoints[0].rotation);
        HomingBullet homingBullet = bullet.GetComponent<HomingBullet>();
        if (homingBullet != null)
        {
            homingBullet.SetDirection(direction);
        }
    }

    private void ShootRegularBullet(float angle)
    {
        GameObject bullet = ObjectPoolManager.Instance.SpawnFromPool("Regular", FirePoints[1].position, FirePoints[1].rotation);
        RegularBullet regularBullet = bullet.GetComponent<RegularBullet>();
        if (regularBullet != null)
        {
            Vector2 direction = Quaternion.Euler(0, 0, angle) * Vector2.left;
            regularBullet.SetDirection(direction);
        }
    }

    public override void Attack()
    {
    }
}
