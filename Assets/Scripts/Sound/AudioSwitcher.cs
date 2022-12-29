using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace TraPortation
{
    using System.Threading.Tasks;
    using Game;
    using UnityEngine.SceneManagement;

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
        float time;
        List<float> changeTo = new List<float>() { -1, -1, -1, -1 };

        public void Start()
        {
            foreach (var audio in audios)
            {
                audio.volume = 0;
                audio.loop = true;
            }

            audios[bgmNum].volume = masterVolume;
        }

        private void Update()
        {
            if (manager.Status == GameStatus.SubMenu && !this.menu)
            {
                this.changeTo[bgmNum] = this.masterVolume / 2;
                this.menu = true;
            }

            if (manager.Status != GameStatus.SubMenu && this.menu)
            {
                this.changeTo[bgmNum] = this.masterVolume;
                this.menu = false;
            }

            this.time += Time.deltaTime;
            if (this.time > 150)
            {
                this.time = 0;
                if (this.bgmNum != 3)
                {
                    this.changeTo[this.bgmNum] = 0;
                    this.bgmNum++;
                    this.changeTo[this.bgmNum] = this.masterVolume;
                }
            }

            this.changeVolume();
        }

        private void changeVolume()
        {
            for (int i = 0; i < audios.Length; i++)
            {
                if (changeTo[i] == -1)
                {
                    continue;
                }

                if (Mathf.Abs(changeTo[i] - audios[i].volume) < 0.01f)
                {
                    audios[i].volume = changeTo[i];
                    changeTo[i] = -1;
                    continue;
                }
                else if (changeTo[i] > audios[i].volume)
                {
                    audios[i].volume += 0.01f;
                }
                else
                {
                    audios[i].volume -= 0.01f;
                }
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