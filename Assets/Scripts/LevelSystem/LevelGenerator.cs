using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private Level currentLevel;
    [SerializeField] private GameObject[] avaliableObjects;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private GameObject platform;
    [SerializeField] private GameObject platformParent;

    [SerializeField] private List<Level> allLevel;
    public GameObject[] AvaliableObjects => avaliableObjects;
    public LayerMask GroundLayer => groundLayer;

    private Transform mapHolder;
    private Dictionary<string, GameObject> objToNameMap = new Dictionary<string, GameObject>();

    private int currentLevelIndex;
    private int spawnedCount = 0;
    private int onPlayingConstSpawned =0;
    private void Awake()
    {
        GetAllLevel();
    }
    private void Start()
    {

        if (currentLevel)
            GenerateLevel(currentLevel);
    }

    public void GenerateLevel(Level level)
    {
        currentLevel = level;
        if (currentLevel == null)
            return;

        CreateObjMap();
        CreateParentGameObject();
        SpawnObjects();
    }
    public void GenerateLevelIngame(Level level)
    {
        currentLevel = level;

        CreateObjMap();
        SpawnPlatform();
        SpawnObjects();
    }
    private void CreateObjMap()
    {
        objToNameMap = new Dictionary<string, GameObject>();
        foreach (var obj in avaliableObjects)
        {
            objToNameMap.Add(obj.name, obj);
        }
    }
    private void GetAllLevel()
    {
        allLevel = new List<Level>();
        for (int i = 0; i < Resources.LoadAll("Levels/").Length ; i++)
        {
            allLevel.Add(Resources.Load<Level>("Levels/Level " + (i+1)));
        }

    }
    private Transform CreateParentGameObject()
    {
        string holderName = "Generated Map";
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).name == holderName)
                DestroyImmediate(transform.GetChild(i).gameObject);
        }

        mapHolder = new GameObject(holderName).transform;
        mapHolder.parent = transform;
        return mapHolder;
    }
    private void SpawnPlatform()
    {

        GameObject currentPlatform = Instantiate(platform, new Vector3(0, 0, onPlayingConstSpawned * 100), platform.transform.rotation, platformParent.transform);


        mapHolder = currentPlatform.transform.GetChild(2);
        for (int i = 0; i < 3; i++)
        {
            currentPlatform.transform.GetChild(0).GetChild(i).GetChild(1).GetComponent<CollectableArea>().SetNeededCount(currentLevel.pickerValues[i]);
        }
        onPlayingConstSpawned++;
        spawnedCount++;
    }


    private void SpawnObjects()
    {
        for (int i = 0; i < currentLevel.levelObjects.Count; i++)
        {
            CreateObject(currentLevel.levelObjects[i]);
        }
    }

    public GameObject CreateNewObject(Level.LevelObjectData data)
    {
        currentLevel.levelObjects.Add(data);
        return CreateObject(data);
    }

    private GameObject CreateObject(Level.LevelObjectData data)
    {
        var prefab = GetPrefab(data.prefebName);
        if (prefab == null)
            return null;
        var instance = Instantiate(GetPrefab(data.prefebName), mapHolder);
        instance.transform.localPosition = data.position;
        instance.transform.localRotation = data.rotation;
        instance.transform.localScale = data.scale;

        data.instantiatedObject = instance;
        return instance;
    }

    private GameObject GetPrefab(string prefebName)
    {
        if (objToNameMap.TryGetValue(prefebName, out GameObject prefab))
            return prefab;
        Debug.LogError($"Prefab {prefebName} is not avaliable. Check the level generator inspector");
        return null;
    }

    public void GenerateLevelOnPlaying(int count)
    {
        currentLevelIndex = SaveLoadManager.GetFakeLevel();
        for (int i = 0; i < count; i++)
        {
            int currentSpawnLevelIndex = ((currentLevelIndex-1) + spawnedCount) % allLevel.Count;
            GenerateLevelIngame(allLevel[currentSpawnLevelIndex]); 
        }

    }
    public void DecraseSpawnedCount()
    {
        spawnedCount--;    
    }
}