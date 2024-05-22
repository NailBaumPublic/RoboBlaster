using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager Instance { get; private set; }

    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
        public Transform parent; // 부모 오브젝트를 위한 필드 추가
    }

    [SerializeField] private List<Pool> Pools;
    private Dictionary<string, Queue<GameObject>> poolDictionary;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        InitializePools();
    }

    private void InitializePools()
    {
        var player1BulletPool = Pools.Find(p => p.tag == "Player1Bullet");
        if (player1BulletPool != null)
        {
            AddPool(player1BulletPool.tag, 50, player1BulletPool.prefab, player1BulletPool.parent);
        }

        if (DataManager.Instance.IsMulti == false)
        {
            var player2BulletPool = Pools.Find(p => p.tag == "Player2Bullet");
            if (player2BulletPool != null)
            {
                AddPool(player2BulletPool.tag, 50, player2BulletPool.prefab, player2BulletPool.parent);
            }
        }

        // 다른 기본 풀들도 초기화
        foreach (Pool pool in Pools)
        {
            if (pool.tag != "Player1Bullet" && pool.tag != "Player2Bullet") // 이미 초기화한 풀은 제외
            {
                AddPool(pool.tag, pool.size, pool.prefab, pool.parent);
            }
        }
    }

    private void AddPool(string tag, int size, GameObject prefab, Transform parent)
    {
        if (poolDictionary.ContainsKey(tag))
            return;

        Queue<GameObject> objectPool = new Queue<GameObject>();

        for (int i = 0; i < size; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.transform.SetParent(parent); // 부모 오브젝트 설정
            obj.SetActive(false);
            objectPool.Enqueue(obj);
        }

        poolDictionary.Add(tag, objectPool);
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning($"Pool with tag {tag} doesn't exist.");
            return null;
        }

        GameObject objectToSpawn = poolDictionary[tag].Dequeue();

        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;
        objectToSpawn.SetActive(true);

        poolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }

    public void ReturnToPool(string tag, GameObject objectToReturn)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning($"Pool with tag {tag} doesn't exist.");
            return;
        }

        objectToReturn.SetActive(false);
        poolDictionary[tag].Enqueue(objectToReturn);
    }

    public List<GameObject> GetAllObjectsInPool(string tag)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning($"Pool with tag {tag} doesn't exist.");
            return null;
        }

        return new List<GameObject>(poolDictionary[tag]);
    }
}
