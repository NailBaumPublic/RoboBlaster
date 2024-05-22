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
		//초당 점수 추가
		AddScore(_scorePerSecond * Time.deltaTime);
	}

	//게임 종료시 호출되는 점수 저장 함수
	//게임 종료 판넬 출력
	//멀티 플레이어시 한명만 죽으면 안끝남
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

	//점수 추가 함수
	//최고기록을 넘을 경우 갱신
	//게임 종료시 증가 안함
	//스코어보드 업데이트
	public void AddScore(float Score)
	{		
		if (IsOver) return;		
		CurrentScore += Score;
		if (CurrentScore > MaxScore) MaxScore = CurrentScore;
		CurrentScoreBoard.text = "Score : " + CurrentScore.ToString("N0");
	}

	/*플레이어 점수를 기록
	 * 기존 기록 목록을 가져오고 거기에 새로운 기록으로 플레이어 이름과 현재 점수를 추가
	 * 만약 이미 존재하는 플레이어라면 기존 점수와 현재 점수를 비교해서 높은 값을 저장
	 * 멀티 플레이어는 따로 저장
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

	//멀티 플레이어인지 할당
	public void SetMultiPlayer(bool ismulti)
	{
		IsMultiPlayer = ismulti;
	}
}
 