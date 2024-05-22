using UnityEngine;

public class RegularAttackShip : BaseEnemy
{
    protected override void Awake()
    {
        base.Awake();
        BulletType = BulletType.Regular;
    }

    public override void Attack()
    {
        GameObject bullet = ObjectPoolManager.Instance.SpawnFromPool("Regular", FirePoint.position, FirePoint.rotation);
        RegularBullet regularBullet = bullet.GetComponent<RegularBullet>();
        if (regularBullet != null)
        {
            regularBullet.SetDirection(Vector2.left);
        }
    }
}