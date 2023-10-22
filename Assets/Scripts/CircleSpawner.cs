using UnityEngine;
public class CircleSpawner : MonoBehaviour
{
    public GameObject circlePrefab;
    private Vector2 worldRanges;

    void Start()
    {
        worldRanges = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width - 50, Screen.height - 50));
        int number = Random.Range(5, 10);

        for (int i = 0; i < number; i++)
        {
            SpawnCircle();
        }
    }

    void SpawnCircle()
    {
        Instantiate(circlePrefab, GetRandomPosition(), Quaternion.identity);
    }

    Vector2 GetRandomPosition()
    {
        return new Vector2(Random.Range(-worldRanges.x, worldRanges.x), Random.Range(-worldRanges.y, worldRanges.y));
    }
}
