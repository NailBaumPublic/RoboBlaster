using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    private static DataManager instance;
    public static DataManager Instance
    {
        get
        {
            // �ν��Ͻ��� ���� ��� ���� ����
            if (instance == null)
            {
                // GameObject���� DataManager ������Ʈ ã��
                instance = FindObjectOfType<DataManager>();

                // ã�� ������Ʈ�� ���� ��� ���� ����
                if (instance == null)
                {
                    // ���ο� GameObject �����Ͽ� DataManager ������Ʈ �߰�
                    GameObject obj = new GameObject("DataManager");
                    instance = obj.AddComponent<DataManager>();
                }
            }
            return instance;
        }
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

	public bool IsMulti = false;

    public int Difficult = 0;

    public string PlayerName = "";

	
}
