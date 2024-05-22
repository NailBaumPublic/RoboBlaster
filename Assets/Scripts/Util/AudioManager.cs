using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;

    public static AudioManager Instance
    {
        get
        {
            // �ν��Ͻ��� ���� ��� ���� ����
            if (instance == null)
            {
                // GameObject���� DataManager ������Ʈ ã��
                instance = FindObjectOfType<AudioManager>();

                // ã�� ������Ʈ�� ���� ��� ���� ����
                if (instance == null)
                {
                    // ���ο� GameObject �����Ͽ� DataManager ������Ʈ �߰�
                    GameObject obj = new GameObject("AudioManager");
                    instance = obj.AddComponent<AudioManager>();
                }
            }
            return instance;
        }
    }
    [SerializeField] List<AudioSource> AudioSources;
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

    public void SetMasterVolume(float volume)
    {
        AudioListener.volume = volume;
    }
    public void SetBGMVolume(float volume)
    {
        AudioSources[0].volume = volume*0.5f;       
    }

    public void SetEffectVolume(float volume)
    {
        AudioSources[1].volume = volume*1.5f;
        AudioSources[2].volume = volume*0.3f;
        AudioSources[3].volume = volume*0.3f;
        AudioSources[4].volume = volume*0.3f;
    }

    public void PlayShootSound()
    {
        AudioSources[1].PlayOneShot(AudioSources[1].clip);
    }
    public void PlayExplosionSound()
    {
        AudioSources[2].PlayOneShot(AudioSources[2].clip);
    }

    public void PlaySkillSound()
    {
        AudioSources[3].PlayOneShot(AudioSources[3].clip);
    }

    public void PlayGetItemSound()
    {
        AudioSources[4].PlayOneShot(AudioSources[4].clip);
    }
}
