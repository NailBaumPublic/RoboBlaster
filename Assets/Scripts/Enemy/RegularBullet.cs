using UnityEngine;

public class RegularBullet : BaseBullet
{
    protected override void Start()
    {
        base.Start();
        BulletType = BulletType.Regular;
    }

    public override void OnHit()
    {
        ObjectPoolManager.Instance.ReturnToPool("Regular", gameObject);
    }
}