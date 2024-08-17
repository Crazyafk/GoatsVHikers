using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

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
