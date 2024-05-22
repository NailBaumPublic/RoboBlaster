using UnityEngine;
using TMPro; // TextMeshPro ���ӽ����̽� ���

public class PlayerLifeUI : MonoBehaviour
{
    [SerializeField] GameObject playerUI;
    [SerializeField] GameObject player2UI;

    [SerializeField] private TextMeshProUGUI playerOneLifeText; // P1 ������ �ؽ�Ʈ
    [SerializeField] private TextMeshProUGUI playerTwoLifeText; // P2 ������ �ؽ�Ʈ

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
            // Player 1�� �������� ��ų ��� ���� ������Ʈ
            playerOneLifeText.text = "Player 1 Life: " + PlayerManager.Instance.PlayerOnecurrentLifePoint;
            playerOneSkillCountText.text = " " + PlayerManager.Instance.PlayerOneSkillUses / 3;

            // ��Ƽ�÷��̾� ��尡 Ȱ��ȭ�Ǿ� �ִ��� Ȯ��
            if (DataManager.Instance.IsMulti == false)
            {
                // ��Ƽ�÷��̾� ����� ��� Player 2�� ���� ���� ǥ��
                playerTwoLifeText.text = "Player 2 Life: " + PlayerManager.Instance.PlayerTwocurrentLifePoint;
                playerTwoSkillCountText.text = " " + PlayerManager.Instance.PlayerTwoSkillUses / 3;
            }
            else
            {
                // ��Ƽ�÷��̾� ��尡 �ƴ� ��� Player 2�� ������ 0���� ǥ��
                playerTwoLifeText.text = "Player 2 Life: 0";
                playerTwoSkillCountText.text = " 0";
            }
        }
    }
}
