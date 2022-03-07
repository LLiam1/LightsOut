using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.Tilemaps;

public static class Pathfinder
{
   public static List<RoomModule> Pathfind(RoomModule start, RoomModule end){

       int safety = 999;

       List<PathTile> open = new List<PathTile>()
       {
           new PathTile(start,null,end)
       };

       Dictionary<RoomModule,PathTile> closed = new Dictionary<RoomModule, PathTile>();
       PathTile current = null;

       while (open.Count > 0 && safety > 0)
       {
           safety--;
           int best = 999;
           PathTile bTile = null;
           foreach (PathTile t in open)
               if (t.Value < best)
               {
                   best = t.Value;
                   bTile = t;
               }

           open.Remove(bTile);

           if (bTile.Tile == end)
           {
               current = bTile;
               break;
           }

           //Just after you find your bTile
           foreach (RoomModule nei in bTile.Tile.theseNeighbors)
           {
               if (nei.isLightOn == false)
               {
                   if (!closed.ContainsKey(nei))
                   {
                       PathTile pt = new PathTile(nei, bTile, end);
                       open.Add(pt);
                       closed.Add(nei, pt);
                       continue;
                   }

                   if (open.Contains(closed[nei]) && closed[nei].FromStart > bTile.FromStart + 1)
                   {
                       closed[nei].FromStart = bTile.FromStart + 1;
                       closed[nei].CameFrom = bTile;
                       closed[nei].Value = closed[nei].FindValue();
                   }
               }
           }
       }
       List<RoomModule> path = new List<RoomModule>();
       while (current != null && current.Tile != start){
           //We add it to the start because we're tracing the path backwards
           path.Insert(0,current.Tile);
           current = current.CameFrom;
       }
       return path;
   }
}

