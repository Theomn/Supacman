using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LevelLoader : MonoBehaviour
{
    public Transform levelLocation;
    public GameObject wallPrefab;
    public GameObject TokenPrefab;
    public GameObject EndPrefab;

    public int levelWidth { get; private set; }
    public int levelHeight { get; private set; }
    private Vector3 levelCenter;
    
    private float wallSize = 3f;

    public List<List<bool>> level;

    private void Awake()
    {
        GameAccessor.Instance().levelLoader = this;
    }


    public void Load(string levelString)
    {
        StringReader reader = new StringReader(levelString);
        string line;
        int x, y = 0;
        while ((line = reader.ReadLine()) != null)
        {
            PlaceAt(wallPrefab, -1, y);
            x = 0;
            foreach (char c in line.ToCharArray())
            {
                if (c == 'X')
                {
                    PlaceAt(wallPrefab, x, y);
                }
                else if (c == '*')
                {
                    PlaceAt(TokenPrefab, x, y);
                }
                else if(c == 'o')
                {
                    PlaceAt(EndPrefab, x, y);
                }
                x++;
            }
            PlaceAt(wallPrefab, x, y);
            levelWidth = Mathf.Max(x, levelWidth);
            y++;
        }
        levelHeight = y;
        levelCenter = Vector3.right * ((levelWidth / 2f) * wallSize - wallSize / 2f) + Vector3.back * ((levelHeight / 2f) * wallSize - wallSize / 2f);
        GameAccessor.Instance().camera.transform.position = levelCenter + Vector3.up * ((Mathf.Max(levelWidth, levelHeight)*wallSize)+5);
        PlaceBoundaries();
    }

    private void PlaceAt(GameObject prefab, int x, int y)
    {
        GameObject gameObject = Instantiate(prefab);
        gameObject.transform.position = levelLocation.position + Vector3.right * x * wallSize + Vector3.back * y * wallSize;
        gameObject.transform.parent = levelLocation;
    }

    private void PlaceBoundaries()
    {
        //Floor
        GameObject floor = Instantiate(wallPrefab);
        floor.transform.position = levelLocation.position + levelCenter + Vector3.down;
        floor.transform.localScale = Vector3.right * levelWidth * wallSize + Vector3.up + Vector3.forward * levelHeight * wallSize;
        floor.name = "Floor";
        floor.transform.parent = levelLocation;

        //upper and down wall
        for (int x = -1; x <= levelWidth; x++)
        {
            PlaceAt(wallPrefab, x, levelHeight);
            PlaceAt(wallPrefab, x, -1);
        }
    }
}
