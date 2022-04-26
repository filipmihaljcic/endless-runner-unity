using UnityEngine;

namespace Project.Scripts
{
    public class Scroll : MonoBehaviour
    {
        [Tooltip("Moving speed of our platforms.")]
        public float _worldSpeed = -0.1f; 

        private void FixedUpdate() 
        {   
            Scrolling();
        }

        private void Scrolling()
        {
            if (PlayerController._isDead) return;

            //platforms move in direction in which our player is moving 
            transform.position += PlayerController._player.transform.forward * _worldSpeed;

            if (PlayerController._currentPlatform == null) return;

            //if we encounter stairs up our world raises
            if (PlayerController._currentPlatform.tag == "stairsUp")   
                this.transform.Translate(0, -0.06f, 0);
        
            if (PlayerController._currentPlatform.tag == "stairsDown")
                this.transform.Translate(0, 0.06f, 0);
        }
    }
}
