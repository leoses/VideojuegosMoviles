using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FlowFreeGame.Menu
{
    public class LevelsScrollViewController : MonoBehaviour
    {
        private int numberOfLevels;
        private Color[] pipesColor;
        private Text dimensionsText;

        [SerializeField] private RectTransform buttonsAreaRect;
        [SerializeField] private RectTransform oneButtonAreaRect;
        [SerializeField] private GameObject verticalZone;
        [SerializeField] private GameObject filaZone;
        [SerializeField] private GameObject textsZone;
        [SerializeField] private GameObject buttonsZone;
        [SerializeField] private LevelButtonItem levelBtnPref;
        [SerializeField] private ContentScrollScript contentScroll;

        private void Start()
        {
            dimensionsText = contentScroll.GetDimensionsText();
            pipesColor = GameManager.Instance.GetColorTheme().colorTheme;
            LoadScrollButtons();
        }

        // load level buttons on game start
        private void LoadScrollButtons()
        {
            LvlActual act = GameManager.Instance.GetLvlActual();
            numberOfLevels = GameManager.Instance.GetLevels()[act.category][act.slotIndex].Length;
            bool blocked = GameManager.Instance.GetCategories()[act.category].lotes[act.slotIndex].levelblocked;

            bool nextLvlsBlockeds = false;
            int conAct = -1;
            int conAct2 = -1;
            GameObject fila = new GameObject();
            GameObject tandaNiveles = new GameObject();

            float buttonAreaWidth = 0.0f;
            for (int i = 0; i < numberOfLevels; i++)
            {
                act.levelIndex = i;
                LvlActual prevLevel = act;

                if (i - 1 >= 0) prevLevel.levelIndex = i - 1;

                //Si el lote en el que estamos tiene niveles bloqueados miramos que esté sin jugar y que no sea el primero del lote 
                if (blocked)
                {
                    if (i - 1 >= 0 && GameManager.Instance.GetLevelBestMoves(act) == 0 && GameManager.Instance.GetLevelBestMoves(prevLevel) == 0)
                        nextLvlsBlockeds = true;
                }

                //Nueva tanda de 30 Niveles
                if (i / 30 > conAct)
                {
                    conAct++;
                    dimensionsText.text = GameManager.Instance.GetCategories()[act.category].lotes[act.slotIndex].nameEach30levels[conAct];
                    tandaNiveles = Instantiate(verticalZone, buttonsZone.transform);
                    buttonAreaWidth += (oneButtonAreaRect.rect.width * 5) + 50.0f;
                    Instantiate(contentScroll, textsZone.transform);
                }

                //Nueva Fila
                if(i / 5 > conAct2)
                {
                    conAct2++;
                    fila = Instantiate(filaZone, tandaNiveles.transform);
                }

                LevelButtonItem levelBtnObj = Instantiate(levelBtnPref, fila.transform);
                levelBtnObj.SetLvl(i);
                levelBtnObj.SetColor(pipesColor[i / 30]);

                if (!blocked || !nextLvlsBlockeds)
                {
                    levelBtnObj.SetButtonInteractable(true);
                }
                else
                {
                    levelBtnObj.SetButtonInteractable(false);
                }


                //En función de si el nivel está perfecto o no activamos la estrella, el tick o el candado
                if (GameManager.Instance.GetIsLevelPerfect(act))
                {
                    levelBtnObj.GetStarObject().SetActive(true);
                }
                else if (GameManager.Instance.GetLevelBestMoves(act) > 0)
                {
                    levelBtnObj.GetTickObject().SetActive(true);
                }
                else if(GameManager.Instance.GetLevelBestMoves(act) == 0 
                    && GameManager.Instance.GetCategories()[act.category].lotes[act.slotIndex].levelblocked
                    && act.levelIndex != 0 && GameManager.Instance.GetLevelBestMoves(prevLevel) == 0) 
                {
                    levelBtnObj.GetLockedObject().SetActive(true);
                }

            }
            Debug.Log(buttonAreaWidth);
            buttonsAreaRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, buttonAreaWidth);
            
        }
    }
}
