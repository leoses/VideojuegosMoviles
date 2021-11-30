using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FreeFlowGame
{
    public class PipeController : MonoBehaviour
    {
        [SerializeField]
        private GameObject pipeObject;

        private Dictionary<Color, Transform> pipeParent;
        private Dictionary<Color, List<GameObject>> pipeList;

        [SerializeField]
        SpriteRenderer pipeRenderer;

        private BoardManager boardManager;

        bool draw = false;

        Vector2 posIni;
        Vector2 posAct;
        Tile tileIni;
        Tile tileAct;

        void Start()
        {
            boardManager = GameManager.Instance.GetBoardManager();
            pipeParent = new Dictionary<Color, Transform>();
            pipeList = new Dictionary<Color, List<GameObject>>();

            Color[] c = boardManager.getPipesColor();
            for (int i = 0; i < c.Length; i++)
            {
                GameObject par = new GameObject();
                par.name = c[i].ToString();
                pipeParent.Add(c[i], par.transform);
                pipeList.Add(c[i], new List<GameObject>());
            }


        }

        private void DestroyChildren(Transform tr)
        {
            foreach (Transform child in tr)
            {
                pipeList[pipeRenderer.color].Remove(child.gameObject);
                boardManager.GetTileAtPosition(child.position).setFree(true);
                boardManager.GetTileAtPosition(child.position).setIndex(-1);
                Destroy(child.gameObject);
            }
        }

        private void DestroyChildrenFromIndex(Transform tr, int index)
        {
            for (int i = index; i < tr.childCount; i++)
            {
                pipeList[tileAct.GetColor()].Remove(tr.GetChild(i).gameObject);
                boardManager.GetTileAtPosition(tr.GetChild(i).position).setFree(true);
                boardManager.GetTileAtPosition(tr.GetChild(i).position).setIndex(-1);
                Destroy(tr.GetChild(i).gameObject);

            }
        }

        void Update()
        {
            //TO DO LIMPIAR CODIGO EN METODOS
            Vector2 posInBoard = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D ra = Physics2D.Raycast(posInBoard, -Vector2.up);

            if (ra.collider != null && Input.GetMouseButtonDown(0))
            {
                Vector2 posAbsBoard = new Vector2(Mathf.RoundToInt(posInBoard.x), Mathf.RoundToInt(posInBoard.y));
                tileIni = boardManager.GetTileAtPosition(posAbsBoard);
                if (tileIni != null)
                {
                    if (tileIni.IsCircle())
                    {
                        draw = true;
                        posIni = posAbsBoard;
                        posAct = posAbsBoard;
                        pipeRenderer.color = tileIni.GetCircleColor();
                        DestroyChildren(pipeParent[pipeRenderer.color]);
                    }
                    else if (tileIni.getIndex() != -1)
                    {
                        draw = true;
                        pipeRenderer.color = tileIni.GetColor();
                        posAct = posAbsBoard;
                        DestroyChildrenFromIndex(pipeParent[pipeRenderer.color], tileIni.getIndex() + 1);
                    }
                }

            }
            else if (draw && Input.GetMouseButton(0))
            {
                Vector2 posAbsBoard = new Vector2(Mathf.RoundToInt(posInBoard.x), Mathf.RoundToInt(posInBoard.y));
                tileAct = boardManager.GetTileAtPosition(posAbsBoard);
                if (ra.collider != null && tileAct != null)
                {
                    if (!tileAct.IsCircle())
                    {
                        if (tileAct.isFree())
                        {
                            Vector2 dir = posAbsBoard - posAct;
                            if (dir.x != 0 && dir.y != 0) { Debug.Log("No me puedo mover en diagonal pai"); }
                            else
                            {
                                if (boardManager.GetTileAtPosition(posAbsBoard) != null)
                                {
                                    float angle = Mathf.Atan2(-dir.x, dir.y) * Mathf.Rad2Deg;
                                    Quaternion rot = Quaternion.Euler(0f, 0f, angle);
                                    posAct = posAbsBoard;
                                    pipeList[pipeRenderer.color].Add(Instantiate(pipeObject, new Vector3(posAct.x, posAct.y), rot, pipeParent[pipeRenderer.color]));
                                    tileAct.setFree(false);
                                    tileAct.setIndex(pipeList[pipeRenderer.color].Count - 1);
                                    tileAct.SetColor(pipeRenderer.color);
                                    Debug.Log(pipeList[pipeRenderer.color].Count - 1);
                                }
                            }
                        }
                        else if (tileAct.GetColor() != pipeRenderer.color)
                        {
                            DestroyChildrenFromIndex(pipeParent[tileAct.GetColor()], tileAct.getIndex());
                        }
                    }

                    bool destroy = false;
                    if (tileAct.IsCircle() && tileAct == tileIni && pipeList[pipeRenderer.color].Count == 1)
                    {
                        destroy = true;
                        posAct = posIni;

                    }
                    else if (!tileAct.isFree() && pipeList[pipeRenderer.color].Count > 1 && tileAct == boardManager.GetTileAtPosition(pipeList[pipeRenderer.color][pipeList[pipeRenderer.color].Count - 2].transform.position))
                    {
                        destroy = true;
                        posAct = pipeList[pipeRenderer.color][pipeList[pipeRenderer.color].Count - 2].transform.position;
                    }
                    if (destroy)
                    {
                        GameObject des = pipeList[pipeRenderer.color][pipeList[pipeRenderer.color].Count - 1];
                        boardManager.GetTileAtPosition(des.transform.position).setFree(true);
                        boardManager.GetTileAtPosition(des.transform.position).setIndex(-1);
                        pipeList[pipeRenderer.color].Remove(des);
                        Destroy(des);
                    }

                    if (tileAct.IsCircle() && tileAct.GetCircleColor() == pipeRenderer.color && tileAct != tileIni)
                    {
                        draw = false;
                        Debug.Log("Pipe Completada");
                    }
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                draw = false;
            }

            if (Input.GetKeyUp(KeyCode.Space)) { PintarPista(); }
        }
        public void PintarPista()
        {
            List<Vector2> l= boardManager.getRandomPipe();
            Color color=  boardManager.GetTileAtPosition(l[0]).GetCircleColor() ;
            pipeRenderer.color = color;
            for (int i = 1; i < l.Count; i++)
            {
                Vector2 dir = l[i] - l[i - 1];
                float angle = Mathf.Atan2(-dir.x, dir.y) * Mathf.Rad2Deg;
                Quaternion rot = Quaternion.Euler(0f, 0f, angle);
                pipeList[color].Add(Instantiate(pipeObject, l[i], rot, pipeParent[color]));
                tileAct = boardManager.GetTileAtPosition(l[i]);
                tileAct.setFree(false);
                tileAct.setIndex(pipeList[color].Count - 1);
                tileAct.SetColor(color);
            }
         
        }
    }

}
