using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UtilStrings{
    
    public static string ConvertPositiveNumberToFixedSize(int number, int size){
        return number.ToString().PadLeft(size, '0');
    }
}
