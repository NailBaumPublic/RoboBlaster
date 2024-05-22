using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseUI : MonoBehaviour
{
    private void Start()
    {
        Time.timeScale = 1;
    }
    //�Ͻ����� ��ư Ŭ���� ���� �Ͻ�����
    public void OnClickPauseBtn()
    {
        Time.timeScale = 0;
    }

    //Resume ��ư Ŭ���� ���� �Ͻ����� ����
    public void OnClickResumeBtn()
    {
        Time.timeScale = 1;
    }

    //Mainmenu��ư Ŭ���� ���θ޴��� �̵�
    public void OnClickMainmenuBtn()
    {
        SceneChange("StartScene");        
    }

    //Retry��ư Ŭ���� ����� ��ε�
    public void OnClickRetryBtn()
    {
        //���� Ȱ��ȭ �Ǿ��ִ� ��(�����) ��������
        Scene currentScene = SceneManager.GetActiveScene();
        //������� �̸� ��������
        string sceneName = currentScene.name;
        SceneChange(sceneName);
    }

    //�� ���� �Լ�
    void SceneChange(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
