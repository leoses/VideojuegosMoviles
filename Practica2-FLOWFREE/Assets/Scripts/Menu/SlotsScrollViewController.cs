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
        //[SerializeField] private Text l1Text;
        //[SerializeField] private Text e1Text;
        //[SerializeField] private Text vText;
        //[SerializeField] private Text e2Text;
        //[SerializeField] private Text l2Text;
        //[SerializeField] private Text sText;

        private Color[] lettersColors;
        private void Start()
        {
            lettersColors = GameManager.Instance.GetColorTheme().colorTheme;
            RectTransform buttonRect = SlotPref.GetButonRectTransform();

            buttonAreaRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 600);

            //l1Text.color = lettersColors[0];
            //e1Text.color = lettersColors[1];
            //vText.color = lettersColors[2];
            //e2Text.color = lettersColors[3];
            //l2Text.color = lettersColors[4];
            //sText.color = lettersColors[5];
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