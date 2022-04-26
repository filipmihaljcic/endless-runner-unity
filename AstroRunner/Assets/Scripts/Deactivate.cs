using JetBrains.Annotations;
using UnityEngine;


namespace Project.Scripts
{

    public class Deactivate : MonoBehaviour
    {
        /*with this we assure that 
        Invoke is called only once*/
        private bool _deactivationScheduled = false;
    
        private void OnCollisionExit([NotNull]Collision player) 
        {
            if (PlayerController._isDead) return;

            if (player.gameObject.tag == "Player" && !_deactivationScheduled)
            {
                // When our element is behind the camera enough 
                _deactivationScheduled = true;
                Invoke(nameof(SetInactive), 4.0f);
            }   
        }

        private void SetInactive()
        {
            // deactivate our platform
            gameObject.SetActive(false);
            _deactivationScheduled = false;
        }
    }
}