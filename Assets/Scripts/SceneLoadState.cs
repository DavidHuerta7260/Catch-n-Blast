using UnityEngine;

public static class SceneLoadState
{
    public static bool equipPitchforkOnLoad = false;
    public static bool enableFishSpawnerOnLoad = false;

    // NEW: stores how many fish were caught in the UnderWater scene
    public static int fishCaughtLastRun = 0;
}


