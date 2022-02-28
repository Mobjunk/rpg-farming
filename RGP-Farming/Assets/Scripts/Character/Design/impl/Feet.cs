using UnityEngine;

[CreateAssetMenu(fileName = "Character Feet", menuName = "Character/New Character Feet")]
public class Feet : BodyPart
{
    public int feetId;
    public override bool RequiresMultiplier()
    {
        return true;
    }
    public override bool UseHairColor()
    {
        return false;
    }
}