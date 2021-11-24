using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FreeFlowGame
{
    public class BoardManager : MonoBehaviour
    {
        private bool boardComplete;

        [SerializeField] 
        private Tile _tilePrefab;

        [SerializeField] 
        private Transform _cam;

        [SerializeField]
        private Color[] pipesColor;

        
        private Transform boardParent;

        List<List<Vector2>> pipes;

        private Dictionary<Vector2, Tile> _tiles;

        private Map m;

        [SerializeField]private Object scene;
     
        public void Initialize()
        {
            pipes = new List<List<Vector2>>();
            m = new Map();
            boardParent = new GameObject().transform;
            boardParent.name = "BoardParent";
            Debug.Log("Initialize");
            setPipes();
            GenerateGrid();
        }

        public void Clear() 
        {
            Destroy(boardParent.gameObject);
        }
        private void setPipes()
        {
            LvlActual lvl= GameManager.Instance.getActualPlay();
            Debug.Log(lvl.levelIndex);
            m.Parse(LectutaLote.Instance.getDictionaryCategories()[lvl.category][lvl.slotIndex].levels[lvl.levelIndex]);
            pipes = m.GetPipes();
        }

        void GenerateGrid()
        {
            _tiles = new Dictionary<Vector2, Tile>();
            
            for (int i = 0; i < m.GetFlownum(); i++)
            {
                for (int j = 0; j < pipes[i].Count; j++)
                {   
                    var spawnedTile = Instantiate(_tilePrefab, new Vector3(pipes[i][j].x, pipes[i][j].y), Quaternion.identity, boardParent);
                    spawnedTile.name = $"Tile {pipes[i][j].x} {pipes[i][j].y}";
                    if(j == 0 || j == pipes[i].Count - 1)
                    {
                        spawnedTile.Init(false);
                        spawnedTile.SetCircleColor(pipesColor[i]);
                    }
                    else spawnedTile.Init(true);

                    spawnedTile.SetPosTile(new Vector2(pipes[i][j].x, pipes[i][j].y));

                    _tiles[new Vector2(pipes[i][j].x, pipes[i][j].y)] = spawnedTile;
                }
            }

            _cam.transform.position = new Vector3((float)m.GetWidth() / 2 - 0.5f, (float)m.GetHeight() / 2 - 5f, -10);
        }

        public Tile GetTileAtPosition(Vector2 pos)
        {
            if (_tiles.TryGetValue(pos, out var tile)) return tile;
            return null;
        }

        public Object getScene() 
        {
            return scene;
        }
    }
}
