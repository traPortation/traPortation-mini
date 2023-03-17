using UnityEngine;

public class ChangeMainVolume : MonoBehaviour
{
	private AudioSource audioSource;

	private void Start()
	{
		// "AudioSource"コンポーネントを取得
		audioSource = gameObject.GetComponent<AudioSource>();

	}

	/// <summary>
	/// スライドバー値の変更イベント
	/// </summary>
	/// <param name="newSliderValue">スライドバーの値(自動的に引数に値が入る)</param>
	public void MainVolumeSliderOnValueChange(float newSliderValue)
	{
		// 音楽の音量をスライドバーの値に変更
		AudioListener.volume = newSliderValue;
	}
}