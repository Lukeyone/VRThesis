using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMapGenerator : MonoBehaviour
{
    [SerializeField] List<Tile> tilesList = new List<Tile>();
    [SerializeField] int mapWidth = 3;
    // Start is called before the first frame update
    void Start()
    {
        SetTilesCoords();
    }

    void SetTilesCoords()
    {
        int currentRow = -1;
        for (int i = 0; i < tilesList.Count; i++)
        {
            int remainder = i % mapWidth;
            if (remainder == 0)
                currentRow++;
            tilesList[i].MapCoordinates = new Vector2(currentRow, remainder);
        }
    }
}
