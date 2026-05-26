using TMPro;
using UnityEngine;

namespace Features.Score
{
    public class CardRanking : MonoBehaviour
    {
        [SerializeField] private TMP_Text positionText;
        [SerializeField] private TMP_Text nameText;
        [SerializeField] private TMP_Text scoreText;

        private const string ScoreFormat = "{0} s";

        public void SetData(int position, string playerName, float time)
        {
            positionText.text = position.ToString();
            nameText.text = playerName;
            scoreText.text = string.Format(ScoreFormat, time);
        }

        public void Clear()
        {
            positionText.text = string.Empty;
            nameText.text = string.Empty;
            scoreText.text = string.Empty;
        }
    }
}