using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSEPlay : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private AudioSource AudioComponent;//AudioSource型の変数aを宣言 使用するAudioSourceコンポーネントをアタッチ必要

    [SerializeField] private AudioClip Click;//AudioClip型の変数b1を宣言 使用するAudioClipをアタッチ必要
    [SerializeField] private AudioClip Cancel;//AudioClip型の変数b2を宣言 使用するAudioClipをアタッチ必要 
    [SerializeField] private AudioClip Pause;//AudioClip型の変数b3を宣言 使用するAudioClipをアタッチ必要 
    [SerializeField] private AudioClip Set;
    [SerializeField] private AudioClip Build;


    //自作の関数1
    public void ClickFn()
    {
        AudioComponent.PlayOneShot(Click);
    }

    //自作の関数2
    public void CancelFn()
    {
        AudioComponent.PlayOneShot(Cancel);
    }

    //自作の関数3
    public void PauseFn()
    {
        AudioComponent.PlayOneShot(Pause);
    }
    public void SetFn(){
        AudioComponent.PlayOneShot(Set);
    }
    public void BuildFn(){
        AudioComponent.PlayOneShot(Build);
    }

}
