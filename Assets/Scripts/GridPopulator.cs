using UnityEngine;

public class GridPopulator : MonoBehaviour
{
    public GameObject[] imagePrefabs;  // Array of image prefabs
    public Transform container;

    public int gridWidth = 3;
    public int gridHeight = 3;
    public float cellSize = 1f;
    public float spacing = 0.2f;

    private void Start()
    {
        ShuffleImagePrefabs();
        PopulateGrid();
    }

    private void ShuffleImagePrefabs()
    {
        int count = imagePrefabs.Length;
        for (int i = 0; i < count - 1; i++)
        {
            int randomIndex = Random.Range(i, count);
            GameObject temp = imagePrefabs[randomIndex];
            imagePrefabs[randomIndex] = imagePrefabs[i];
            imagePrefabs[i] = temp;
        }
    }

    private void PopulateGrid()
    {
        Vector2 gridSize = new Vector2(gridWidth, gridHeight);
        float totalWidth = (gridWidth * cellSize) + ((gridWidth - 1) * spacing);
        float totalHeight = (gridHeight * cellSize) + ((gridHeight - 1) * spacing);
        Vector2 startPos = new Vector2(-totalWidth / 2f, totalHeight / 2f);

        int index = 0;
        for (int y = 0; y < gridHeight; y++)
        {
            for (int x = 0; x < gridWidth; x++)
            {
                Vector2 cellPosition = startPos + new Vector2(x * (cellSize + spacing), -y * (cellSize + spacing));
                Vector2 scale = new Vector2(cellSize, cellSize);

                GameObject imagePrefab = imagePrefabs[index % imagePrefabs.Length];
                GameObject imageObj = Instantiate(imagePrefab, container);
                imageObj.transform.localPosition = cellPosition;
                imageObj.transform.localScale = scale;

                index++;
            }
        }
    }
}




