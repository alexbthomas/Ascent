using UnityEngine;

public class PlayerResources : MonoBehaviour
{
    public int currentSpark = 100;

    public void AddSpark(int amount)
    {
        currentSpark += amount;
        Debug.Log("Added Spark. Current Spark: " + currentSpark);
    }

    public bool UseSpark(int amount)
    {
        if (currentSpark >= amount)
        {
            currentSpark -= amount;
            Debug.Log("Used Spark for ability. Remaining Spark: " + currentSpark);
            return true;
        }
        Debug.Log("Not enough Spark to use ability.");
        return false;
    }
}
