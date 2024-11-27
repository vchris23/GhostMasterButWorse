using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

public class Targeter
{
    public static object FindTarget(Target type, Room room) 
    {
        switch (type)
        {
            case Target.Human:
                if (room.humansInRoom.Count > 0)
                {
                    int randomHuman = Random.Range(0, room.humansInRoom.Count);
                    return room.humansInRoom[randomHuman];
                }
                return null;
            case Target.Fetter:
                return new Human(); //Not implemented
            case Target.RoomHumans:
                return room.humansInRoom.ToArray();
            case Target.RoomEntities:
                return room.entities.ToArray();
            case Target.RoomClutter:
                return room.clutter.ToArray();
            case Target.RandomRoomClutter:
                if (room.clutter.Count >= 0)
                {
                    int randomClutter = Random.Range(0, room.clutter.Count);
                    return room.clutter[randomClutter];
                }
                return null;
            case Target.Objects:
                return new Human();
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
        
    }
} 
