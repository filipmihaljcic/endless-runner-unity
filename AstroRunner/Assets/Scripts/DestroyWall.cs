using JetBrains.Annotations;
using UnityEngine;
using System.Collections.Generic;

namespace Project.Scripts
{
    public class DestroyWall : MonoBehaviour
    {
        [Tooltip("Individual bricks of our wall.")]
        public GameObject[] _bricks;
        private List<Rigidbody> _bricksRBs = new List<Rigidbody>();
        private List<Vector3> _positions = new List<Vector3>();
        private List<Quaternion> _rotations = new List<Quaternion>();
        private Collider _col;

        private void OnEnable()
        {
            _col.enabled = true;

            // build wall
            for (int i = 0; i < _bricks.Length; i++)
            {
                _bricks[i].transform.localPosition = _positions[i];
                _bricks[i].transform.localRotation = _rotations[i];
                _bricksRBs[i].isKinematic = true;
            }
        }
        private void Awake()
        {
            _col = this.GetComponent<Collider>();

            // add pieces, positions, and 
            // rotations of wall in array and list
            foreach (GameObject _b in _bricks)
            {
                _bricksRBs.Add(_b.GetComponent<Rigidbody>());
                _positions.Add(_b.transform.localPosition);
                _rotations.Add(_b.transform.localRotation);
            }
        }
        private void OnCollisionEnter([NotNull]Collision _other)
        {
            if (_other.gameObject.tag == "Spell")
            {
                // destroy wall
                _col.enabled = false;
                PlayerController._sfx[5].Play();
                foreach (Rigidbody _r in _bricksRBs)
                    _r.isKinematic = false;
            }
        }
    }
}