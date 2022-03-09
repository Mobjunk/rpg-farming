using UnityEngine;

[CreateAssetMenu(fileName = "Character Chest", menuName = "Character/New Character Chest")]
public class Chest : BodyPart
{
    public int chestId;
    public override bool RequiresMultiplier()
    {
        return true;
    }
    public override bool UseHairColor()
    {
        return false;
    }
}