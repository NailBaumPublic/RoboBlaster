using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }
    private List<PBulletController> activeBullets = new List<PBulletController>();

    [SerializeField] private GameObject playerPrefab; // �ν��Ͻ�ȭ�� ������
    [SerializeField] private GameObject player2Prefab;   // �� ��° �÷��̾��� ������

    public int PlayerOnecurrentLifePoint = 0; // P1���� ����
    public int PlayerTwocurrentLifePoint = 0; // P2���� ����

    private int PlayerOneMaxLifePoint = 0; // P1�ִ� ����
    private int PlayerTwoMaxLifePoint = 0; // P2�ִ� ����

    [Header("3�� ����� �����Ͻÿ�")]
    public int PlayerOneSkillUses = 0; // P1 ��ų ��� ���� Ƚ�� (�⺻�� ���� �ʿ�)
    public int PlayerTwoSkillUses = 0; // P2 ��ų ��� ���� Ƚ�� (�⺻�� ���� �ʿ�)

    private GameObject player1Instance; // �ν��Ͻ�ȭ�� player ��ü
    private GameObject player2Instance;  // �� ��° �÷��̾� �ν��Ͻ�
    private GameObject currentlyRespawningPlayer1; // 1P�� ���� ��Ȱ �ʵ�
    private GameObject currentlyRespawningPlayer2; // 2P�� ���� ��Ȱ �ʵ�

    private DataManager dataManager;

    public MainSceneManager mainSceneManager;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        dataManager = DataManager.Instance;
    }

    private void Start()
    {

        // �����տ��� player �ν��Ͻ��� �����ϰ�, �ش� �ν��Ͻ��� PlayerInputController ������Ʈ�� ���� ������ ����
        player1Instance = Instantiate(playerPrefab, Vector2.left * 7.6f, Quaternion.identity);
        SetupPlayer(player1Instance, true);

        PlayerInputController inputController = player1Instance.GetComponent<PlayerInputController>();
        if (dataManager.IsMulti == false)
        {
            player2Instance = Instantiate(player2Prefab, Vector2.left * 7.6f, Quaternion.identity);
            SetupPlayer(player2Instance, false);
        }
    }

    private void SetupPlayer(GameObject player, bool isPlayerOne)
    {
        PlayerInputController inputController = player.GetComponent<PlayerInputController>();
        if (inputController != null)
        {
            inputController.pManager = this;
            inputController.isPlayerOne = isPlayerOne;  // �� �÷��̾ isPlayerOne ���� ����
        }
    }

    public void RespawnPlayer(GameObject player, bool isPlayerOne)
    {
        if (isPlayerOne)
        {
            currentlyRespawningPlayer1 = player;
            Invoke("RespawnPlayerExe1", 2);
        }
        else
        {
            currentlyRespawningPlayer2 = player;
            Invoke("RespawnPlayerExe2", 2);
        }
    }

    private void RespawnPlayerExe1()
    {
        if (PlayerOnecurrentLifePoint > PlayerOneMaxLifePoint)
        {
            PlayerOnecurrentLifePoint--;
            if (currentlyRespawningPlayer1 != null)
            {
                currentlyRespawningPlayer1.transform.position = Vector2.left * 7.6f;
                currentlyRespawningPlayer1.SetActive(true);
                currentlyRespawningPlayer1 = null;
            }
        }
        else
        {
            mainSceneManager.GameEnd();
        }
    }

    private void RespawnPlayerExe2()
    {
        if (PlayerTwocurrentLifePoint > PlayerTwoMaxLifePoint)
        {
            PlayerTwocurrentLifePoint--;
            if (currentlyRespawningPlayer2 != null)
            {
                currentlyRespawningPlayer2.transform.position = Vector2.left * 7.6f;
                currentlyRespawningPlayer2.SetActive(true);
                currentlyRespawningPlayer2 = null;
            }
        }
        else
        {
            mainSceneManager.GameEnd();
        }
    }

    public void IncreaseLife(bool isPlayerOne)
    {
        if (isPlayerOne)
        {
            PlayerOnecurrentLifePoint += 1;
        }
        else
        {
            PlayerTwocurrentLifePoint += 1;
        }
    }

    public bool CanUseSkill(bool isPlayerOne)
    {
        return isPlayerOne ? PlayerOneSkillUses > 0 : PlayerTwoSkillUses > 0;
    }

    public void UseSkill(bool isPlayerOne)
    {
        if (CanUseSkill(isPlayerOne))
        {
            if (isPlayerOne)
            {
                PlayerOneSkillUses -= 1;
            }
            else
            {
                PlayerTwoSkillUses -= 1;
            }
        }
    }

    public void EnableMultiBulletPotion(bool isPlayerOne)
    {
        Debug.Log("�Ѿ˾� 3����");
        StartCoroutine(ActivateMultiShot(isPlayerOne));
    }
    private IEnumerator ActivateMultiShot(bool isPlayerOne)
    {
        PlayerInputController playerController = isPlayerOne ? player1Instance.GetComponent<PlayerInputController>() : player2Instance.GetComponent<PlayerInputController>();
        if (playerController != null)
        {
            playerController.isMultiShotActive = true;
            yield return new WaitForSeconds(5f); // 5�� ���� ���� �߻� Ȱ��ȭ
            playerController.isMultiShotActive = false;
        }
    }

    public void ActivateShieldPotion(bool isPlayerOne)
    {
        if (isPlayerOne)
        {
            PlayerOneSkillUses += 3;
        }
        else
        {
            PlayerTwoSkillUses += 3;
        }
    }

    public void RegisterBullet(PBulletController bullet)
    {
        if (!activeBullets.Contains(bullet))
        {
            activeBullets.Add(bullet);
        }
    }

    public void UnregisterBullet(PBulletController bullet)
    {
        if (activeBullets.Contains(bullet))
        {
            activeBullets.Remove(bullet);
        }
    }
    public void EnableBiggerBulletPotion(bool isPlayerOne)
    {
        // �±װ˻�
        string bulletTag = isPlayerOne ? "Player1Bullet" : "Player2Bullet";

        // ���� ������ƮǮ
        List<GameObject> bullets = ObjectPoolManager.Instance.GetAllObjectsInPool(bulletTag);
        if (bullets == null || bullets.Count == 0)
        {
            return;
        }

        foreach (var bulletObj in bullets)
        {
            PBulletController bullet = bulletObj.GetComponent<PBulletController>();
            if (bullet != null)
            {
                bullet.IncreaseSizeLevel(); // �Ѿ��� ũ�� �ܰ� ����
            }
        }

        // ���� �����Ǵ� �Ѿ˵鵵 ū ũ��� �߻�ǵ��� ����
        GameObject playerInstance = isPlayerOne ? player1Instance : player2Instance;
        if (playerInstance != null)
        {
            PlayerInputController playerController = playerInstance.GetComponent<PlayerInputController>();
            if (playerController != null)
            {
                var bulletController = playerController.GetComponentInChildren<PBulletController>();
                if (bulletController != null)
                {
                    bulletController.IncreaseSizeLevel();
                }
            }
        }
    }
}
