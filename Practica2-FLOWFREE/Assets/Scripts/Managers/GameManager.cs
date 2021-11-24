using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public struct LvlActual
{
    public int category;
    public int slotIndex;
    public int levelIndex;
}

public class GameManager : MonoBehaviour
{
    #region SerializeVariables
    [SerializeField]
    private FreeFlowGame.BoardManager boardManager;

    //[SerializeField] private LectutaLote lvlManager;
    #endregion

    #region PublicVariables
    //Array de los lotes sobre los que vamos a trabajar Intro-Manias-Rectangles
    public CategoryPack[] categories;
    //
    public AdsManager ads;

    //de momento publico para que podamos darle a las escenas sin que se joda 
    public static LvlActual act;

#if UNITY_EDITOR
    //Variables p�blicas para seleccionar desde el inspector que lote, pack y nivel cargamos por defecto. V�lido para probar desde
    //inspector

    public int lote;
    public int pack;
    public int level;
#endif

#endregion

    #region PrivateVariables
    private DataSystem data;
    private static GameManager _instance;

    //Variable de la escena en la que estamos para saber que inicializar sin necesidad de pasar por MainMenu-LevelSelector-FreeFlow
    private Scene actualScene;

    //Variable que controla el numero de pistas
    private int clues;
    private bool saveCorrect;
    #endregion

    public static GameManager Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private void Start()
    {
        data = SaveSystem.LoadData();

        actualScene = SceneManager.GetActiveScene();
        // ads.ShowBanner();
        
        //Peque�o chanchullo de momento para poder probar la funcionalidad directamente sin empezar el menu todo el rato
        //Basicamente inicializo boardManager en el start de gameManager si es la escena del juego la primera 
        //To do quitarlo e intentar que lecturalote no dependa de monobehvoiur
        //LectutaLote.Instance.Initialize();

        InitData();
        if (SceneManager.GetActiveScene().name == boardManager.getScene().name)
        {
            Debug.Log("Inicio");
            boardManager.Initialize();
        }
    }

    private void InitData()
    {
        clues = 0;
        if (data == null)
        {
            saveCorrect = false;
            data = new DataSystem(0);
        }
        else
        {
            saveCorrect = true;
            clues = data.clues;
        }

        //For de Categorias Intro-manias-rectangles
        for (int i = 0; i < categories.Length; i++)
        {
            int numLotes = categories[i].lotes.Length;
            //For leyendo los archivos de cada categor�a, es decir cada lote
            TextAsset[] slot = new TextAsset[numLotes];
            for (int j = 0; j < numLotes; j++)
            {
                slot[j] = categories[i].lotes[j].maps;
                //TO DO FULL CERDADA
                char[] c = new char[1] { '\n' };
                string[] lvls = slot[j].text.Split(c, StringSplitOptions.RemoveEmptyEntries);

                categories[i].lotes[j].levels = lvls;
                categories[i].lotes[j].bestScoresInLevels = new int[lvls.Length];

                if (saveCorrect)
                {
                    categories[i].lotes[j].bestScoresInLevels = data.minFlow[categories[i].categoryName][j];
                }
                else
                {
                    if (data.minFlow.ContainsKey(categories[i].categoryName))
                    {
                        data.minFlow[categories[i].categoryName].Add(categories[i].lotes[j].bestScoresInLevels);
                    }
                    else
                    {
                        List<int[]> lminflow = new List<int[]>();
                        lminflow.Add(categories[i].lotes[j].bestScoresInLevels);
                        data.minFlow.Add(categories[i].categoryName, lminflow);
                    }
                }
            }
        }
    }

    public void DarPista() {
        ads.PlayerRewardedAd(OnRewardedAdSuccess);
    }

    void OnRewardedAdSuccess() 
    {
        Debug.Log("Pista para tu body ");
    }

    public void LevelSuccess() 
    {
        ads.PlayAd();
    }

    private void OnLevelWasLoaded(int level)
    {
        Debug.Log(level);
        Debug.Log(SceneManager.GetActiveScene().name);
        if (SceneManager.GetActiveScene().name == boardManager.getScene().name)
        {
            boardManager.Initialize();
        }
    }

    public FreeFlowGame.BoardManager GetBoardManager()
    {
        return boardManager;
    }

    public void LoadScene(string name)
    {
        DontDestroyOnLoad(this);
        SceneManager.LoadScene(name);
    }

    public DataSystem getData() { return data; }
    public void setData(DataSystem data) { this.data = data; }

    public void Quit()
    {
        SaveSystem.SaveData(data);
       // PlayerPrefs.Save();
        Application.Quit();
    }

    public LvlActual getActualPlay() 
    {
        return act;
    }

    public CategoryPack[] GetCategories()
    {
        return categories;
    }

    public void SetCategory(int cat) 
    {
        act.category = cat;
    }
    public void SetSlot(int slot)
    {
        act.slotIndex = slot;
    }
    public void SetLevel(int lvl)
    {
        act.levelIndex = lvl;
    }

    public void Restart() 
    {
        boardManager.Clear();
        boardManager.Initialize();
    }

    public void NextLevel() 
    {
        if (act.levelIndex + 1 < categories[act.category].lotes[act.slotIndex].levels.Length 
            && (categories[act.category].lotes[act.slotIndex].bestScoresInLevels[act.levelIndex+1]>0 || !categories[act.category].lotes[act.slotIndex].levelblocked))
        {
            act.levelIndex += 1;
            boardManager.Clear();
            boardManager.Initialize();
        }
    } 
    public void BackLevel() 
    {
        if (act.levelIndex - 1 >= 0)
        {
            act.levelIndex -= 1;
            boardManager.Clear();
            boardManager.Initialize();
        }
    }
}