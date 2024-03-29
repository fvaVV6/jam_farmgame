﻿using System;
using System.Collections.Generic;

public enum TileType { Impassable, Floor, Virtual }

//Basic Tile unit.
//Positional unit that composes the grid-like map. Things don't exactly need to be on Tiles, but the tile grid is useful for certain operations.
public class Tile : IAtom
{
    public readonly WorldData WorldData;

    public TileType Type { get; set; }

    //Atom data
    public int X { get; set; }
    public int Y { get; set; }
    public int Z { get; set; }

    public int QuadStartIndex => (X + Z * WorldData.SizeX) * 4;

    public string Examine
    {
        get
        {
            string retVal;
            switch (Type)
            {
                case TileType.Floor:
                    retVal = "There's some flooring here.";
                    break;
                case TileType.Impassable:
                    retVal = "This is open space!";
                    break;
                case TileType.Virtual:
                    retVal = "You ain't supposed to look at this!";
                    break;
                default:
                    throw new IndexOutOfRangeException($"Unhandled tile type {Type}");
            }

            retVal += $" TileInfo = [X={X}, Z={Z}] [ContentsCount = {Contents.Count}]";
            return retVal;
        }
    }
    public List<object> Contents { get; }

    //Constructor for tile-check operations...
    public Tile(int x, int y, int z)
    {
        this.X = x;
        this.Y = y;
        this.Z = z;
    }

    public Tile(WorldData world, int x, int y, int z) : this(x, y, z)
    {
        this.WorldData = world;

        this.Contents = new List<object>
        {
            this
        };
    }

    /// <summary>
    /// Returns the cardinal neighbors of a given Tile.
    /// </summary>
    /// <param name="tile">Array of returned tiles. Some of them can be null, if we're on a border!</param>
    /// <returns></returns>
    public Tile[] GetCardinalNeighbors(Tile tile)
    {
        Tile[] neighbors = new Tile[4];

        //N
        neighbors[0] = WorldData.WorldTiles[X, Z+1];
        //E
        neighbors[1] = WorldData.WorldTiles[X+1, Z];
        //S
        neighbors[2] = WorldData.WorldTiles[X, Z-1];
        //W
        neighbors[3] = WorldData.WorldTiles[X-1, Z];

        return neighbors;
    }

    /// <summary>
    /// Returns all neighbors of a given Tile.
    /// </summary>
    /// <param name="tile">Array of returned tiles. Some of them can be null, if we're on a border!</param>
    /// <returns></returns>
    public Tile[] GetAllNeighbors(Tile tile)
    {
        Tile[] neighbors = new Tile[8];

        //N
        neighbors[0] = WorldData.WorldTiles[X, Z + 1];
        //E
        neighbors[1] = WorldData.WorldTiles[X + 1, Z];
        //S
        neighbors[2] = WorldData.WorldTiles[X, Z - 1];
        //W
        neighbors[3] = WorldData.WorldTiles[X - 1, Z];
        //NE
        neighbors[4] = WorldData.WorldTiles[X + 1, Z + 1];
        //NW
        neighbors[5] = WorldData.WorldTiles[X - 1, Z + 1];
        //SE
        neighbors[6] = WorldData.WorldTiles[X + 1, Z - 1];
        //SW
        neighbors[7] = WorldData.WorldTiles[X - 1, Z - 1];

        return neighbors;
    }



}
