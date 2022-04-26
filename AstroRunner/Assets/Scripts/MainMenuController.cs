using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Project.Scripts
{   
    public class MainMenuController : MonoBehaviour
    {
        private GameObject[] _subPanels;
        private GameObject[] _mainMenuButtons;
        private int _maxLives = 3;

        private void Start()
        {
            // put every gameobject with tag "subpanel" in panels array 
            _subPanels = GameObject.FindGameObjectsWithTag("subpanel");
            _mainMenuButtons = GameObject.FindGameObjectsWithTag("mmbutton");

            foreach (GameObject _sp in _subPanels)
                _sp.SetActive(false);
        }   

        public void OpenPanel(Button _button)
        {
            // accessing and activating the child of our button(child is panel)
            _button.gameObject.transform.GetChild(1).gameObject.SetActive(true);
    
            foreach (GameObject _b in _mainMenuButtons)
                if (_b != _button.gameObject)
                    _b.SetActive(false);
        }

        public void ClosePanel(Button _button)
        {
            // accessing parent(button) and deactivating it
            _button.gameObject.transform.parent.gameObject.SetActive(false);

            foreach (GameObject _b in _mainMenuButtons)
                _b.SetActive(true);
        }

        public void LoadGameScene()
        {
            PlayerPrefs.SetInt("lives", _maxLives);
            SceneManager.LoadScene("Platforms", LoadSceneMode.Single);
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        private void Update() 
        {
            if (Input.GetKey("escape"))
                QuitGame();
        }
    }
}
