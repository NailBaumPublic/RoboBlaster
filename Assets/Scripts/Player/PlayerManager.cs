using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }
    private List<PBulletController> activeBullets = new List<PBulletController>();

    [SerializeField] private GameObject playerPrefab; // 인스턴스화할 프리팹
    [SerializeField] private GameObject player2Prefab;   // 두 번째 플레이어의 프리팹

    public int PlayerOnecurrentLifePoint = 0; // P1현재 생명
    public int PlayerTwocurrentLifePoint = 0; // P2현재 생명

    private int PlayerOneMaxLifePoint = 0; // P1최대 생명
    private int PlayerTwoMaxLifePoint = 0; // P2최대 생명

    [Header("3의 배수로 설정하시오")]
    public int PlayerOneSkillUses = 0; // P1 스킬 사용 가능 횟수 (기본값 설정 필요)
    public int PlayerTwoSkillUses = 0; // P2 스킬 사용 가능 횟수 (기본값 설정 필요)

    private GameObject player1Instance; // 인스턴스화된 player 객체
    private GameObject player2Instance;  // 두 번째 플레이어 인스턴스
    private GameObject currentlyRespawningPlayer1; // 1P를 위한 부활 필드
    private GameObject currentlyRespawningPlayer2; // 2P를 위한 부활 필드

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

        // 프리팹에서 player 인스턴스를 생성하고, 해당 인스턴스의 PlayerInputController 컴포넌트에 대한 참조를 설정
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
            inputController.isPlayerOne = isPlayerOne;  // 각 플레이어에 isPlayerOne 값을 설정
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
        Debug.Log("총알아 3방향");
        StartCoroutine(ActivateMultiShot(isPlayerOne));
    }
    private IEnumerator ActivateMultiShot(bool isPlayerOne)
    {
        PlayerInputController playerController = isPlayerOne ? player1Instance.GetComponent<PlayerInputController>() : player2Instance.GetComponent<PlayerInputController>();
        if (playerController != null)
        {
            playerController.isMultiShotActive = true;
            yield return new WaitForSeconds(5f); // 5초 동안 다중 발사 활성화
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
        // 태그검사
        string bulletTag = isPlayerOne ? "Player1Bullet" : "Player2Bullet";

        // 참조 오브젝트풀
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
                bullet.IncreaseSizeLevel(); // 총알의 크기 단계 증가
            }
        }

        // 이후 생성되는 총알들도 큰 크기로 발사되도록 설정
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
