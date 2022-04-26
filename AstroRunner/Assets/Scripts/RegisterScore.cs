using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts
{
    public class RegisterScore : MonoBehaviour
    {
        private void Start()
        {
            // this will register score that we are getting while playing
            GameData._singleton._scoreText = GetComponent<Text>();
            // update it and pass 0 to make sure it says the same
            GameData._singleton.UpdateScore(0);
        }
    }
}
