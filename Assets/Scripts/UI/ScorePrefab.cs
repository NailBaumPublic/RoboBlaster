using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScorePrefab : MonoBehaviour
{
	[SerializeField] TextMeshProUGUI NameText;
	[SerializeField] TextMeshProUGUI ScoreText;

	public void SetScore(string name, float score)
	{
		NameText.SetText(name);
		ScoreText.SetText(score.ToString("N0"));
	}
}
