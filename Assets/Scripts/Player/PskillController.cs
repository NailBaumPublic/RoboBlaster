using UnityEngine;

public class PskillController : MonoBehaviour
{
    // 삭제할 태그 목록
    private string[] enemyTags = { "Enemy1", "Enemy2", "Enemy3", "Enemy4", "Homing", "Regular" };

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DestroyIfEnemy(collision);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        DestroyIfEnemy(collision);
    }

    private void DestroyIfEnemy(Collider2D collision)
    {
        if (collision.gameObject.layer == 6 || collision.gameObject.layer == 8)
        {
            foreach (string tag in enemyTags)
            {
                if (collision.CompareTag(tag))
                {
                    collision.gameObject.SetActive(false);
                    break; // 태그가 맞으면 파괴하고 루프 종료
                }
            }
        }
    }
}
