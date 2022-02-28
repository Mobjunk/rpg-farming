using UnityEngine;

[CreateAssetMenu(fileName = "Character Eyes", menuName = "Character/New Character Eyes")]
public class Eyes : BodyPart
{
    public int eyesId;
    
    public override bool RequiresMultiplier()
    {
        return true;
    }
    public override bool UseHairColor()
    {
        return false;
    }
}