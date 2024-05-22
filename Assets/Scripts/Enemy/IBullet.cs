using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletType
{
    Regular,
    Homing,
    LaserBeam
}
public interface IBullet
{
    float Speed { get; set; }
    BulletType BulletType { get; set; }
    void OnHit();
    void SetDirection(Vector2 direction);
}