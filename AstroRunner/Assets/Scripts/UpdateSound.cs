using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Project.Scripts
{
    public class UpdateSound : MonoBehaviour
    {
        private List<AudioSource> _sfx = new List<AudioSource>();

        public void Start()
        {   
            AudioSource [] _allAS = GameObject.FindWithTag("gamedata").GetComponentsInChildren<AudioSource>();

            for (int i = 1; i < _allAS.Length; i++)
                _sfx.Add(_allAS[i]);
        

            Slider _sfxSlider = GetComponent<Slider>();

            if(PlayerPrefs.HasKey("sfxvolume"))
            {
                _sfxSlider.value = PlayerPrefs.GetFloat("sfxvolume");
                UpdateSoundVolume(_sfxSlider.value);
            }
            else 
            {
                _sfxSlider.value = 1;
                UpdateSoundVolume(1);
            }
        }

         public void UpdateSoundVolume(float _value)
         {
            PlayerPrefs.SetFloat("sfxvolume", _value);
            foreach (AudioSource _s in _sfx)
                _s.volume = _value;
         }
    }
}

