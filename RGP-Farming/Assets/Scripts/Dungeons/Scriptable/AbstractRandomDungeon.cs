using UnityEngine;

[CreateAssetMenu(fileName = "SimpleRandomWalkParameters_", menuName = "Dungeons/SimpleRandomWalk")]
public class AbstractRandomDungeon : ScriptableObject
{
    public int Iterations = 10;
    public int WalkLength = 10;
    public bool StartRandomlyEachIteration = true;
}
