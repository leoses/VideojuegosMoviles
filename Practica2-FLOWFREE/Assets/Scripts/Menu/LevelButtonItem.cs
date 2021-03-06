using UnityEngine;
using UnityEngine.UI;

namespace FlowFreeGame.Menu
{
    public class LevelButtonItem : MonoBehaviour
    {
        private int levelIndex;

        [SerializeField] private string scene;
        [SerializeField] private Image img;
        [SerializeField] private Text text;
        [SerializeField] private Button button;
        [SerializeField] private GameObject starObject;
        [SerializeField] private GameObject tickObject;
        [SerializeField] private GameObject lockedObject;

        private void Awake()
        {
            //button.onClick.AddListener(() => OnLevelButtonClick());
            starObject.SetActive(false);
            tickObject.SetActive(false);
            lockedObject.SetActive(false);
        }
        public void SetColor(Color color)
        {
            img.color = color;
        }

        public void SetLvl(int lvl)
        {
            levelIndex = lvl;
            text.text = (levelIndex + 1).ToString();
        }

        public void OnLevelButtonClick()
        {
            GameManager.Instance.SetLevel(levelIndex);
            GameManager.Instance.LoadScene(scene);
        }

        public void SetButtonInteractable(bool interactuable)
        {
            button.interactable = interactuable;
        }

        public GameObject GetStarObject()
        {
            return starObject;
        }

        public GameObject GetTickObject()
        {
            return tickObject;
        }

        public GameObject GetLockedObject()
        {
            return lockedObject;
        }
    }
}