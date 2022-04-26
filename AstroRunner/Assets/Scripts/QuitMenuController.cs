using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Project.Scripts
{
    public class QuitMenuController : MonoBehaviour
    {
        public void LoadQuitScene()
        {
            SceneManager.LoadScene("Menu", LoadSceneMode.Single);
        }
    }
}