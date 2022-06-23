using UnityEngine;

public class SwitchAudio : MonoBehaviour {
    [SerializeField]
    private AudioSource[] _audios;

    [Range(0, 3)]
    public int bgmNum = 0;

    [Range(0, 1)]
    public float masterVolume = 0.5f;

    public void Start() {
        for (int i = 0; i < _audios.Length; i++) {
            _audios[i].volume = 0;
            _audios[i].loop = true;
        }
    }

    private void Update () {
        for (int i = 0; i < _audios.Length; i++) {
            if (i == bgmNum) {
                _audios[i].volume += 0.01f;
                if (_audios[i].volume > masterVolume) {
                    _audios[i].volume = masterVolume;
                }
            } else {
                _audios[i].volume -= 0.01f;
            }
            
        }
        
    }
}