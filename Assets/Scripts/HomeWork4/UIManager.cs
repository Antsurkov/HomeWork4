using TMPro;
using UnityEngine;
namespace HomeWork4
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject winPanel;
        [SerializeField] private GameObject losePanel;
        [SerializeField] private TextMeshProUGUI coinsTextMesh;

        private string coinsString = "Coins collected: {0}/{1}";
        
        
        public void ShowWinPanel()
        {
            winPanel.gameObject.SetActive(true);
        }
        
        public void ShowLosePanel()
        {
            losePanel.gameObject.SetActive(true);
        }
    
        public void UpdateCoinsCount(int newCount, int totalCount)
        {
            coinsTextMesh.text = string.Format(coinsString, newCount.ToString(), totalCount.ToString());
        }
    }
}

