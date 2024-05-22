using UnityEngine;

public class HomingAttackShip : BaseEnemy
{
    protected override void Awake()
    {
        base.Awake();
        BulletType = BulletType.Homing;
    }

    public override void Attack()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) return;

        Vector2 playerDirection = (player.transform.position - FirePoint.position).normalized;
        GameObject bullet = ObjectPoolManager.Instance.SpawnFromPool("Homing", FirePoint.position, FirePoint.rotation);
        HomingBullet homingBullet = bullet.GetComponent<HomingBullet>();
        if (homingBullet != null)
        {
            homingBullet.SetDirection(playerDirection);
        }
    }
}