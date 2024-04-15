using System;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using MiniJSON;

public class DungeonMapLoader : MonoBehaviour
{
    public string serverURL;

    public GameObject backgroundPrefab;
    public GameObject wallPrefab;
    public GameObject benchPrefab;
    public GameObject bookPrefab;
    public GameObject torchPrefab;
    public GameObject collectablePrefab;
    public GameObject cratePrefab;
    public GameObject tablePrefab;
    public GameObject doorPrefab;

    IEnumerator Start()
    {
        Debug.Log("Sending request to: " + serverURL);

        using (UnityWebRequest www = UnityWebRequest.Get(serverURL))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Server request failed: " + www.error);
            }
            else
            {
                string response = www.downloadHandler.text;
                Debug.Log("Server response received: " + response);

                // Parse response JSON and generate dungeon
                DungeonMapData data = ParseDungeonMapData(response);

                // Check if data is valid
                if (data == null || data.data == null)
                {
                    Debug.LogError("Failed to deserialize DungeonMapData from JSON");
                    yield break; // Exit early if data is null
                }

                // Log data length for debugging
                Debug.Log("Data length: " + data.data.Length);

                // Create dungeon based on data
                GenerateDungeon(data);
            }
        }
    }

    DungeonMapData ParseDungeonMapData(string jsonResponse)
    {
        DungeonMapData dungeonMapData = new DungeonMapData();
        try
        {
            // Parse the JSON response into a dictionary
            Dictionary<string, object> jsonDict = Json.Deserialize(jsonResponse) as Dictionary<string, object>;
            if (jsonDict != null && jsonDict.ContainsKey("data"))
            {
                // Extract the 'data' array from the dictionary
                List<object> dataList = jsonDict["data"] as List<object>;
                if (dataList != null)
                {
                    // Convert the 'data' array into the desired format (int[][])
                    dungeonMapData.data = new int[dataList.Count][];
                    for (int i = 0; i < dataList.Count; i++)
                    {
                        List<object> innerList = dataList[i] as List<object>;
                        if (innerList != null)
                        {
                            dungeonMapData.data[i] = new int[innerList.Count];
                            for (int j = 0; j < innerList.Count; j++)
                            {
                                dungeonMapData.data[i][j] = Convert.ToInt32(innerList[j]);
                            }
                        }
                    }
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Failed to parse DungeonMapData: " + e.Message);
        }
        return dungeonMapData;
    }

    void GenerateDungeon(DungeonMapData data)
    {
        float tileSize = 1.0f; // Adjust this value based on the size of your tiles

        for (int i = 0; i < data.data.Length; i++)
        {
            for (int j = 0; j < data.data[i].Length; j++)
            {
                int tileType = data.data[i][j];

                GameObject prefabToInstantiate = GetPrefabForTileType(tileType);

                if (prefabToInstantiate != null)
                {
                    // Calculate the position for the tile based on row (i) and column (j)
                    Vector3 tilePosition = new Vector3(j * tileSize, -i * tileSize, 0f);

                    // Instantiate the prefab at the calculated position with no rotation

                    Instantiate(prefabToInstantiate, tilePosition, Quaternion.identity);
                    Debug.Log($"Instantiated {prefabToInstantiate.name} at {tilePosition}");
                }
                else
                {
                    Debug.LogWarning($"Prefab not assigned for tileType: {tileType}");
                }
            }
        }
    }

    GameObject GetPrefabForTileType(int tileType)
    {
        switch (tileType)
        {
            case 0:
                return backgroundPrefab;
            case 1:
                return wallPrefab;
            case 2:
                return benchPrefab;
            case 3:
                return tablePrefab;
            case 4:
                return collectablePrefab;
            case 5:
                return bookPrefab;
            case 6:
                return torchPrefab;
            case 7:
                return doorPrefab;
            case 8:
                return cratePrefab;
            default:
                Debug.LogWarning($"Unknown tileType: {tileType}");
                return null;
        }
    }
}

[System.Serializable]
public class DungeonMapData
{
    public int[][] data;
}
