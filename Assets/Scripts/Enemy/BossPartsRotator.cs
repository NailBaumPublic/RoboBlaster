using UnityEngine;

public class BossPartRotator : MonoBehaviour
{
    public Transform BossTransform;
    public float RotationSpeed = 5f;
    public float Radius = 3f;
    public float InitialAngleOffset = 0f;

    private float _angle;

    private void Start()
    {
        
        _angle = InitialAngleOffset;
    }

    private void Update()
    {
        _angle += RotationSpeed * Time.deltaTime;

        float rad = _angle * Mathf.Deg2Rad;

        Vector2 offset = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)) * Radius;
        transform.position = BossTransform.position + (Vector3)offset;
    }
}