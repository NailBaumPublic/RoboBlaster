using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    private TopDownController controller;

    [SerializeField] private Transform playerBulletSpawnPoint;
    [SerializeField] private string bulletTag;
    //[SerializeField] GameObject playerBullet;

    private void Awake()
    {
        controller = GetComponent<TopDownController>();
    }

    private void Start()
    {
        controller.OnFireEvent += OnShoot;
    }

    private void OnShoot()
    {
        CreatProjectile();
    }

    private void CreatProjectile()
    {
        GameObject bullet = ObjectPoolManager.Instance.SpawnFromPool(bulletTag, playerBulletSpawnPoint.position, Quaternion.identity);
        AudioManager.Instance.PlayShootSound();
    }
}
