using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Text;

public class MapManager : MonoBehaviour {

	public float[,] HeightsMap;

	Terrain myTerrain;
	TerrainData myTerrainData;
		
	// Use this for initialization
	void Start () {
		if ( !myTerrain )
		{
			myTerrain = Terrain.activeTerrain; // find the active terrain
		}		
		myTerrainData = myTerrain.terrainData; // store the terrainData
		GetTerrainData ();				

	}

	void GetTerrainData() {
		HeightsMap = myTerrainData.GetHeights (0, 0, myTerrainData.heightmapWidth, myTerrainData.heightmapHeight);

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
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
