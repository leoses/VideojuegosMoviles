using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FreeFlowGame
{
    public class PipeController : MonoBehaviour
    {
        [SerializeField]
        private GameObject pipeObject;

        [SerializeField]
        private Transform pipeParent;

        [SerializeField]
        SpriteRenderer pipeRenderer;

        private BoardManager boardManager;

        bool draw = false;

        Vector2 posIni;
        Vector2 posAct;
        Tile tileIni;
        Tile tileAct;
        Tile lasttile;

        void Start()
        {
            boardManager = GameManager.Instance.GetBoardManager();
        }



        void Update()
        {
            Vector2 posInBoard = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D ra = Physics2D.Raycast(posInBoard, -Vector2.up);

            if (ra.collider != null && Input.GetMouseButtonDown(0))
            {
                Vector2 posAbsBoard = new Vector2(Mathf.RoundToInt(posInBoard.x), Mathf.RoundToInt(posInBoard.y));
                tileIni = boardManager.GetTileAtPosition(posAbsBoard);
                if (tileIni != null && !tileIni.IsEmpty())
                {
                    draw = true;
                    posIni = posAbsBoard;
                    posAct = posAbsBoard;
                    pipeRenderer.color = tileIni.GetCircleColor();
                }
            }
            else if (draw && Input.GetMouseButton(0))
            {
                Vector2 posAbsBoard = new Vector2(Mathf.RoundToInt(posInBoard.x), Mathf.RoundToInt(posInBoard.y));
                tileAct = boardManager.GetTileAtPosition(posAbsBoard);

                if (ra.collider != null && tileAct != null && tileAct != tileIni && tileAct.IsEmpty())
                {

                    Vector2 dir =  posAbsBoard  - posAct  ;
                    if (dir.x != 0 && dir.y != 0) { Debug.Log("No me puedo mover en diagonal pai"); }
                    else
                    {
                        if (boardManager.GetTileAtPosition(posAbsBoard) != null)
                        {
                            float angle = Mathf.Atan2(-dir.x, dir.y) * Mathf.Rad2Deg;
                            Quaternion rot = Quaternion.Euler(0f, 0f, angle);
                            posAct = posAbsBoard;
                            Instantiate(pipeObject, new Vector3(posAct.x, posAct.y),rot, pipeParent);
                            tileAct.setFree(false);
                        }
                    }
                }
                if (ra.collider != null && tileAct != null && tileAct != tileIni && !tileAct.IsEmpty() && tileAct.GetCircleColor() == pipeRenderer.color)
                {
                    draw = false;
                    Debug.Log("Pipe Completada");
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                draw = false;
            }
        }
    }
}
