using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
HikerCanMove class
This class performs verification of legal moves on the hiker end.
*/
public class HikerCanMove : MonoBehaviour
{
    public bool CanMove(Vector3Int fromPos, Vector3Int toPos)
    {
        //Temporary behaviour - always return true!
        //checks such as movement limits or distance might happen here
        return true;
    }
}
