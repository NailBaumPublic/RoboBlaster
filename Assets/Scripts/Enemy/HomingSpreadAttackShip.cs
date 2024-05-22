using System.Collections;
using UnityEngine;

public class HomingSpreadAttackShip : BaseEnemy
{
    [SerializeField] private float BurstInterval = 0.2f;
    [SerializeField] private int BurstCount = 3;
    [SerializeField] private float MultipleProjectilesAngle = 15f;
    [SerializeField] private int NumberOfProjectilesPerShot = 3;

    private int _currentBurstCount;

    protected override void Awake()
    {
        base.Awake();
        BulletType = BulletType.Homing;
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
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) return;

        Vector2 playerDirection = (player.transform.position - FirePoint.position).normalized;
        ShootBullet(playerDirection);

        float minAngle = -(NumberOfProjectilesPerShot / 2f) * MultipleProjectilesAngle + 0.5f * MultipleProjectilesAngle;

        for (int i = 0; i < NumberOfProjectilesPerShot; i++)
        {
            float angle = minAngle + MultipleProjectilesAngle * i;
            Vector2 spreadDirection = Quaternion.Euler(0, 0, angle) * playerDirection;
            ShootBullet(spreadDirection);
        }
    }

    private void ShootBullet(Vector2 direction)
    {
        GameObject bullet = ObjectPoolManager.Instance.SpawnFromPool("Homing", FirePoint.position, FirePoint.rotation);
        HomingBullet homingBullet = bullet.GetComponent<HomingBullet>();
        if (homingBullet != null)
        {
            homingBullet.SetDirection(direction);
        }
    }
}