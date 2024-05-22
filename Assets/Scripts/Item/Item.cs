using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
	public Item_type Type;
	public int Amount;
	public int Probability;
	public float Speed { get; private set; }
	Rigidbody2D _rigidbody2D;

	public Item(Item_type _type, int _amount)
	{
		Type = _type;
		Amount = _amount;
	}

	private void Awake()
	{
		_rigidbody2D = GetComponent<Rigidbody2D>();
	}

	private void Start()
	{
		_rigidbody2D.AddForce(new Vector2(-Speed, 0));
	}
	private void Update()
	{
		if(transform.position.x < -9)
		{
			Destroy(gameObject);
		}
	}

	//������ �Ҹ�. �ɷ�ġ�� ���� ��Ű�� �����
	public virtual void UseItem(PlayerInputController player)
	{

	}

	//ĳ���Ϳ� �浹�� �Ҹ�
	private void OnTriggerEnter2D(Collider2D collision)
	{
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            PlayerInputController player = collision.GetComponent<PlayerInputController>();
			AudioManager.Instance.PlayGetItemSound();
            if (player != null)
            {
                gameObject.SetActive(false);
                UseItem(player);
                Destroy(gameObject);
            }
        }
    }

	public void SetSpeed(float speed)
	{
		Speed = speed;
	}
}

public enum Item_type
{
	LIfe,
	MultiBullet,
	Shield,
	BiggerBullet
}
