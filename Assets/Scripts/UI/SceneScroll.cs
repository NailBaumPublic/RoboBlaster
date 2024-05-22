using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneScroll : MonoBehaviour
{
    [SerializeField] float ScrollSpeed = 5f;  // 스크롤 속도
    [SerializeField] GameObject[] Backgrounds;  // 배경 오브젝트 배열
    private float _backgroundWidth = 22.4f;  // 배경 오브젝트의 폭

   
    void Update()
    {
        // 각 배경 오브젝트를 스크롤
        foreach (GameObject background in Backgrounds)
        {
            background.transform.Translate(Vector3.left * ScrollSpeed * Time.deltaTime);

            // 배경 오브젝트가 화면의 왼쪽 끝을 지나면 다시 오른쪽 끝으로 이동
            if (background.transform.position.x <= -_backgroundWidth)
            {
                Vector3 newPos = new Vector3(background.transform.position.x + _backgroundWidth * Backgrounds.Length, background.transform.position.y, background.transform.position.z);
                background.transform.position = newPos;
            }
        }
    }
}
