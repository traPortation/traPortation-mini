using UnityEngine;
using Zenject;

namespace TraPortation
{
    using Game;

    public class AudioSwitcher : MonoBehaviour
    {
        [SerializeField]
        private AudioSource[] audios;

        [Range(0, 3)]
        public int bgmNum = 0;

        [Range(0, 1)]
        public float masterVolume = 0.5f;
        [Inject]
        GameManager manager;
        bool menu = false;

        public void Start()
        {
            foreach (var audio in audios)
            {
                audio.volume = 0;
                audio.loop = true;
            }
        }

        private void Update()
        {
            foreach (var (audio, index) in audios.Indexed())
            {
                if (index == bgmNum)
                {
                    audio.volume = Mathf.Min(audio.volume + 0.01f, masterVolume);
                }
                else
                {
                    audio.volume = Mathf.Max(audio.volume - 0.01f, 0);
                }
            }

            if (manager.Status == GameStatus.SubMenu && !this.menu)
            {
                this.masterVolume /= 2;
                this.menu = true;
            }

            if (manager.Status != GameStatus.SubMenu && this.menu)
            {
                this.masterVolume *= 2;
                this.menu = false;
            }
        }

        public void Pause()
        {
            foreach (var audio in audios)
            {
                audio.Pause();
            }
        }

        public void UnPause()
        {
            foreach (var audio in audios)
            {
                audio.UnPause();
            }
        }
    }
}