using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
/*
BoardManager class
This Class should be placed on the main tilemap along with the ClickManager.
Only one instance of this class should exist.

This class stores references to objects such as hikers on the board in its array.
Objects with the RegisterOnBoard class register automatically on start.

A major functionality of this class is the AttemptMove function which is used when a move should be made.
*/
public class BoardManager : MonoBehaviour
{
    [SerializeField]
    Vector2Int boardSize;
    GameObject[,] boardObjects;
    Tilemap tileMap;

    //Start is called on game load
    void Start()
    {
        tileMap = GetComponent<Tilemap>();
        boardObjects = new GameObject[boardSize.x,boardSize.y];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Adds a new object to the board 
    public void RegisterObject(GameObject newObject)
    {
        Vector3Int tilePos = tileMap.WorldToCell(newObject.transform.position);
        boardObjects[tilePos.x, tilePos.y] = newObject;
    }
    
    // Gets the Object at a tile position, returning null if there isn't one.
    public GameObject GetObjectAtTile(Vector3Int tilePos)
    {
        return boardObjects[tilePos.x, tilePos.y];
    }

    /* Attempts to move a piece from fromPos to toPos, first checking validity, 
    and then executing the move both in the array and in the scene.
    currently programmed for only hiker movements.

    Throws: InvalidOperationException if there is no object at fromPos.
    */
    public void AttemptMove(Vector3Int fromPos, Vector3Int toPos)
    {
        //Logging line - left in zombie form because of its moderate complexity - uncomment to enable
        Debug.Log("Move attempted from " + fromPos.ToString() + " to " + toPos.ToString());

        //Get and verify moving object
        GameObject movingObject = GetObjectAtTile(fromPos);
        if (movingObject == null) { throw new System.InvalidOperationException("No Object at fromPos"); }

        //---------Verification---------
        bool verified = false;
        //Hiker Verification
        if (movingObject.CompareTag("Hiker"))
        {
            verified = movingObject.GetComponent<HikerCanMove>().CanMove(fromPos, toPos);
        }

        if (!verified) { return; }

        //----------Execution------------
        //This currently assumes no extra pieces on the board - different behaviour required if so (e.g. attacks)

        boardObjects[toPos.x, toPos.y] = movingObject;
        boardObjects[fromPos.x, fromPos.y] = null;

        Vector3 worldPos = tileMap.GetCellCenterWorld(toPos);
        movingObject.transform.position = new Vector3(worldPos.x, worldPos.y, 0f);
    }
    }
