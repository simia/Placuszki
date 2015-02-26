using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Text;

public class MapManager : MonoBehaviour {

	public float[,] HeightsMap;

	Terrain myTerrain;
	TerrainData myTerrainData;
		
	private static MapManager m_Instance;
	public static MapManager Instance { get { return m_Instance; } }
	
	void Awake()
	{
		m_Instance = this;
	}
	
	void OnDestroy()
	{
		m_Instance = null;
	}

	// Use this for initialization
	void Start () {
		if ( !myTerrain )
		{
			myTerrain = Terrain.activeTerrain; // find the active terrain
		}		
		myTerrainData = myTerrain.terrainData; // store the terrainData
		HeightsMap = myTerrainData.GetHeights(0, 0, myTerrainData.heightmapWidth, myTerrainData.heightmapHeight);
		//printToFile();				

	}

	/*void printToFile() {
		StringBuilder sb = new StringBuilder ();

		//string h = "";
		for (int y = 0; y < myTerrainData.heightmapHeight; y++) 
		{

			for(int x = 0; x < myTerrainData.heightmapWidth; x++)
			{
				sb.Append(HeightsMap[y,x]);
				sb.Append(",");
				//h += Mathf.RoundToInt(Mathf.Clamp(heights[y,x], -1.0f, 1.0f) * 100) + ",";
			}
			sb.AppendLine();
			//print (h + "\n");
		}

		using (StreamWriter sw = new StreamWriter("TestFile.txt")) 
		{
			// Add some text to the file.
			sw.Write(sb.ToString());
		}
		//myTerrainData.heightmapHeight
	}*/
	
	// Update is called once per frame
	void Update () {
	
	}
}
