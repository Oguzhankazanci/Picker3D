using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Data/LevelData")]
[System.Serializable]

public class LevelData : ScriptableObject
{
    public List<Ball> Balls;
    public List<Helicopter> Helicopters;
    public List<BoostObj> BoostObjects;
    public List<PickerNeeds> Pickers;
    [System.Serializable]
    public struct Ball
    {
        public Vector3 ballPose;
        public Vector3 ballEuler;
    }
    [System.Serializable]
    public struct Helicopter
    {
        public Vector3 helicopterPose;
        public Vector3 helicopterEuler;
    }
    [System.Serializable]
    public struct BoostObj
    {
        public Vector3 boostObjPose;
        public Vector3 boostObjEuler;
    }
    [System.Serializable]
    public struct PickerNeeds
    {
        public int pickersCount;
    }
}
