using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UtilInt{
    public static int checkBound(ref int counter, int limit){
        int toAdd = 0;

        while(counter > limit - 1){
            counter -= limit;
            toAdd++;
        }

        return toAdd;
    }
}
