using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Loading : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // シーンの読み込みをする
        Invoke("Load", 0.1f);
    }

    private AsyncOperation async;
	//　読み込み率を表示するスライダー
	[SerializeField]
    private Slider slider;

    public void Load() {
        Debug.Log("Loading");
		//　コルーチンを開始
		StartCoroutine("LoadData");
        
	}
	IEnumerator LoadData() {
		// シーンの読み込みをする
		async = SceneManager.LoadSceneAsync("Game Scene");

		//　読み込みが終わるまで進捗状況をスライダーの値に反映させる
		while(!async.isDone) {
			var progressVal = Mathf.Clamp01(async.progress / 0.9f);
			slider.value = progressVal;
			yield return null;
		}
	}
}
