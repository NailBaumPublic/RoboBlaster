using UnityEngine;
using TMPro; // TextMeshPro 네임스페이스 사용

public class PlayerLifeUI : MonoBehaviour
{
    [SerializeField] GameObject playerUI;
    [SerializeField] GameObject player2UI;

    [SerializeField] private TextMeshProUGUI playerOneLifeText; // P1 라이프 텍스트
    [SerializeField] private TextMeshProUGUI playerTwoLifeText; // P2 라이프 텍스트

    [SerializeField] private TextMeshProUGUI playerOneSkillCountText;
    [SerializeField] private TextMeshProUGUI playerTwoSkillCountText;

    private void Start()
    {
        if (DataManager.Instance.IsMulti)
        {
            playerUI.SetActive(true);
        }
        else if (!DataManager.Instance.IsMulti)
        {
            playerUI.SetActive(true);
            player2UI.SetActive(true);
        }
    }
    private void Update()
    {
        if (PlayerManager.Instance != null)
        {
            // Player 1의 라이프와 스킬 사용 정보 업데이트
            playerOneLifeText.text = "Player 1 Life: " + PlayerManager.Instance.PlayerOnecurrentLifePoint;
            playerOneSkillCountText.text = " " + PlayerManager.Instance.PlayerOneSkillUses / 3;

            // 멀티플레이어 모드가 활성화되어 있는지 확인
            if (DataManager.Instance.IsMulti == false)
            {
                // 멀티플레이어 모드일 경우 Player 2의 실제 정보 표시
                playerTwoLifeText.text = "Player 2 Life: " + PlayerManager.Instance.PlayerTwocurrentLifePoint;
                playerTwoSkillCountText.text = " " + PlayerManager.Instance.PlayerTwoSkillUses / 3;
            }
            else
            {
                // 멀티플레이어 모드가 아닐 경우 Player 2의 정보를 0으로 표시
                playerTwoLifeText.text = "Player 2 Life: 0";
                playerTwoSkillCountText.text = " 0";
            }
        }
    }
}
