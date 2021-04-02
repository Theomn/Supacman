using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LevelController : MonoBehaviour
{
    public TextAsset levelText;

    public Transform levelLocation;
    public GameObject wallPrefab;
    public GameObject tokenPrefab;
    public GameObject endPrefab;
    public GameObject vilainPrefab;

    public int levelWidth { get; private set; }
    public int levelHeight { get; private set; }
    private Vector3 levelCenter;

    private List<GameObject> endBlocks;
    private int tokenRemaining;
    
    private float wallSize = 3f;

    /// <summary>
    /// Represents the level walls layout as a boolean 2D array. Dimension in this order: grid[y][x]
    /// </summary>
    public List<List<bool>> grid { get; private set; }

    private void Awake()
    {
        GameAccessor.Instance().level = this;
        grid = new List<List<bool>>();
        endBlocks = new List<GameObject>();
    }

    public Vector3 LevelToWorldPosition(int x, int y)
    {
        return levelLocation.position + Vector3.right * x * wallSize + Vector3.back * y * wallSize;
    }

    public void TakeToken()
    {
        tokenRemaining--;
        if (tokenRemaining <= 0)
        {
            foreach (GameObject endBlock in endBlocks)
            {
                endBlock.SetActive(true);
            }
        }
    }



    // LEVEL GENERATION

    public void Load()
    {
        StringReader reader = new StringReader(levelText.text);
        string line;
        int x, y = 1;
        grid.Add(new List<bool>());
        while ((line = reader.ReadLine()) != null)
        {
            grid.Add(new List<bool>());
            PlaceWallAt(0, y);
            grid[y].Add(true);
            x = 1;
            foreach (char c in line.ToCharArray())
            {
                grid[y].Add(false);
                if (c == 'X')
                {
                    PlaceWallAt(x, y);
                    grid[y][x] = true;
                }
                else if (c == '*')
                {
                    PlaceTokenAt(x, y);
                }
                else if (c == 'e')
                {
                    PlaceEndAt(x, y);
                }
                else if (c == 'p')
                {
                    PlacePlayerAt(x, y);
                }
                else if (c == '1')
                {
                    PlaceVilain1At(x, y);
                }
                else if (c == '2')
                {
                    PlaceVilain2At(x, y);
                }
                x++;
            }
            PlaceWallAt(x, y);
            grid[y].Add(true);
            levelWidth = Mathf.Max(x, levelWidth);
            y++;
        }
        levelHeight = y;
        levelCenter = Vector3.right * ((levelWidth / 2f) * wallSize) + Vector3.back * ((levelHeight / 2f) * wallSize);
        GameAccessor.Instance().camera.transform.position = levelCenter + Vector3.up * ((Mathf.Max(levelWidth, levelHeight)*wallSize)+5);
        PlaceBoundaries();
        AStarAlgorithm.Initialize(grid);
    }

    private void PlaceWallAt(int x, int y)
    {
        GameObject gameObject = Instantiate(wallPrefab);
        gameObject.transform.position = LevelToWorldPosition(x, y);
        gameObject.transform.parent = levelLocation;
    }

    private void PlaceEndAt(int x, int y)
    {
        GameObject gameObject = Instantiate(endPrefab);
        gameObject.transform.position = LevelToWorldPosition(x, y);
        gameObject.transform.parent = levelLocation;
        gameObject.SetActive(false);
        endBlocks.Add(gameObject);
    }

    private void PlaceTokenAt(int x, int y)
    {
        GameObject gameObject = Instantiate(tokenPrefab);
        gameObject.transform.position = LevelToWorldPosition(x, y);
        gameObject.transform.parent = levelLocation;
        tokenRemaining++;
    }

    private void PlacePlayerAt(int x, int y)
    {
        Player player = GameAccessor.Instance().player;
        player.transform.position = LevelToWorldPosition(x, y);
        player.x = x;
        player.y = y;
    }

    private void PlaceVilain1At(int x, int y)
    {
        GameObject gameObject = Instantiate(vilainPrefab);
        gameObject.transform.position = LevelToWorldPosition(x, y);
        var agent = gameObject.GetComponent<ControllableAgent>();
        agent.x = x;
        agent.y = y;
        agent.controllerStrategy = new AIHorizontalVilainStrategy();
    }

    private void PlaceVilain2At(int x, int y)
    {
        GameObject gameObject = Instantiate(vilainPrefab);
        gameObject.transform.position = LevelToWorldPosition(x, y);
        var agent = gameObject.GetComponent<ControllableAgent>();
        agent.x = x;
        agent.y = y;
        agent.controllerStrategy = new AIDiagonalVilainStrategy();
    }

    private void PlaceBoundaries()
    {
        grid.Add(new List<bool>());
        for (int x = 0; x <= levelWidth; x++)
        {
            PlaceWallAt(x, 0);
            grid[0].Add(true);
            PlaceWallAt(x, levelHeight);
            grid[levelHeight].Add(true);
        }

        GameObject floor = Instantiate(wallPrefab);
        floor.transform.position = levelLocation.position + levelCenter + Vector3.down;
        floor.transform.localScale = Vector3.right * levelWidth * wallSize + Vector3.up + Vector3.forward * levelHeight * wallSize;
        floor.name = "Floor";
        floor.transform.parent = levelLocation;
    }

    public void DebugPrintLevelMatrix()
    {
        string outputString = string.Empty;
        foreach(List<bool> line in grid)
        {
            foreach(bool tile in line)
            {
                outputString += tile ? 'X' : '.';
            }
            outputString += '\n';
        }
        Debug.Log(outputString);
    }
}
