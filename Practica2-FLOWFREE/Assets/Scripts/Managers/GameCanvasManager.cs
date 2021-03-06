using UnityEngine;
using UnityEngine.UI;

namespace FlowFreeGame
{
    public enum Icon {None,Tick,Star }
    public class GameCanvasManager : MonoBehaviour
    {
        [SerializeField]
        private Text clueText;
        [SerializeField]
        private Text flowsText;
        [SerializeField]
        private Text bestText;
        [SerializeField]
        private Text movesText;
        [SerializeField]
        private Text pipePercentage;
        [SerializeField]
        private Text levelText;
        [SerializeField]
        private Text dimentionsText;
        [SerializeField]
        private GameObject winningPannel;
        [SerializeField]
        private Text pannelText;
        [SerializeField]
        private Text nextLevelButtonText;
        [SerializeField]
        private Button nextLevelButton;
        [SerializeField]
        private Button prevLevelButton;
        [SerializeField]
        private GameObject starIcon;
        [SerializeField]
        private GameObject tickIcon;



        //TO DO: quitarlos de aqui y en el inspector llamar a los de gameManager/levelManager
        public void ChangeNextLevel()
        {
            LevelManager.Instance.NextLevel();
        }
        public void ChangeBackLevel()
        {
            LevelManager.Instance.BackLevel();
        }
        public void RestartLevel()
        {
            LevelManager.Instance.Restart();
        }

        public void GoToLevelScene()
        {
            GameManager.Instance.LoadScene("LevelSelectorRemake");
        }

        public void UseClue()
        {
            LevelManager.Instance.UseClue();
        }

        public void GetNewClue()
        {
            GameManager.Instance.GetNewClue();
        }

        public void SetClueText(int clues)
        {
            clueText.text = clues + " x";
        }

        public void SetBestText(int n)
        {
            string s;
            if (n == 0)
            {
                s = "-";
            }
            else { s = n.ToString(); }
            bestText.text = "best : " + s;
        }
        public void SetflowsText(int n, int total)
        {
            flowsText.text = " flows : " + n + " / " + total;
        }
        public void SetMovesText(int n)
        {
            movesText.text = " moves : " + n;
        }
        public void SetPercentageText(int n)
        {
            pipePercentage.text = "pipe : " + n + " %";
        }

        public void SetLevelText(int n, int w, int h)
        {
            levelText.text = "Level " + n + " ";
            levelText.color = GameManager.Instance.GetCategories()[GameManager.Instance.GetLvlActual().category].categoryColor;
            dimentionsText.text = w + "x" + h;
        }

        public void SetPanelActive(bool active)
        {
            winningPannel.SetActive(active);
        }

        public void SetPannelText(string text)
        {
            pannelText.text =text;
        }

        public void SetPannelButtonText(string text)
        {
            nextLevelButtonText.text = text;
        }

        public void IsNextLevelButtonInteractuable( bool active)
        {
            nextLevelButton.interactable = active;
        }
        public void IsPrevLevelButtonInteractuable( bool active)
        {
            prevLevelButton.interactable = active;
        }

        public void SetIcon(Icon icon)
        {
            switch (icon)
            {
                case Icon.None:
                    starIcon.SetActive(false);
                    tickIcon.SetActive(false);
                    break;
                case Icon.Tick:
                    starIcon.SetActive(false);
                    tickIcon.SetActive(true);
                    break;
                case Icon.Star:
                    tickIcon.SetActive(false);
                    starIcon.SetActive(true);
                    break;
            }
        }
    }
}
