using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class StartUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI MultiplayBtnText;

    [SerializeField] TMP_InputField InputField;
    [SerializeField] GameObject DifficultPopUp;
    public void OnClickMultiBtn()
    {
        if (DataManager.Instance.IsMulti)
        {
            MultiplayBtnText.text = "Multi Play";
            DataManager.Instance.IsMulti = false;
        }
        else
        {
            MultiplayBtnText.text = "Single Play";
            DataManager.Instance.IsMulti = true;
        }
    }

    public void OnChooseDifficultBtn(int difficult)
    {
        DataManager.Instance.Difficult = difficult;
        SceneManager.LoadScene("ExGameScene");
    }

    public void OnClickGameStartBtn()
    {
        if(InputField.text.Length <= 0)
        {
            return;
        }

        DataManager.Instance.PlayerName = InputField.text;
        DifficultPopUp.SetActive(true);
    }
}
