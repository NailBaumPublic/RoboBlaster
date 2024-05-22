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
            // 인스턴스가 없을 경우 새로 생성
            if (instance == null)
            {
                // GameObject에서 DataManager 컴포넌트 찾기
                instance = FindObjectOfType<DataManager>();

                // 찾은 컴포넌트가 없을 경우 새로 생성
                if (instance == null)
                {
                    // 새로운 GameObject 생성하여 DataManager 컴포넌트 추가
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
