using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class DungeonMapLoader : MonoBehaviour
{
    public string serverURL = "http://localhost:8000/get_map";

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
                DungeonMapData data = JsonUtility.FromJson<DungeonMapData>(response);

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

    void GenerateDungeon(DungeonMapData data)
    {
        for (int i = 0; i < data.data.Length; i++)
        {
            for (int j = 0; j < data.data[i].Length; j++)
            {
                int tileType = data.data[i][j];
                if (backgroundPrefab != null && tileType == 0)
                {
                    Debug.Log("Processing tileType: " + tileType);

                    Instantiate(backgroundPrefab, new Vector3(i, 0, j), Quaternion.identity);
                }
                else if (wallPrefab != null && tileType == 1)
                {
                    Debug.Log("Processing tileType: " + tileType);

                    Instantiate(wallPrefab, new Vector3(i, 0, j), Quaternion.identity);
                }
                else if (benchPrefab != null && tileType == 2)
                {
                    Debug.Log("Processing tileType: " + tileType);

                    Instantiate(benchPrefab, new Vector3(i, 0, j), Quaternion.identity);
                }
                else if (tablePrefab != null && tileType == 3)
                {
                    Debug.Log("Processing tileType: " + tileType);

                    Instantiate(tablePrefab, new Vector3(i, 0, j), Quaternion.identity);
                }
                else if (collectablePrefab != null && tileType == 0)
                {
                    Debug.Log("Processing tileType: " + tileType);

                    Instantiate(collectablePrefab, new Vector3(i, 0, j), Quaternion.identity);
                }
                else if (bookPrefab != null && tileType == 0)
                {
                    Debug.Log("Processing tileType: " + tileType);

                    Instantiate(bookPrefab, new Vector3(i, 0, j), Quaternion.identity);
                }
                else if (torchPrefab != null && tileType == 0)
                {
                    Debug.Log("Processing tileType: " + tileType);

                    Instantiate(torchPrefab, new Vector3(i, 0, j), Quaternion.identity);
                }
                else if (doorPrefab != null && tileType == 0)
                {
                    Debug.Log("Processing tileType: " + tileType);

                    Instantiate(doorPrefab, new Vector3(i, 0, j), Quaternion.identity);
                }
                else if (cratePrefab != null && tileType == 0)
                {
                    Debug.Log("Processing tileType: " + tileType);

                    Instantiate(cratePrefab, new Vector3(i, 0, j), Quaternion.identity);
                }
            }
        }
    }
}

[System.Serializable]
public class DungeonMapData
{
    public int[][] data;
}

