using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    public static int LevelScore = 0;

    private static MazeGenerator instance;
    [SerializeField] private GameObject CellPrefab;

    [SerializeField] private Vector3 CellSize = new Vector3(2, 0, 2);

    private MazeCreatorCell[,] _maze;

    [SerializeField] private MazeDifficulty difficulty = MainMenuController.SelectedDifficulty;

    private void Start()
    {
        instance = this;
    }
    public static void MazeSpawning(MazeDifficulty difficulty)
    {
        if (difficulty == null) return;
        
        instance._maze = difficulty.GenerateMaze();

        for (var x = instance._maze.GetLength(0) - 1; x >= 0; x--)
        {
            for (var z = instance._maze.GetLength(1) - 1; z >= 0; z--)
            {
                var cell = Instantiate(instance.CellPrefab, new Vector3(x * instance.CellSize.x, z * instance.CellSize.y, z * instance.CellSize.z), Quaternion.identity).GetComponent<Cell>();

                cell.WallLeft.SetActive(instance._maze[x, z].WallLeft);
                cell.WallBottom.SetActive(instance._maze[x, z].WallBottom);
                cell.Floor.SetActive(instance._maze[x, z].Floor);
                cell.Star.SetActive(instance._maze[x, z].Star);
                cell.Exit.SetActive(instance._maze[x, z].Exit);
            }
        }
    }
}
