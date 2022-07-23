using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    The basic needed for a creature
*/

public class Creature : MonoBehaviour{
    protected float health = 0;

    protected void ChangeLife(float amount){
        health += amount;
    }
}
