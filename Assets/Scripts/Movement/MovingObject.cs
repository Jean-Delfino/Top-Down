using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface MovingObject {
    void Move(Vector2 velocity);
    void DoAnimation(string animationName, bool state);
} 
