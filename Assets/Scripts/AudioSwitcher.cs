using UnityEngine;

public class AudioSwitcher : MonoBehaviour {
    [SerializeField]
    private AudioSource[] audios;

    [Range(0, 3)]
    public int bgmNum = 0;

    [Range(0, 1)]
    public float masterVolume = 0.5f;

    public void Start() {
        foreach (var audio in audios) {
            audio.volume = 0;
            audio.loop = true;
        }
    }

    private void Update () {
        int i = -1;
        foreach (var audio in audios) {
            i++;
            if (i == bgmNum) {
                audio.volume = Mathf.Min(audio.volume + 0.01f, masterVolume);
            } else {
                audio.volume = Mathf.Max(audio.volume - 0.01f, 0);
            }
            
        }
        
    }
}