using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundPlayAcrossSceneMain : MonoBehaviour
{
    void Start()
    {
        clip = gameObject.GetComponent<AudioSource>().clip;
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
    void DelayMethod()
    {
        
        Debug.Log("Delay call");
        Destroy(this.gameObject);
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name != "Game Scene"){
            Invoke(nameof(DelayMethod), 2.0f);
        }
    }
    AudioClip clip;

    public void PlayStart()
    {
        GetComponent<AudioSource>().PlayOneShot(clip);
    }
    private void OnDestroy()
    {
        // Destroy時に登録したInvokeをすべてキャンセル
        CancelInvoke();
    }

}