using UnityEngine;

[CreateAssetMenu(fileName = "Character Beard", menuName = "Character/New Character Beard")]
public class Beard : BodyPart
{
    public override bool RequiresMultiplier()
    {
        return true;
    }
    public override bool UseHairColor()
    {
        return true;
    }
}