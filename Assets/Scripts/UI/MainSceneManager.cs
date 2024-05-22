using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainSceneManager : MonoBehaviour
{
	public float CurrentScore { get; private set; }
	public float MaxScore {  get; private set; }
	public string PlayerName { get; private set; } = "DefaultName";
	public bool IsOver { get; private set; } = false;
	public bool IsMultiPlayer { get; private set; } = false;

	private int _scorePerSecond = 1;
	private string _scoreRecordFileName = "ScoreRecord.json";
	private string _scoreRecordFileNameMultiPlayer = "ScoreRecordMultiPlayer.json";
	private int _deadPlayer = 0;

	[SerializeField] GameObject EndPanel;
	[SerializeField] TextMeshProUGUI CurrentScoreBoard;
	[SerializeField] TextMeshProUGUI EndPanelScore;

	private void Awake()
	{
		MaxScore = LoadPlayerScore(PlayerName);
		CurrentScore = 0;
	}

	private void Start()
	{		
		SetMultiPlayer(DataManager.Instance.IsMulti);
		PlayerName = DataManager.Instance.PlayerName;
	}

	private void Update()
	{
		//�ʴ� ���� �߰�
		AddScore(_scorePerSecond * Time.deltaTime);
	}

	//���� ����� ȣ��Ǵ� ���� ���� �Լ�
	//���� ���� �ǳ� ���
	//��Ƽ �÷��̾�� �Ѹ� ������ �ȳ���
	public void GameEnd()
	{
		if (!IsMultiPlayer)
		{
			if(_deadPlayer == 0) { _deadPlayer++; return; }
		}
		IsOver = true;
		EndPanel.SetActive(true);
		EndPanelScore.text = CurrentScoreBoard.text;
		SavePlayerScore();
	}

	//���� �߰� �Լ�
	//�ְ����� ���� ��� ����
	//���� ����� ���� ����
	//���ھ�� ������Ʈ
	public void AddScore(float Score)
	{		
		if (IsOver) return;		
		CurrentScore += Score;
		if (CurrentScore > MaxScore) MaxScore = CurrentScore;
		CurrentScoreBoard.text = "Score : " + CurrentScore.ToString("N0");
	}

	/*�÷��̾� ������ ���
	 * ���� ��� ����� �������� �ű⿡ ���ο� ������� �÷��̾� �̸��� ���� ������ �߰�
	 * ���� �̹� �����ϴ� �÷��̾��� ���� ������ ���� ������ ���ؼ� ���� ���� ����
	 * ��Ƽ �÷��̾�� ���� ����
	*/
	public void SavePlayerScore()
	{
		
		Dictionary<string, float> data = JsonSaveLoad.JsonLoad< Dictionary<string, float>>(_scoreRecordFileName);
		if (!IsMultiPlayer) { data = JsonSaveLoad.JsonLoad<Dictionary<string, float>>(_scoreRecordFileNameMultiPlayer); }
		if (data != null)
		{
			if (data.ContainsKey(PlayerName))
			{
				if(CurrentScore > data[PlayerName])
				{
					data[PlayerName] = CurrentScore;
				}
			}
			else
			{
				data.Add(PlayerName, CurrentScore);
			}
		}
		else
		{
			data = new Dictionary<string, float>
			{
				{ PlayerName, CurrentScore }
			};
		}
		if (!IsMultiPlayer)
		{
			JsonSaveLoad.JsonSave(data, _scoreRecordFileNameMultiPlayer);
		}
		else
		{
			JsonSaveLoad.JsonSave(data, _scoreRecordFileName);
		}
	}

	public float LoadPlayerScore(string name)
	{
		float score = 0;
		Dictionary<string, float> data = JsonSaveLoad.JsonLoad<Dictionary<string, float>>(_scoreRecordFileName);
		if (!IsMultiPlayer) { data = JsonSaveLoad.JsonLoad<Dictionary<string, float>>(_scoreRecordFileNameMultiPlayer); }

		if (data != null)
		{
			if (data.ContainsKey(name))
			{
				score = data[name];
			}
		}
		return score;
	}

	//��Ƽ �÷��̾����� �Ҵ�
	public void SetMultiPlayer(bool ismulti)
	{
		IsMultiPlayer = ismulti;
	}
}
 