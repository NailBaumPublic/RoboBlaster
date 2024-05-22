using UnityEngine;
using System.Collections; // 코루틴 사용시 꼭 필요한 유징문
using System.Collections.Generic;

public class PlayerBoomSkrill : MonoBehaviour
{
    [SerializeField] GameObject ShiledEffect;
    [SerializeField] private Transform playerBoomPoint;

    private TopDownController controller;
    private PlayerInputController inputController;
    private List<GameObject> currentEffectInstances = new List<GameObject>();


    private void Awake()
    {
        controller = GetComponent<TopDownController>();
        inputController = GetComponent<PlayerInputController>();
    }

    private void Start()
    {
        controller.OnSkillEvent += OnSkill;
        controller.OnDeathEvent += OnDeath; // 사망 이벤트 등록
    }

    private void OnSkill()
    {
        bool isPlayerOne = inputController.isPlayerOne; // 플레이어가 1P인지 2P인지 확인

        if (PlayerManager.Instance.CanUseSkill(isPlayerOne)) // 스킬 사용 가능 여부 확인
        {
            PlayerManager.Instance.UseSkill(isPlayerOne); // 스킬 사용 횟수 감소
            AudioManager.Instance.PlaySkillSound();
            StartCoroutine(CreateBoomAndDestroy()); // 코루틴 적용
        }
    }

    private IEnumerator CreateBoomAndDestroy() // 코루틴 적용
    {
        // 스킬 효과 생성
        GameObject effectInstance = Instantiate(ShiledEffect, playerBoomPoint.position, Quaternion.identity);
        currentEffectInstances.Add(effectInstance); // 리스트에 추가

        // 2초 동안 대기
        yield return new WaitForSeconds(2);

        // 생성된 스킬 효과 파괴
        Destroy(effectInstance);
        currentEffectInstances.Remove(effectInstance); // 리스트에서 제거
    }

    private void OnDeath()
    {
        // 모든 활성화된 스킬 효과 제거
        foreach (var effectInstance in currentEffectInstances)
        {
            if (effectInstance != null)
            {
                Destroy(effectInstance);
            }
        }
        currentEffectInstances.Clear(); // 리스트 초기화
    }
}
