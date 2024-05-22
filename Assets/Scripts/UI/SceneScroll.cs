using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneScroll : MonoBehaviour
{
    [SerializeField] float ScrollSpeed = 5f;  // ��ũ�� �ӵ�
    [SerializeField] GameObject[] Backgrounds;  // ��� ������Ʈ �迭
    private float _backgroundWidth = 22.4f;  // ��� ������Ʈ�� ��

   
    void Update()
    {
        // �� ��� ������Ʈ�� ��ũ��
        foreach (GameObject background in Backgrounds)
        {
            background.transform.Translate(Vector3.left * ScrollSpeed * Time.deltaTime);

            // ��� ������Ʈ�� ȭ���� ���� ���� ������ �ٽ� ������ ������ �̵�
            if (background.transform.position.x <= -_backgroundWidth)
            {
                Vector3 newPos = new Vector3(background.transform.position.x + _backgroundWidth * Backgrounds.Length, background.transform.position.y, background.transform.position.z);
                background.transform.position = newPos;
            }
        }
    }
}
