using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Map
{
    int width;
    int height;
    int lvlnum;
    int flownum;
    List<int> bridge;
    List<int> holes;
    List<int> walls;
    List<List<int>> pipes;
    public bool Parse(string lvl)
    {
        string[] data = lvl.Split(';');

        //Cabecera
        //PREGUNTAR A PEBLO PORQUE NOS DA MAPAS ROTOS ALWAYS EN INGLES 
        string cabecera = data[0].Trim('B', '+');

        string[] comas = cabecera.Split(',');
        List<int> dim = comas[0].Split(':').Select(int.Parse).ToList();
        width = dim[0];
        height = dim.Count == 1 ? width : dim[1];
        lvlnum = int.Parse(comas[2]);
        flownum = int.Parse(comas[3]);

        ///Otro dia se vera 
        if (comas.Length > 4)
        {
            bridge = comas[4].Split(':').Select(int.Parse).ToList();
        }
        if (comas.Length > 5)
        {
            holes = comas[5].Split(':').Select(int.Parse).ToList();
        }
        if (comas.Length > 6)
        {
            walls = comas[6].Split('|').Select(int.Parse).ToList();
        }

        pipes = new List<List<int>>();

        for (int i = 1; i < data.Length; i++)
        {
            pipes.Add(data[i].Split(',').Select(int.Parse).ToList());
        }
        return true;
    }


    public List<List<int>> GetPipes() { return pipes; }
    public List<int> Getbridge() { return bridge; }
    public List<int> Getholes() { return holes; }
    public List<int> Getwalls() { return walls; }
    public int GetWidth() { return width; }
    public int GetHeight() { return height; }

    public int GetFlownum() { return flownum; }
    public int GetLvlnum()  { return lvlnum; }

    
}
