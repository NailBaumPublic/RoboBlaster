using UnityEngine;

public class PBulletController : MonoBehaviour
{
    private Rigidbody2D rigidbody;

    [SerializeField] private LayerMask enemyLayer;

    public float speed = 20f;

    private bool isBigBullet = false; // �Ѿ��� ū�� ���θ� �����ϴ� �÷���
    public bool isPlayerOne; // P1 �Ǵ� P2 ����
    private int sizeLevel = 1; // �Ѿ� ũ�� �ܰ�

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
