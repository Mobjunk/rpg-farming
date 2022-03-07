using UnityEngine;

[CreateAssetMenu(fileName = "Character Hair", menuName = "Character/New Character Hair")]
public class Hair : BodyPart
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