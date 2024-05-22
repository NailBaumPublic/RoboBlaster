using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StartSceneManager : MonoBehaviour
{
	[SerializeField] GameObject ScorePrefab;
	[SerializeField] GameObject HighScorePopUp;
	[SerializeField] GameObject HighScoreTab;

	private string _scoreRecordFileName = "ScoreRecord.json";
	private string _scoreRecordFileNameMultiPlayer = "ScoreRecordMultiPlayer.json";

	public void ShowScores()
	{
		HighScorePopUp.SetActive(true);

		foreach (Transform child in HighScoreTab.transform)
		{
			Destroy(child.gameObject);
		}

		Dictionary<string, float> data = JsonSaveLoad.JsonLoad<Dictionary<string, float>>(_scoreRecordFileName);
		if (!DataManager.Instance.IsMulti)
		{
			data = JsonSaveLoad.JsonLoad<Dictionary<string, float>>(_scoreRecordFileNameMultiPlayer);
		}

		float y = 200f;
		if (data != null)
		{
			var sortedDictionary = data.OrderByDescending(x => x.Value);
			int i = 0;

			foreach (KeyValuePair<string, float> kvp in sortedDictionary)
			{
				if (i > 4) { break; } else { i++; }
				string key = kvp.Key;
				float score = kvp.Value;

				GameObject newPair = Instantiate(ScorePrefab, HighScoreTab.transform);
				newPair.transform.SetParent(HighScoreTab.transform, false);
				RectTransform rectTransform = newPair.GetComponent<RectTransform>();
				rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, y);
				newPair.GetComponent<ScorePrefab>().SetScore(key, score);
				y -= 100f;
			}
		}
	}
}
