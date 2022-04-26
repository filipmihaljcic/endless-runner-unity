using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts
{
    public class DisplayStats : MonoBehaviour
    {
         [Header("Stats settings")]

         [Tooltip("Last score UI placeholder.")]
         public Text _lastScore;
         [Tooltip("Highest score UI placholder.")]
         public Text _highestScore;

         private void OnEnable() 
         {
            DisplayScore();
         }

         private void DisplayScore()
         {
            if (PlayerPrefs.HasKey("lastscore"))
                // last score is displayed according 
                // to what is saved in PlayerPrefs
               _lastScore.text = "Last Score: " + PlayerPrefs.GetInt("lastscore");
            else 
               _lastScore.text = "Last Score: 0";
        
            if (PlayerPrefs.HasKey("highscore"))
               // high score is displayed according 
               // to what is saved in PlayerPrefs
               _highestScore.text = "Highest Score: " + PlayerPrefs.GetInt("highscore");
            else 
               _highestScore.text = "Highest Score: 0";
         }
      }  
   }
