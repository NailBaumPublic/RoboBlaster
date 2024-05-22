using UnityEngine;
using System.Collections; // �ڷ�ƾ ���� �� �ʿ��� ��¡��
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
        controller.OnDeathEvent += OnDeath; // ��� �̺�Ʈ ���
    }

    private void OnSkill()
    {
        bool isPlayerOne = inputController.isPlayerOne; // �÷��̾ 1P���� 2P���� Ȯ��

        if (PlayerManager.Instance.CanUseSkill(isPlayerOne)) // ��ų ��� ���� ���� Ȯ��
        {
            PlayerManager.Instance.UseSkill(isPlayerOne); // ��ų ��� Ƚ�� ����
            AudioManager.Instance.PlaySkillSound();
            StartCoroutine(CreateBoomAndDestroy()); // �ڷ�ƾ ����
        }
    }

    private IEnumerator CreateBoomAndDestroy() // �ڷ�ƾ ����
    {
        // ��ų ȿ�� ����
        GameObject effectInstance = Instantiate(ShiledEffect, playerBoomPoint.position, Quaternion.identity);
        currentEffectInstances.Add(effectInstance); // ����Ʈ�� �߰�

        // 2�� ���� ���
        yield return new WaitForSeconds(2);

        // ������ ��ų ȿ�� �ı�
        Destroy(effectInstance);
        currentEffectInstances.Remove(effectInstance); // ����Ʈ���� ����
    }

    private void OnDeath()
    {
        // ��� Ȱ��ȭ�� ��ų ȿ�� ����
        foreach (var effectInstance in currentEffectInstances)
        {
            if (effectInstance != null)
            {
                Destroy(effectInstance);
            }
        }
        currentEffectInstances.Clear(); // ����Ʈ �ʱ�ȭ
    }
}
