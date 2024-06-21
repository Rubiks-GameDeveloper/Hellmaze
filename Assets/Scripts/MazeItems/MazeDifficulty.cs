using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New MazeGenerationSample", menuName = "ScriptableObjects/Difficulty")]
[Serializable]
public class MazeDifficulty : ScriptableObject
{
    [SerializeReference, SubclassPicker] public Algorithm algorithm;
    public int pointCount;
    public int width;
    public int height;
    public int time;

    public MazeCreatorCell[,] GenerateMaze()
    {
        return algorithm.CreateMaze(width, height, pointCount);
    }
}
