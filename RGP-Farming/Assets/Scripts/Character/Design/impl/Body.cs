using UnityEngine;

[CreateAssetMenu(fileName = "Character Body", menuName = "Character/New Character Body")]
public class Body : BodyPart
{
    public override bool RequiresMultiplier()
    {
        return false;
    }
    public override bool UseHairColor()
    {
        return false;
    }
}