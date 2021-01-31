using UnityEngine;

public class EnumUtils
{
    public static Vector2 ConvertToVector(MovementSide side)
    {
        int horizontalValue = 0;
        int verticalValue = 0; 
        
        if (Mathf.Abs((int) side / 2) < 1)
        {
            horizontalValue = 0;
            verticalValue = (int) side;
        }
        else
        {
            horizontalValue = (int) side/2;
            verticalValue = 0;
        }

        return new Vector2(horizontalValue, verticalValue);
    }
}