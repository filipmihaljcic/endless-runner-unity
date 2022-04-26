using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts
{
    public class FloatUpText : MonoBehaviour
    {
        private Text _text;
        private float _alpha = 1f; // how fast our text will fade out

        private void Start() 
        {
           _text = this.GetComponent<Text>();
           _text.color = Color.white;
        }

        private void Update() 
        {
           FloatingText();
        }

        private void FloatingText()
        {
           this.transform.Translate(0f, 20f, 0f); // move our coin text on y-axis
           _alpha -= 0.01f;
           _text.color = new Color (_text.color.r, _text.color.g, _text.color.b, _alpha);

           // after our text fades out
           if (_alpha < 0) Destroy(gameObject);
       }
    }
}
