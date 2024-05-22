using UnityEngine;

public class HomingBullet : BaseBullet
{
    protected override void Start()
    {
        base.Start();
        BulletType = BulletType.Homing;
    }

    public override void OnHit()
    {
        ObjectPoolManager.Instance.ReturnToPool("Homing", gameObject);
    }
}