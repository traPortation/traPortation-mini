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
        List<float> relativeVolumes = new List<float>() { 0, 0, 0, 0 };
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

            relativeVolumes[bgmNum] = masterVolume;
        }

        public void MainVolumeSliderOnValueChange(float newSliderValue)
	    {
		    // 音楽の音量をスライドバーの値に変更
		    masterVolume = newSliderValue;
	    }

        private void Update()
        {
            if (manager.Status == GameStatus.SubMenu && !this.menu)
            {
                this.changeTo[bgmNum] = 1f / 2f;
                this.menu = true;
            }

            if (manager.Status != GameStatus.SubMenu && this.menu)
            {
                this.changeTo[bgmNum] = 1;
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
                    this.changeTo[this.bgmNum] = 1;
                }
            }

            this.changeVolume();
            this.setVolume();
        }

        // relativeVolumeを反映する
        void setVolume() {
            for (int i = 0; i < audios.Length; i++)
            {
                audios[i].volume = relativeVolumes[i] * masterVolume;
            }
        }

        // changeTo[i] を relativeVolume[i] に近づける
        private void changeVolume()
        {
            for (int i = 0; i < audios.Length; i++)
            {
                if (changeTo[i] == -1)
                {
                    continue;
                }

                if (Mathf.Abs(changeTo[i] - relativeVolumes[i]) < 0.01f)
                {
                    relativeVolumes[i] = changeTo[i];
                    changeTo[i] = -1;
                    continue;
                }
                else if (changeTo[i] > relativeVolumes[i])
                {
                    relativeVolumes[i] += 0.01f;
                }
                else
                {
                    relativeVolumes[i] -= 0.01f;
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