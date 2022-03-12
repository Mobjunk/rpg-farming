using System;
using UnityEngine;

[Serializable]
public abstract class BodyPart : ScriptableObject
{
    [Header("Type")] public BodyType bodyType;

    [Header("Testing loading sprites...")]
    public string[] walkPathName;
    public string[] axePathName;
    public string[] pickaxePathName;
    public string[] wateringCanPathName;
    public string[] hoePathName;
    public string[] carryPathName;
    public string[] fishingPathName;
    public string[] swordPathName;

    public string GetFileName(string pAction, int pIndex)
    {
        if (!bodyType.Equals(BodyType.BODY) && pIndex > 1) pIndex = 0;
        
        string path = GetPath(pAction)[pIndex];
        int pos = path.LastIndexOf("/") + 1;
        return path.Substring(pos, path.Length - pos) + "_"; //.Replace(".png", "_")
    }

    private string[] GetPath(string pAction)
    {
        switch (pAction)
        {
            case "AXE_SWING":
                return axePathName;
            case "PICKAXE_SWING":
                return pickaxePathName;
            case "WATERING":
                return wateringCanPathName;
            case "HOE_SWING":
                return hoePathName;
            case "CARRY":
                return carryPathName;
            case "FISHING":
            case "FISHING_IDLE":
                return fishingPathName;
            case "SWORD_SWING":
                return swordPathName;
            default:
                return walkPathName;
        }
    }

    public int GetSpriteIndex(string pAction, int pRotation)
    {
        switch (bodyType)
        {
            //bottom, top, right, left
            case BodyType.BEARD:
            case BodyType.EYES:
            case BodyType.HAIR:
                switch (pRotation)
                {
                    case 0: //Down
                        switch (pAction)
                        {
                            default: return 0;
                        }
                    case 1: //Left
                        switch (pAction)
                        {
                            case "AXE_SWING":
                            case "PICKAXE_SWING":
                            case "HOE_SWING":
                            case "FISHING":
                                return 210;
                            case "WATERING":
                                return 84;
                            case "SWORD_SWING":
                                return 168;
                            default: return 336;
                        }
                    case 2: //Right
                        switch (pAction)
                        {
                            case "AXE_SWING":
                            case "PICKAXE_SWING":
                            case "HOE_SWING":
                            case "FISHING":
                                return 140;
                            case "WATERING":
                                return 56;
                            case "SWORD_SWING":
                                return 112;
                            default: return 224;
                        }
                    case 3: //Top
                        switch (pAction)
                        {
                            case "AXE_SWING":
                            case "PICKAXE_SWING":
                            case "HOE_SWING":
                            case "FISHING":
                                return 70;
                            case "WATERING":
                                return 28;
                            case "SWORD_SWING":
                                return 56;
                            default: return 112;
                        }
                }
                break;
            case BodyType.BODY:
            case BodyType.HAT:
                switch (pRotation)
                {
                    case 0: //Down
                        switch (pAction)
                        {
                            default: return 0;
                        }
                    case 1: //Left
                        switch (pAction)
                        {
                            case "AXE_SWING":
                            case "PICKAXE_SWING":
                            case "HOE_SWING":
                            case "FISHING":
                                return 15;
                            case "WATERING":
                                return 6;
                            case "SWORD_SWING":
                                return 12;
                            default: return 24;
                        }
                    case 2: //Right
                        switch (pAction)
                        {
                            case "AXE_SWING":
                            case "PICKAXE_SWING":
                            case "HOE_SWING":
                            case "FISHING":
                                return 10;
                            case "WATERING":
                                return 4;
                            case "SWORD_SWING":
                                return 8;
                            default: return 16;
                        }
                    case 3: //Top
                        switch (pAction)
                        {
                            case "AXE_SWING":
                            case "PICKAXE_SWING":
                            case "HOE_SWING":
                            case "FISHING":
                                return 5;
                            case "WATERING":
                                return 2;
                            case "SWORD_SWING":
                                return 4;
                            default: return 8;
                        }
                }
                break;
            case BodyType.CHEST:
            case BodyType.LEGS:
            case BodyType.FEET:
                switch (pRotation)
                {
                    case 0: //Down
                        switch (pAction)
                        {
                            default: return 0;
                        }
                    case 1: //Left
                        switch (pAction)
                        {
                            case "AXE_SWING":
                            case "PICKAXE_SWING":
                            case "HOE_SWING":
                            case "FISHING":
                                return 150;
                            case "WATERING":
                                return 60;
                            case "SWORD_SWING":
                                return 120;
                            default: return 240;
                        }
                    case 2: //Right
                        switch (pAction)
                        {
                            case "AXE_SWING":
                            case "PICKAXE_SWING":
                            case "HOE_SWING":
                            case "FISHING":
                                return 100;
                            case "WATERING":
                                return 40;
                            case "SWORD_SWING":
                                return 80;
                            default: return 160;
                        }
                    case 3: //Top
                        switch (pAction)
                        {
                            case "AXE_SWING":
                            case "PICKAXE_SWING":
                            case "HOE_SWING":
                            case "FISHING":
                                return 50;
                            case "WATERING":
                                return 20;
                            case "SWORD_SWING":
                                return 40;
                            default: return 80;
                        }
                }
                break;
        }
        return -1;
    }

    public abstract bool RequiresMultiplier();

    public abstract bool UseHairColor();
}
