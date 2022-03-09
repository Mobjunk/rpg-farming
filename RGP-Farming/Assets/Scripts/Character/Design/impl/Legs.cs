using UnityEngine;

[CreateAssetMenu(fileName = "Character Legs", menuName = "Character/New Character Legs")]
public class Legs : BodyPart
{
    public int legsId;
    public override bool RequiresMultiplier()
    {
        return true;
    }
    public override bool UseHairColor()
    {
        return false;
    }
}