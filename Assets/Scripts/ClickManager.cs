using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/*
ClickManager Class
This Class should be placed on the main tilemap along with the BoardManager.
Only one instance of this class should exist.

This class processes clicks - especially clicks on the tilemap. 
It handles selection of hikers and moving them before putting the request through to BoardManager.
*/
public class ClickManager : MonoBehaviour
{
    Tilemap tileMap;
    BoardManager boardManager;
    Vector3Int? selectedHiker = null; //null if no hiker is selected. refers to grid pos of hiker if one is selected.

    //Start is called on game load
    void Start()
    {
        tileMap = GetComponent<Tilemap>();
        boardManager = GetComponent<BoardManager>();
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
    Attempts a move if a hiker is selected, Selects a hiker if otherwise and it is present.
    */
    void OnTileClicked(Vector3Int tilePos, TileBase tile)
    {       
        if(selectedHiker == null) //NO HIKER SELECTED--------------------
        {
            GameObject objectOnTile = boardManager.GetObjectAtTile(tilePos);
            if(objectOnTile == null){return;}
            
            //Activate Hiker Movement Mode
            if(objectOnTile.CompareTag("Hiker"))
            {
                selectedHiker = tilePos;

                //Logging line - left in zombie form because of its moderate complexity - uncomment to enable
                //Debug.Log("Selected Hiker at "+tilePos.ToString());
            }
        }else{                    //HIKER SELECTED------------------------
            boardManager.AttemptMove((Vector3Int)selectedHiker, tilePos);
            selectedHiker = null;
        }
    }
}
