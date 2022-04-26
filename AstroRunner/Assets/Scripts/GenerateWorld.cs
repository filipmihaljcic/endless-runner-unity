using UnityEngine;

namespace Project.Scripts
{
    public class GenerateWorld : MonoBehaviour
    {
        // this variable is used 
        // to track platform positions
        public static GameObject _dummyTraveller;
        public static GameObject _lastPlatform;

        private void Awake()
        {
            _dummyTraveller = new GameObject("dummy");
        }

        public static void RunDummy()
        {
            // we grab random object from our pool 
            GameObject _platform = Pool._singleton.GetRandom();

            if (_platform == null) return; 
        
            if (_lastPlatform != null)
            {
                // if we encounter TSection then our dummy will be 20 units ahead
                if (_lastPlatform.tag == "platformTSection")
                    _dummyTraveller.transform.position = _lastPlatform.transform.position 
                    + PlayerController._player.transform.forward * 20;
            
                else 
                    _dummyTraveller.transform.position = _lastPlatform.transform.position
                    + PlayerController._player.transform.forward * 10;
            
                // if we ecounter stairs up our dummy goes up by 5 on y axis
                if (_lastPlatform.tag == "stairsUp")
                    _dummyTraveller.transform.Translate(0, 5, 0);
            }
    
            _lastPlatform = _platform;
            _platform.SetActive(true);
            // update our platform position to 
            // be equal to dummyTraveller position
            _platform.transform.position = _dummyTraveller.transform.position;
            _platform.transform.rotation = _dummyTraveller.transform.rotation;

            // if we encounter stairs down we lower 
            // dummy by -5 and rotate stairs by 180 degrees
            if (_platform.tag == "stairsDown")
            {
                _dummyTraveller.transform.Translate(0, -5, 0);
                _platform.transform.Rotate(0, 180, 0);
                _platform.transform.position = _dummyTraveller.transform.position;
            }
        }
    }
}

       
        
          

