using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldScrolling : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    Vector2Int currentTilePosition = new Vector2Int(0,0);
    [SerializeField] Vector2Int playerTilePosition;
    Vector2Int onTileGridPlayerPosition;
    [SerializeField] float tileSize = 20f;
    GameObject[,] terrainTiles;


    [SerializeField] int terrainTileHorizontalCount;
    [SerializeField] int terrainTileVerticalCount;

    [SerializeField] int fieldOfVisionHeight = 3;
    [SerializeField] int fieldOfVisionWidth = 3;

    private void Awake()
    {
        terrainTiles = new GameObject[terrainTileHorizontalCount,terrainTileVerticalCount];
    }

    private void Start()
    {
        UpdateTileOnScreen();
    }

    private void Update()
    {
        playerTilePosition.x = (int)(playerTransform.position.x / tileSize);
        playerTilePosition.y = (int)(playerTransform.position.y / tileSize); 
        
        playerTilePosition.x -= playerTransform.position.x <0 ? 1:0;
        playerTilePosition.y -= playerTransform.position.y <0 ? 1:0;

        if(currentTilePosition != playerTilePosition)
        {
            currentTilePosition = playerTilePosition;

            onTileGridPlayerPosition.x = CalculatPositionOnAxis(onTileGridPlayerPosition.x, true);
            onTileGridPlayerPosition.y = CalculatPositionOnAxis(onTileGridPlayerPosition.y, true);
            UpdateTileOnScreen();
        }
    }

    private void UpdateTileOnScreen()
    {
        for(int pov_x = -(fieldOfVisionWidth/2); pov_x <= fieldOfVisionWidth/2; pov_x++)
        {
            for (int pov_y = -(fieldOfVisionHeight/2); pov_y <= fieldOfVisionHeight/2; pov_y++)
            {
                int tileToUpdate_x = CalculatPositionOnAxis(playerTilePosition.x + pov_x, true);
                int tileToUpdate_y = CalculatPositionOnAxis(playerTilePosition.y + pov_y, false);

                //Debug.Log("TileToUpdate_x" + tileToUpdate_x + " TileToUpdate_y" + tileToUpdate_y);

                GameObject tile = terrainTiles[tileToUpdate_x, tileToUpdate_y];
                tile.transform.position = CalculatTilePosition(playerTilePosition.x + pov_x, playerTilePosition.y + pov_y);
            }
        }
    }

    private Vector3 CalculatTilePosition(int v1, int v2)
    {
        return new Vector3(v1 * tileSize, v2 * tileSize, 0f);
    }

    private int CalculatPositionOnAxis(float currentValue, bool horizontal)
    {
        if (horizontal)
        {
            if (currentValue >= 0)
            {
                currentValue = currentValue % terrainTileHorizontalCount;
            }
            else
            {
                currentValue += 1;
                currentValue = terrainTileHorizontalCount -1 + currentValue % terrainTileHorizontalCount;
            }
        }
        else
        {
            if (currentValue >= 0)
            {
                currentValue = currentValue % terrainTileVerticalCount;
            }
            else
            {
                currentValue += 1;
                currentValue = terrainTileVerticalCount -1 + currentValue % terrainTileVerticalCount;
            }
        }
        //if(onTileGridPlayerPosition.x > 0)
        //{
        //    onTileGridPlayerPosition.x = playerTilePosition.x % terrainTileHorizontalCount;
        //}
        //else
        //{
        //    onTileGridPlayerPosition.x = terrainTileHorizontalCount -1 + playerTilePosition.x % terrainTileVerticalCount;
        //}

        //if(onTileGridPlayerPosition.y > 0)
        //{
        //    onTileGridPlayerPosition.y = playerTilePosition.y % terrainTileVerticalCount;
        //}
        //else
        //{
        //    onTileGridPlayerPosition.y = terrainTileVerticalCount -1 + playerTilePosition.y % terrainTileHorizontalCount;
        //}

        return (int)currentValue;
    }

    public void Add(GameObject tileGameObject, Vector2Int tilePosition)
    {
        terrainTiles[tilePosition.x, tilePosition.y] = tileGameObject;
    }
}
