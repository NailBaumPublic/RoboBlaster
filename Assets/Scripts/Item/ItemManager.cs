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

	//주기적으로 아이템 생성
	IEnumerator RunItemGenerater()
	{
        while (true)
        {
			GenerateItems();
			yield return new WaitForSeconds(GenerationWaitTime);
		}
	}

	//랜덤한 높이에 랜덤한 아이템 생성
	private void GenerateItems()
    {
		int itemProb = UnityEngine.Random.Range(0, 100);//랜덤 아이템
		float height = UnityEngine.Random.Range(minGenerationHeight, maxGenerationHeight);//랜덤 높이
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

	//생성 주기 대기시간 설정
	public void SetGenerationWaitTime(float time)
	{
		GenerationWaitTime = time;
	}

	//아이템 날라가는 속도 조절
	public void SetItemSpeed(float speed)
	{
		ItemSpeed = speed;
	}
}
