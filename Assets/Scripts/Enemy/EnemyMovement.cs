using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : TopDownController
{
    [SerializeField] private float Speed;
    [SerializeField] private float DestructionMargin = 0.1f;

    private Camera _mainCamera;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _rigidbody.velocity = Vector2.left.normalized * Speed;
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        CheckIfOutOfBounds();
    }

    private void CheckIfOutOfBounds()
    {
        Vector3 viewportPosition = _mainCamera.WorldToViewportPoint(transform.position);

        if (viewportPosition.x < -DestructionMargin || viewportPosition.x > 1 + DestructionMargin ||
            viewportPosition.y < -DestructionMargin || viewportPosition.y > 1 + DestructionMargin)
        {
            ObjectPoolManager.Instance.ReturnToPool(tag, gameObject);
        }
    }
}