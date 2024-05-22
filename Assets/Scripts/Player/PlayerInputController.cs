using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : TopDownController
{
    private Animator animator; // Animator 컴포넌트 참조 추가

    public PlayerManager pManager;
    public bool isPlayerOne;  // 플레이어가 1P인지 2P인지 구분하는 필드
    public bool isMultiShotActive = false; // 다중 발사 모드 활성화 여부
    private float attackCooldown = 0.2f; // 공격 간격
    private float attackTimer = 0f; // 공격 타이머
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private LayerMask enemyBullet;

    private bool skillButtonPressed = false;

    private void Awake()
    {
        animator = GetComponent<Animator>(); // Animator 컴포넌트 가져오기
    }

    private void Update()
    {
        if (isAttacking)
        {
            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0f)
            {
                FireBullet(Vector2.right);
                if (isMultiShotActive)
                {
                    FireBullet(Vector2.right + Vector2.up * 0.2f);
                    FireBullet(Vector2.right + Vector2.down * 0.2f);
                }
                attackTimer = attackCooldown;
            }
        }
    }

    public void OnFire(InputValue value)
    {
        isAttacking = value.isPressed;
        if (isAttacking)
        {
            FireBullet();
            if (isMultiShotActive)
            {
                FireBullet(Vector2.right + Vector2.up * 0.2f);
                FireBullet(Vector2.right + Vector2.down * 0.2f);
            }
        }
    }
    private void FireBullet(Vector2 direction = default)
    {
        if (direction == default)
        {
            direction = Vector2.right;
        }

        var bullet = ObjectPoolManager.Instance.SpawnFromPool(isPlayerOne ? "Player1Bullet" : "Player2Bullet", transform.position, Quaternion.identity);
        if (bullet != null)
        {
            var bulletController = bullet.GetComponent<PBulletController>();
            if (bulletController != null)
            {
                bulletController.isPlayerOne = isPlayerOne;
                bulletController.GetComponent<Rigidbody2D>().velocity = direction * bulletController.speed;
            }
            AudioManager.Instance.PlayShootSound(); // 총알 발사 사운드 재생
        }
    }
    public void OnSkill(InputValue value)
    {
        if (value.isPressed && !skillButtonPressed)
        {
            skillButtonPressed = true;
            isSkill = true;
        }
        else if (!value.isPressed)
        {
            skillButtonPressed = false;
            ResetSkillUsed(); // 버튼을 뗐을 때 스킬 사용 가능하도록 재설정
        }
    }


    public void OnMove(InputValue value)
    {
        Vector2 moveInput = value.Get<Vector2>().normalized;
        CallMoveEvent(moveInput);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            animator.SetTrigger("isHit");
            StartCoroutine(DeactiveAfterAnime());
        }

        if (collision.gameObject.layer == 8)
        {
            animator.SetTrigger("isHit");
            StartCoroutine(DeactiveAfterAnime());
        }
    }

    private IEnumerator DeactiveAfterAnime()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        CallMoveEvent(Vector2.zero);
        isAttacking = false;
        AudioManager.Instance.PlayExplosionSound();
        gameObject.SetActive(false);

        CallDeathEvent(); // 사망 이벤트 호출

        pManager.RespawnPlayer(this.gameObject, isPlayerOne);

    }
}
