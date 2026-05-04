public class CheckpointSystem
{
    public static bool RestAreaUsed { get; private set; }
    public static int RestAreaScene { get; private set; }

    public static void SetCheckpoint(int sceneIndex)
    {
        RestAreaUsed = true;
        RestAreaScene = sceneIndex;
    }

    public static void Reset()
    {
        RestAreaUsed = false;
        RestAreaScene = -1;
    }
}