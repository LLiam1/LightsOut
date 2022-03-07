using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PathTile
{
    public RoomModule Tile;
    public PathTile CameFrom;
    public int FromStart;
    public int FromEnd;

    public int Value;

    public PathTile(RoomModule t, PathTile cf, RoomModule dest)
    {
        Tile = t;
        CameFrom = cf;
        FromStart = CameFrom != null ? CameFrom.FromStart + 1 : 0;
        FromEnd = Mathf.Abs(Tile.X - dest.X) + Mathf.Abs(Tile.Y - dest.Y);
    }

    public int FindValue()
    {
        return FromStart + FromEnd;
    }
}
