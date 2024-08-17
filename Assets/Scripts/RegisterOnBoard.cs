using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
RegisterOnBoard class
This class registers its object with the BoardManager on start.
*/
public class RegisterOnBoard : MonoBehaviour
{
    BoardManager boardManager;
    
    // Start is called before the first frame update
    void Start()
    {
        boardManager = GameObject.Find("Grid/Tilemap").GetComponent<BoardManager>();
        boardManager.RegisterObject(this.gameObject);
    }
}
