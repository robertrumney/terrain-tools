using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class TreeMeshToTerrain : EditorWindow
{
    // Serializable class to hold a list of tree mesh GameObjects
    [System.Serializable]
    public class TreeMeshList
    {
        public List<GameObject> items = new List<GameObject>();
    }

    // Instance of the TreeMeshList to store the trees
    public TreeMeshList treeMeshList = new TreeMeshList();

    private SerializedObject serializedObject;
    private SerializedProperty treeMeshesProperty;

    // MenuItem to create and show the editor window
    [MenuItem("Tools/Tree Mesh to Terrain")]
    public static void ShowWindow()
    {
        var window = GetWindow<TreeMeshToTerrain>("Tree Mesh to Terrain");
        window.serializedObject = new SerializedObject(window);
        window.treeMeshesProperty = window.serializedObject.FindProperty("treeMeshList.items");
    }

    // Function to render the GUI for the editor window
    void OnGUI()
    {
        serializedObject.Update();

        GUILayout.Label("Drag Tree Meshes Here:", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(treeMeshesProperty, true);

        // Button to start the tree painting process
        if (GUILayout.Button("Paint Trees on Terrain"))
        {
            PaintTreesOnTerrain();
        }

        serializedObject.ApplyModifiedProperties();
    }

    // Function to iterate over tree meshes and paint them on the terrain
    void PaintTreesOnTerrain()
    {
        Debug.Log("Doing method");

        foreach (GameObject treeMesh in treeMeshList.items)
        {
            if (treeMesh != null)
            {
                Debug.Log("doing item");

                // Deactivate the tree mesh GameObject
                treeMesh.SetActive(false);

                // Raycast to find the terrain below the tree mesh
                if (Physics.Raycast(treeMesh.transform.position + Vector3.up * 100, Vector3.down, out RaycastHit hit, Mathf.Infinity))
                {
                    Debug.Log("raycast hit something:" + hit.collider.name);

                    // Check if the hit object is a Terrain
                    Terrain hitTerrain = hit.collider.GetComponent<Terrain>();
                    if (hitTerrain != null)
                    {
                        // Paint the tree on the terrain
                        PaintTreeOnTerrain(treeMesh, hitTerrain);
                    }
                }
            }
        }
    }

    // Function to paint a single tree on the terrain
    void PaintTreeOnTerrain(GameObject treeMesh, Terrain terrain)
    {
        TerrainData terrainData = terrain.terrainData;
        Vector3 terrainPos = GetTerrainPosition(treeMesh.transform.position, terrain);

        // Find the matching tree prototype for the tree mesh
        TreePrototype matchedPrototype = FindMatchingTreePrototype(treeMesh, terrainData);

        if (matchedPrototype != null)
        {
            int prototypeIndex = FindPrototypeIndex(matchedPrototype, terrainData);

            if (prototypeIndex != -1)
            {
                // Create and add a tree instance to the terrain
                TreeInstance treeInstance = new TreeInstance
                {
                    position = terrainPos,
                    prototypeIndex = prototypeIndex,
                    widthScale = 1,
                    heightScale = 1,
                    color = Color.white,
                    lightmapColor = Color.white
                };

                terrain.AddTreeInstance(treeInstance);
            }
        }
    }

    // Function to convert world position to terrain-relative position
    Vector3 GetTerrainPosition(Vector3 worldPosition, Terrain terrain)
    {
        return new Vector3(
            (worldPosition.x - terrain.transform.position.x) / terrain.terrainData.size.x,
            0,
            (worldPosition.z - terrain.transform.position.z) / terrain.terrainData.size.z);
    }

    // Function to find a matching tree prototype in the terrain data
    TreePrototype FindMatchingTreePrototype(GameObject treeMesh, TerrainData terrainData)
    {
        // Use Regex to remove all text within parentheses from the tree mesh name
        string pattern = @" \(.*?\)";
        string treeMeshName = Regex.Replace(treeMesh.name, pattern, "").Trim();

        foreach (var prototype in terrainData.treePrototypes)
        {
            // Compare the cleaned-up tree mesh name with prototype prefab names
            if (prototype.prefab.name == treeMeshName)
            {
                return prototype;
            }
        }

        Debug.LogWarning("No matching tree prototype found for: " + treeMeshName);
        return null;
    }

    // Function to find the index of a given tree prototype in the terrain data
    int FindPrototypeIndex(TreePrototype prototype, TerrainData terrainData)
    {
        for (int i = 0; i < terrainData.treePrototypes.Length; i++)
        {
            if (terrainData.treePrototypes[i].prefab == prototype.prefab)
            {
                return i;
            }
        }

        return -1;
    }
}
