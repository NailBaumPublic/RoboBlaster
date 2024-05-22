using UnityEngine;

public class PBulletController : MonoBehaviour
{
    private Rigidbody2D rigidbody;

    [SerializeField] private LayerMask enemyLayer;

    public float speed = 20f;

    private bool isBigBullet = false; // 총알이 큰지 여부를 저장하는 플래그
    public bool isPlayerOne; // P1 또는 P2 구분
    private int sizeLevel = 1; // 총알 크기 단계

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        rigidbody.velocity = Vector2.right * speed;
        if (PlayerManager.Instance != null)
        {
            PlayerManager.Instance.RegisterBullet(this);
        }
        UpdateBulletSize();
    }

    private void Update()
    {
        if (transform.position.x > 11.0f)
        { 
            ObjectPoolManager.Instance.ReturnToPool(tag, gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & enemyLayer) != 0)
        {
            ObjectPoolManager.Instance.ReturnToPool(gameObject.tag, gameObject);
        }
    }

    public void SetBigBullet(bool isBig)
    {
        isBigBullet = isBig;
        UpdateBulletSize();
    }
    public void IncreaseSizeLevel()
    {
        sizeLevel++;
        UpdateBulletSize();
    }

    private void UpdateBulletSize()
    {
        transform.localScale = new Vector3(2f, 1f * sizeLevel, 1f);
    }
}
