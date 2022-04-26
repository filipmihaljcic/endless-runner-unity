using UnityEngine;

namespace Project.Scripts
{
    public class SmoothFollow : MonoBehaviour
    {
        [Tooltip("Object that camera follows.")]
        public Transform _target;
        private float _distance = 3.0f;
        private float _height = 0.7f;
        private float _heightOffset = 0.0f;
        private float _heightDamping = 5.0f;
        private float _rotationDamping = 3.0f;
   
        private void LateUpdate()
        {
            SmoothCamera();
        }

        private void SmoothCamera()
        {
             if (_target == null)
                return;

             if (!PlayerController._isDead)
             {
                float _wantedRotationAngle = _target.eulerAngles.y;
                float _wantedHeight = _target.position.y + _height;

                float _currentRotationAngle = transform.eulerAngles.y;
                float _currentHeight = transform.position.y;

                _currentRotationAngle = Mathf.LerpAngle(_currentRotationAngle, _wantedRotationAngle, _rotationDamping * Time.deltaTime);
                _currentHeight = Mathf.Lerp(_currentHeight, _wantedHeight, _heightDamping * Time.deltaTime);

                Quaternion _currentRotation = Quaternion.Euler(0, _currentRotationAngle, 0);

                transform.position = _target.position;
                transform.position -= _currentRotation * Vector3.forward * _distance;

                transform.position = new Vector3(transform.position.x,
                                    _currentHeight + _heightOffset,
                                    transform.position.z);
             }

                transform.LookAt(_target);
            }
        }
   }    
