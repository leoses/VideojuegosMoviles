using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FreeFlowGame
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField]
        GameCanvasManager canvasManager;

        private static LevelManager _instance;

        public static LevelManager Instance { get { return _instance; } }

        private void Awake()
        {
            if (_instance != null)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this;
            }
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        public void Restart()
        {
            BoardManager.Instance.GetPipeController().ResetPipes();
        }

        public void NextLevel()
        {
            LvlActual act = GameManager.Instance.GetLvlActual();
            CategoryPack[] categories = GameManager.Instance.GetCategories();
            List<List<Nivel[]>> levels = GameManager.Instance.GetLevels();
            
            if (act.levelIndex + 1 < levels[act.category][act.slotIndex].Length
                 && (levels[act.category][act.slotIndex][act.levelIndex + 1].bestMoves > 0
                || !categories[act.category].lotes[act.slotIndex].levelblocked))
            {
                Debug.Log("Cambio de nivel");
                GameManager.Instance.SetLevel(act.levelIndex + 1);
                BoardManager.Instance.Initialize();
            }
        }
        public void BackLevel()
        {
            LvlActual act = GameManager.Instance.GetLvlActual();
            if (act.levelIndex - 1 >= 0)
            {
                Debug.Log("Cambio de nivel");
                GameManager.Instance.SetLevel(act.levelIndex - 1);
                BoardManager.Instance.Initialize();
            }
        }

        public void UseClue()
        {
            GameManager.Instance.UseClue();
            BoardManager.Instance.GetPipeController().PaintClue();
            canvasManager.SetClueText(GameManager.Instance.GetNumClues());
        }

        public void SetLevelText(int n, int w, int h)
        {
            canvasManager.SetLevelText(n, w, h);
        }

        public void SetflowsText(int n)
        {
            canvasManager.SetflowsText(n, BoardManager.Instance.GetPipeSolution().Count);
        }

        public void SetPercentageText(int n)
        {
            canvasManager.SetPercentageText(n);
        }

        public void SetMovesText(int n)
        {
            canvasManager.SetMovesText(n);
        }

        public void SetBestText()
        {
            LvlActual act = GameManager.Instance.GetLvlActual();
            List<List<Nivel[]>> levels = GameManager.Instance.GetLevels();
            canvasManager.SetBestText(levels[act.category][act.slotIndex][act.levelIndex].bestMoves);
        }

        public void setClueText()
        {
            canvasManager.SetClueText(GameManager.Instance.GetNumClues());
        }

        public void LevelCompleted(int moves)
        {
            //TO DO: Desbloquear el nivel siguiente y guardar cosos supongo

            canvasManager.SetPannelText(moves);
            canvasManager.SetPanelActive(true);
        }
    }
}
