using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int score = 0;
    public static GameManager instance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created    
    void Awake()
    {
        instance = this;
    }

    // Function to update score
    public void AddScore(int value)
    {
        score += value;
        Debug.Log("Score : " + score);
    }

}
