public interface IEnemy
{
    int Hp { get; set; }
    float Speed { get; set; }
    BulletType BulletType { get; set; }
    void OnHit();
    bool OnDead();
}