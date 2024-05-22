using UnityEngine;

public class RegularSpreadAttackShip : BaseEnemy
{
    // SerializeField 멤버 변수 - 파스칼 사용
    [SerializeField] private float BurstInterval = 0.2f;
    [SerializeField] private int BurstCount = 3;
    [SerializeField] private float MultipleProjectilesAngle = 15f;
    [SerializeField] private int NumberOfProjectilesPerShot = 3;

    // Private 멤버 변수 - _(언더 스코어) + 카멜 사용
    private int _currentBurstCount;

    protected override void Awake()
    {
        base.Awake();
        BulletType = BulletType.Regular;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        _currentBurstCount = BurstCount;
    }

    protected override void FixedUpdate()
    {
        _attackTimer += Time.fixedDeltaTime;
        if (_currentBurstCount > 0 && _attackTimer >= BurstInterval)
        {
            Attack();
            _attackTimer = 0f;
            _currentBurstCount--;
        }
        else if (_currentBurstCount == 0 && _attackTimer >= BaseAttackInterval)
        {
            _attackTimer = 0f;
            _currentBurstCount = BurstCount;
        }
        UpdateAnimator();
    }

    public override void Attack()
    {
        float minAngle = -(NumberOfProjectilesPerShot / 2f) * MultipleProjectilesAngle + 0.5f * MultipleProjectilesAngle;

        for (int i = 0; i < NumberOfProjectilesPerShot; i++)
        {
            float angle = minAngle + MultipleProjectilesAngle * i;
            ShootBullet(angle);
        }
    }

    private void ShootBullet(float angle)
    {
        GameObject bullet = ObjectPoolManager.Instance.SpawnFromPool("Regular", FirePoint.position, FirePoint.rotation);
        RegularBullet regularBullet = bullet.GetComponent<RegularBullet>();
        if (regularBullet != null)
        {
            Vector2 direction = Quaternion.Euler(0, 0, angle) * Vector2.left;
            regularBullet.SetDirection(direction);
        }
    }
}