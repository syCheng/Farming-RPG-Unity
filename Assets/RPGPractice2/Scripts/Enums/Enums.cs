using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FarmingRPG2
{

    public enum InventoryLocation
    {
        player,
        chest,
        count
    }

    public enum ToolEffect
    {
        none,
        watering
    }


    public enum CharacterPartAnimator
    {
        body,
        arms,
        hair,
        tool,
        hat,
        count
    }

    public enum Direction
    {
        up,
        down,
        left,
        right,
        none,
    }
}
