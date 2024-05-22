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
    //일시정지 버튼 클릭시 게임 일시정지
    public void OnClickPauseBtn()
    {
        Time.timeScale = 0;
    }

    //Resume 버튼 클릭시 게임 일시정지 해제
    public void OnClickResumeBtn()
    {
        Time.timeScale = 1;
    }

    //Mainmenu버튼 클릭시 메인메뉴로 이동
    public void OnClickMainmenuBtn()
    {
        SceneChange("StartScene");        
    }

    //Retry버튼 클릭시 현재씬 재로드
    public void OnClickRetryBtn()
    {
        //현재 활성화 되어있는 씬(현재씬) 가져오기
        Scene currentScene = SceneManager.GetActiveScene();
        //현재씬의 이름 가져오기
        string sceneName = currentScene.name;
        SceneChange(sceneName);
    }

    //씬 변경 함수
    void SceneChange(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
