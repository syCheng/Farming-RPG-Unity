using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace FarmingRPG
{

    public delegate void MovementDelegate(float inputX,float inputY,bool isWalking,bool isRunning,bool isIdle,
        bool isCarrying,ToolEffect toolEffect,
        bool isUsingToolRight,bool isUsingToolLeft,bool isUsingToolUp,bool isUsingToolDown,
        bool isLiftingToolRight,bool isLiftingToolLeft,bool isLiftingToolUp,bool isLiftingToolDown,
        bool isPickingRight,bool isPickingLeft,bool isPickingUp,bool isPickingDown,
        bool isSwingToolRight,bool isSwingToolLeft,bool isSwingToolUp,bool isSwingToolDown,
        bool idleUp,bool idleDown,bool idleLeft,bool idleRight);


    public static class EventHandler
    {

        //Inventory Update event.
        public static event Action<InventoryLocation, List<InventoryItem>> InventoryUpdateEvent;

        //inventory update event call for publisher
        public static void CallInventoryUpdatedEvent(InventoryLocation inventoryLocation,List<InventoryItem> inventoryList)
        {
            if (InventoryUpdateEvent != null)
            {
                InventoryUpdateEvent.Invoke(inventoryLocation, inventoryList);
            }
        }








        //Movement Event
        public static event MovementDelegate MovementEvent;

        //Movement Event call for publishers
        public static void CallMovementEvent(float inputX, float inputY, bool isWalking, bool isRunning, bool isIdle,
        bool isCarrying, ToolEffect toolEffect,
        bool isUsingToolRight, bool isUsingToolLeft, bool isUsingToolUp, bool isUsingToolDown,
        bool isLiftingToolRight, bool isLiftingToolLeft, bool isLiftingToolUp, bool isLiftingToolDown,
        bool isPickingRight, bool isPickingLeft, bool isPickingUp, bool isPickingDown,
        bool isSwingToolRight, bool isSwingToolLeft, bool isSwingToolUp, bool isSwingToolDown,
        bool idleUp, bool idleDown, bool idleLeft, bool idleRight)
        {
            if (MovementEvent != null)
            {
                MovementEvent.Invoke(inputX, inputY,
                    isWalking, isRunning, isIdle, isCarrying,
                    toolEffect,
                    isUsingToolRight, isUsingToolLeft, isUsingToolUp, isUsingToolDown,
                    isLiftingToolRight, isLiftingToolLeft, isLiftingToolUp, isLiftingToolDown,
                    isPickingRight, isPickingLeft, isPickingUp, isPickingDown,
                    isSwingToolRight, isSwingToolLeft, isSwingToolUp, isSwingToolDown,
                    idleUp, idleDown, idleLeft, idleRight);
            }
        }

    }
}
