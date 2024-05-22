using UnityEngine;

public class CameraBounds : MonoBehaviour
{
    private Camera camera;
    private Vector3 minBounds;
    private Vector3 maxBounds;
    private float playerHalfWidth;
    private float playerHalfHeight;

    private void Awake()
    {
        camera = Camera.main;

        // 플레이어의 콜라이더 크기를 기반으로 플레이어의 반 너비와 높이 계산
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        playerHalfWidth = spriteRenderer.bounds.extents.x;
        playerHalfHeight = spriteRenderer.bounds.extents.y;
    }

    private void LateUpdate()
    {
        // 카메라의 뷰포트 좌표를 월드 좌표로 변환하여 경계를 구한다.
        Vector3 cameraBottomLeft = camera.ViewportToWorldPoint(new Vector3(0, 0, camera.nearClipPlane));
        Vector3 cameraTopRight = camera.ViewportToWorldPoint(new Vector3(1, 1, camera.nearClipPlane));

        minBounds = cameraBottomLeft;
        maxBounds = cameraTopRight;

        // 플레이어의 현재 위치를 가져온다.
        Vector3 playerPosition = transform.position;

        // 플레이어의 위치를 카메라의 경계 내로 제한
        playerPosition.x = Mathf.Clamp(playerPosition.x, minBounds.x + playerHalfWidth, maxBounds.x - playerHalfWidth);
        playerPosition.y = Mathf.Clamp(playerPosition.y, minBounds.y + playerHalfHeight, maxBounds.y - playerHalfHeight);

        // 제한된 위치로 플레이어를 이동
        transform.position = playerPosition;
    }
}
