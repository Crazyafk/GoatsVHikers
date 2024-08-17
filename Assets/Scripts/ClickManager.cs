using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ClickManager : MonoBehaviour
{
    Tilemap tileMap;

    //Start is called on game load
    void Start()
    {
        tileMap = GetComponent<Tilemap>();
    }

    //Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) //Only triggers once per click, not once per frame :)
        {
            OnLeftClick();
        }
    }

    /*Handles checks and calculations before OnTileClicked() is called, including:
        - getting the clicked tile position and type
        - disregarding clicks on null (out of bounds) tiles
    */
    void OnLeftClick()
    {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int tilePos = tileMap.WorldToCell(worldPoint);

        TileBase tile = tileMap.GetTile(tilePos);
        if(tile != null)
        {
            OnTileClicked(tilePos, tile);
        }
    }
    /*Called when a tile is clicked.
    */
    void OnTileClicked(Vector3Int tilePos, TileBase tile)
    {
        Debug.Log(tilePos);
    }
}
