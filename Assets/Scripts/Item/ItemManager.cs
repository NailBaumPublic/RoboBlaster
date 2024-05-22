using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
	public float GenerationWaitTime { get; private set; } = 5f;
	public float ItemSpeed { get; private set; } = 75f;

	[SerializeField] Item[] Items;
    [SerializeField] MainSceneManager MainSceneManager;

	private float maxGenerationHeight = 4.5f;
	private float minGenerationHeight = -4.5f;

	private void Start()
	{
		StartCoroutine(RunItemGenerater());
	}

	//�ֱ������� ������ ����
	IEnumerator RunItemGenerater()
	{
        while (true)
        {
			GenerateItems();
			yield return new WaitForSeconds(GenerationWaitTime);
		}
	}

	//������ ���̿� ������ ������ ����
	private void GenerateItems()
    {
		int itemProb = UnityEngine.Random.Range(0, 100);//���� ������
		float height = UnityEngine.Random.Range(minGenerationHeight, maxGenerationHeight);//���� ����
        Vector3 spawnPosition = new Vector3(transform.position.x, height, 0);

		foreach(var item in Items)
		{
			itemProb -= item.Probability;
			if(itemProb <= 0)
			{
				Item newItem = Instantiate(item, spawnPosition, Quaternion.identity, transform);
				newItem.SetSpeed(ItemSpeed);
				break;
			}
		}
    }

	//���� �ֱ� ���ð� ����
	public void SetGenerationWaitTime(float time)
	{
		GenerationWaitTime = time;
	}

	//������ ���󰡴� �ӵ� ����
	public void SetItemSpeed(float speed)
	{
		ItemSpeed = speed;
	}
}
