using JetBrains.Annotations;
using UnityEngine;


namespace Project.Scripts
{
    public class Pickup : MonoBehaviour
    {
        [Header("Coin Settings")]
        
        [Tooltip("Text that shows above our coin.")]
        public GameObject _floatText;
        [Tooltip("Particle effect when coin is picked up.")]
        public GameObject _coinParticleEffect;
        private MeshRenderer[] _meshRenderers;
        private GameObject _canvas; 

        private void Start()
        {
            _meshRenderers = this.GetComponentsInChildren<MeshRenderer>();
            _canvas = GameObject.Find("Canvas");
        }

        private void OnTriggerEnter([NotNull]Collider _col) 
        {
            if (_col.gameObject.tag == "Player")
                CoinPickup();  
        } 

        private void OnEnable()
        {
            if (_meshRenderers != null) 
                foreach(MeshRenderer _m in _meshRenderers)
                    _m.enabled = true;
        }

        private void CoinPickup()
        {
            GameData._singleton.UpdateScore(10);
            GameObject _scoreText = Instantiate(_floatText);
            // we child our floatText under 
            // canvas to be able to see it
            _scoreText.transform.SetParent(_canvas.transform); 

            // this makes our effect stays in 
            // same place after turning left or right
            Quaternion _particleQuaternion = PlayerController._player.transform.rotation;
            _particleQuaternion *= Quaternion.Euler(0f, 180f, 0f);

            GameObject _scoreEffect = Instantiate(_coinParticleEffect, this.transform.position, _particleQuaternion);
            // destroy particles after 1 second
            Destroy(_scoreEffect, 1f); 
            // play pickup sound after
            // our coin is picked up
            PlayerController._sfx[1].Play(); 

            // saving the position of our coin on the screen
            Vector3 _screenPoint = Camera.main.WorldToScreenPoint(this.transform.position);  
            _scoreText.transform.position = _screenPoint;
            foreach(MeshRenderer _m in _meshRenderers)
                _m.enabled = false;
         }
     }  
 }

