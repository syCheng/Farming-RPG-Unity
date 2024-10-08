using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FarmingRPG
{

    public enum AnimationName
    {
        idleDown,
        idleUp,
        idleRight,
        idleLeft,

        walkUp,
        walkDown,
        walkRight,
        walkLeft,

        runUp,
        runDown,
        runRight,
        runLeft,

        useToolUp,
        useToolDown,
        useToolRight,
        useToolLeft,

        swingToolUp,
        swingToolDown,
        swingToolRight,
        swingToolLeft,

        liftToolUp,
        liftToolDown,
        liftToolRight,
        liftToolLeft,

        holdToolUp,
        holdToolDown,
        holdToolRight,
        holdToolLeft,

        pickDown,
        pickUp,
        pickRight,
        pickLeft,
        count
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

    public enum PartVariantColour
    {
        none,
        count
    }


    public enum PartVariantType
    {
        none,
        carry,
        hoe,
        pickAxe,
        axe,
        scythe,
        wateringCan,
        count
    }


    //Inventory location
    public enum InventoryLocation
    {
        player,
        chest,
        count
    }


    public enum ToolEffect
    {
        none,
        watering,
    }


    public enum Direction
    {
        up,
        down,
        left,
        right,
        none,
    }


    public enum ItemType
    {
        Seed,
        Commodity,
        Watering_tool,
        Hoeing_tool,
        Chopping_tool,
        Breaking_tool,
        Reaping_tool,
        Collecting_tool,
        Reapable_scenary,
        Furniture,
        none,
        count
    }



}
