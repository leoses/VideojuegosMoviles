using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FlowFreeGame
{
    public class Map
    {
        private int width;
        private int height;
        private int numPipes;

        private List<int> bridge;
        private List<int> holes;
        private List<List<Vector2>> pipes;

        //Vector2-> posicion en el tablero, int -> cara en la que hay pared
        private Dictionary<Vector2, bool[]> wallsInBoard;
        private List<Vector2> holesPosInBoard;

        public bool Parse(string lvl)
        {
            wallsInBoard = new Dictionary<Vector2, bool[]>();
            holesPosInBoard = new List<Vector2>();
            string[] data = lvl.Split(';');
            string cabecera = data[0].Replace("+B", "");
            string[] comas = cabecera.Split(',');
            List<int> dim = comas[0].Split(':').Select(int.Parse).ToList();
            width = dim[0];
            height = dim.Count == 1 ? width : dim[1];
            int lvlnum = int.Parse(comas[2]);
            numPipes = int.Parse(comas[3]);

            if (comas.Length > 4 && comas[4].Length > 0)
            {
                bridge = comas[4].Split(':').Select(int.Parse).ToList();
            }
            if (comas.Length > 5 && comas[5].Length > 0)
            {
                holes = comas[5].Split(':').Select(int.Parse).ToList();

                foreach (int hole in holes)
                {
                    holesPosInBoard.Add(GetPosInBoard(hole));
                }

                foreach(Vector2 holePos in holesPosInBoard)
                {
                    List<Vector2> adyacents = GetAdyTilesInBoard(holePos);

                    foreach(Vector2 ady in adyacents)
                    {
                        if (!holesPosInBoard.Contains(ady))
                        {
                            //Suponemos por defecto adyacente abajo-arriba
                            int wall = 0;
                            //Son adyacentes iz-dcha
                            if (holePos.x + 1 == ady.x) { wall = 1; }
                            //Son adyacentes dcha-izq
                            else if (holePos.x - 1 == ady.x) { wall = 3; }
                            //Son adyacentes arriba-abajo
                            else if (holePos.y - 1 == ady.y) { wall = 2; }


                            AddWallToDictionary(ady, wall );
                        }
                    }
                }

            }
            if (comas.Length > 6 && comas[6].Length > 0)
            {
                char[] sep = { '|', ':' };
                List<int> walls = comas[6].Split(sep).Select(int.Parse).ToList();

                for (int j = 0; j < walls.Count - 1; j += 2)
                {
                    int valor = walls[j];
                    int valor2 = walls[j + 1];
                    //Suponemos por defecto adyacente abajo-arriba
                    int wall = 0;
                    //Son adyacentes iz-dcha
                    if (valor + 1 == valor2) { wall = 1; }
                    //Son adyacentes dcha-izq
                    else if (valor - 1 == valor2) { wall = 3; }
                    //Son adyacentes arriba-abajo
                    else if (valor + height == valor2) { wall = 2; }


                    AddWallToDictionary(GetPosInBoard(valor), wall);
                    AddWallToDictionary(GetPosInBoard(valor2), (wall + 2) % 4);
                }
            }

            pipes = new List<List<Vector2>>();
            for (int i = 1; i < data.Length; i++)
            {
                string[] pipe = data[i].Split(',');
                List<Vector2> aux = new List<Vector2>();
                for (int j = 0; j < pipe.Length; j++)
                {
                    aux.Add(GetPosInBoard(int.Parse(pipe[j])));
                }
                pipes.Add(aux);
            }

            return true;
        }

        private List<Vector2> GetAdyTilesInBoard(Vector2 ady)
        {
            List<Vector2> adyList = new List<Vector2>();
            if (ady.x != 0) adyList.Add(new Vector2(ady.x - 1, ady.y));
            if(ady.x != width - 1) adyList.Add(new Vector2(ady.x + 1, ady.y)); ;
            if (ady.y != 0) adyList.Add(new Vector2(ady.x, ady.y-1));
            if (ady.y != height - 1) adyList.Add(new Vector2(ady.x, ady.y+1)); ;

            return adyList;
        }

        private Vector2 GetPosInBoard(int valor)
        {
            int posX = valor % width;
            int posY = valor / height;
            if (width != height)
            {
                if (width < height) posY = valor / width;
            }
            return new Vector2(posX, -posY);
        }

        private void AddWallToDictionary(Vector2 pos, int wallPos)
        {
            if (!wallsInBoard.ContainsKey(pos))
            {
                bool[] array = new bool[4];
                array[wallPos] = true;
                wallsInBoard.Add(pos, array);
            }
            else wallsInBoard[pos][wallPos] = true;
        }

        public List<List<Vector2>> GetPipes() { return pipes; }

        public List<int> Getbridge() { return bridge; }

        public List<int> Getholes() { return holes; }

        public int GetWidth() { return width; }

        public int GetHeight() { return height; }

        public int GetNumPipes() { return numPipes; }

        public Dictionary<Vector2, bool[]> GetWallsInBoard() { return wallsInBoard; }

    }
}
