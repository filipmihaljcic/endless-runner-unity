using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts
{
    public class GameData : MonoBehaviour
    {
        [Header("Game Settings")]

        [Tooltip("Music slider component.")]
        public GameObject _musicSlider; 
        [Tooltip("Sound slider component.")]
        public GameObject _soundSlider;
        public Text _scoreText = null;

        public static GameData _singleton;
        private int _score = 0;

        private void Awake() 
        {
            GameObject[] _gd = GameObject.FindGameObjectsWithTag("gamedata");

            // make sure that only one gd object exists in Scene
            if (_gd.Length > 1) Destroy(gameObject);
        
            // this will keep game data even if 
            // scene reloads or when scene switches
            DontDestroyOnLoad(gameObject); 
            _singleton = this;

            // update our music volume when game starts
            _musicSlider.GetComponent<UpdateMusic>().Start();
            _soundSlider.GetComponent<UpdateSound>().Start();

            PlayerPrefs.SetInt("score", 0);
        }

        public void UpdateScore(int _s) 
        {
            _score += _s;
            PlayerPrefs.SetInt("score", _score);
            if (_scoreText != null)
                _scoreText.text = "Score: " + _score;
        }
    }
}
