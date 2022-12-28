using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundPlay : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Game Scene"){
            Destroy(this.gameObject);
        }
    }
    AudioClip clip;

    void Start()
    {
        clip = gameObject.GetComponent<AudioSource>().clip;
    }

    public void PlayStart()
    {
        GetComponent<AudioSource>().PlayOneShot(clip);
    }
}