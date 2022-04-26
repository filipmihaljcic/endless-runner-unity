using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Project.Scripts
{
    public class UpdateMusic : MonoBehaviour
    {
        private List <AudioSource> _music = new List<AudioSource>();

        public void Start()
        {
            
            AudioSource [] _allAS = GameObject.FindWithTag("gamedata").GetComponentsInChildren<AudioSource>();
            // add first AudioSource component attached to our gameobject
            _music.Add(_allAS[0]);
            Slider _musicSlider = GetComponent<Slider>();

            if (PlayerPrefs.HasKey("musicvolume"))
            {
                 _musicSlider.value = PlayerPrefs.GetFloat("musicvolume");
                 // update our music loudness
                 UpdateMusicVolume(_musicSlider.value);
            }
            else 
            {
                 _musicSlider.value = 1;
                 UpdateMusicVolume(1);
            }
        }

        public void UpdateMusicVolume(float _value)
        {   
            // every time we move the volume slider we set value 
            PlayerPrefs.SetFloat("musicvolume", _value);
            foreach(AudioSource _m in _music)
            // set volume of specific AudioSource component
                _m.volume = _value;
        }   
    }
}

