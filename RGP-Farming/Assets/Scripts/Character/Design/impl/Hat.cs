using UnityEngine;

[CreateAssetMenu(fileName = "Character Hat", menuName = "Character/New Character Hat")]
public class Hat : BodyPart
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