using UnityEngine;
using SOC;
using UnityEngine.UI;

namespace FlowFreeGame.Menu
{
    public class SlotsScrollViewController : MonoBehaviour
    {

        [SerializeField] private CategoryTextItem CatPref;
        [SerializeField] private SlotButtonItem SlotPref;
        [SerializeField] private RectTransform buttonAreaRect;
        [SerializeField] private Text levels;

        private Color[] lettersColors;
        private void Start()
        {
            lettersColors = GameManager.Instance.GetColorTheme().colorTheme;
            RectTransform buttonRect = SlotPref.GetButonRectTransform();

            buttonAreaRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 600);

            levels.text = "<color=#" + ColorUtility.ToHtmlStringRGBA(lettersColors[0]) + ">L</color><color=#" + ColorUtility.ToHtmlStringRGBA(lettersColors[1]) + ">e</color><color=#" + ColorUtility.ToHtmlStringRGBA(lettersColors[2]) + ">v</color><color=#" + ColorUtility.ToHtmlStringRGBA(lettersColors[3]) + ">e</color><color=#" + ColorUtility.ToHtmlStringRGBA(lettersColors[4]) + ">l</color><color=#" + ColorUtility.ToHtmlStringRGBA(lettersColors[5]) + ">s</color>";
            
            LoadLevelButtons();
        }

        // load level buttons on game start
        private void LoadLevelButtons()
        {
            CategoryPack[] Categories = GameManager.Instance.GetCategories();

            float buttonAreaSize = 0; 
            for (int i = 0; i < Categories.Length; i++)
            {
                CategoryTextItem cat = Instantiate(CatPref, transform);
                buttonAreaSize += cat.GetRectTransform().rect.height;
                Color c = Categories[i].categoryColor;
                cat.SetColor(c);
                cat.SetName(Categories[i].name);
                for (int j = 0; j < Categories[i].lotes.Length; j++)
                {
                    SlotButtonItem slotButton = Instantiate(SlotPref, transform);
                    buttonAreaSize += slotButton.GetButonRectTransform().rect.height;
                    slotButton.SetCategory(i);
                    slotButton.SetSlot(j);
                    slotButton.SetText(Categories[i].lotes[j].packName, Categories[i].categoryColor);
                }
            }

            buttonAreaRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, buttonAreaSize);
        }

    }
}