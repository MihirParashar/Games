using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

	public Texture2D map;

	public ColorToPrefab[] colorMappings;

	void Awake()
	{
		//If we haven't already spawned objects
		if (GameObject.FindGameObjectWithTag("Player") == null)
		{
			GenerateLevel();
		}
	}


	public void GenerateLevel()
	{
		for (int x = 0; x < map.width; x++)
		{
			for (int y = 0; y < map.height; y++)
			{
				GenerateTile(x, y);
			}
		}
	}

	void GenerateTile(int x, int y)
	{
		Color pixelColor = map.GetPixel(x, y);

		if (pixelColor.a == 0)
		{
			// The pixel is transparrent, so we can skip it.
			return;
		}

		foreach (ColorToPrefab colorMapping in colorMappings)
		{

			
			if (colorMapping.color.Equals(pixelColor))
			{
				Vector2 position = new Vector2(x, y - 5);
				Instantiate(colorMapping.prefab, position, Quaternion.identity, transform);
			}
		}
	}

}
