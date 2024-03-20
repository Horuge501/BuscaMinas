using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform tilePrefab;
    [SerializeField] private Transform gameHolder;

    private List<Tile> tiles = new();

    private int width;
    private int height;
    private int numberMines;

    private readonly float tileSize = 0.5f;

    void Start()
    {
        CreateGameBoard(9, 9, 10);
        ResetGameState();
    }

    public void CreateGameBoard(int widthB, int heightB, int numberMinesB) 
    {
        width = widthB;
        height = heightB;
        numberMines = numberMinesB;

        for (int row = 0; row < heightB; row++) 
        {
            for (int col = 0; col < widthB; col++) 
            {
                Transform tileTransform = Instantiate(tilePrefab);
                tileTransform.parent = gameHolder;
                float xIndex = col - ((widthB - 1) / 2);
                float yIndex = row - ((heightB - 1) / 2);
                tileTransform.localPosition = new Vector2(xIndex * tileSize, yIndex * tileSize);
                Tile tile = tileTransform.GetComponent<Tile>();
                tiles.Add(tile);
            }
        }
    }

    private void ResetGameState() 
    {
        int[] minePositions = Enumerable.Range(0, tiles.Count).OrderBy(x => Random.Range(0, 1)).ToArray();

        for (int i = 0; i < numberMines; i++) 
        {
            int pos = minePositions[i];
            tiles[pos].isMine = true;
        }

        for (int i = 0; i < tiles.Count; i++) 
        {
            tiles[i].mineCount = HowManyMines(i);
        }
    }

    private int HowManyMines(int location) 
    {
        int count = 0;
        foreach (int pos in GetNeighbours(location)) 
        {
            if (tiles[pos].isMine) 
            {
                count++;
            }
        }
        return count;
    }

    private List<int> GetNeighbours(int pos) 
    {
        List<int> neighbours = new();
        int row = pos / width;
        int col = pos % width;

        if (row < (height -1)) 
        {
            neighbours.Add(pos + width);
            if (col > 0) 
            {
                neighbours.Add(pos + width - 1);
            }
            if (col < (width - 1)) 
            {
                neighbours.Add(pos + width + 1);
            }
        }
        if (col > 0) 
        {
            neighbours.Add(pos - 1);
        }
        if (col < (width - 1)) 
        {
            neighbours.Add(pos + 1);
        }
        if (row > 0) 
        {
            neighbours.Add(pos - width);
            if (col > 0) 
            {
                neighbours.Add(pos - width - 1);
            }
            if (col < (width - 1)) 
            {
                neighbours.Add(pos - width + 1);
            }
        }   
        return neighbours;
    }
}
