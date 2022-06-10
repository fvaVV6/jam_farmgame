﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileType { Void, Floor, Virtual }

//Basic Tile unit.
//They can be of different types, and they're used to manage the grid-like map.
public class Tile : IAtom
{
    public WorldData World;

    private TileType _type = TileType.Void;

    public TileType Type
    {
        get { return _type; }
        set
        {
            //Keep track of the old type before we replace it.
            TileType oldtype = _type;
            _type = value;

        }
    }

    //Atom data
    public int X { get; set; }
    public int Y { get; set; }
    public int Z { get; set; }
    public string Examine { get; set; }
    public List<object> Contents { get; set; }

    //Constructor for tile-check operations...
    public Tile(int x, int y, int z)
    {
        this.X = x;
        this.Y = y;
        this.Z = z;
    }

    public Tile(WorldData world, int x, int y, int z)
    {
        this.World = world;
        this.X = x;
        this.Y = y;
        this.Z = z;

        this.Contents = new List<object>();
        this.Contents.Add(this);

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
        neighbors[0] = World.WorldTiles[X, Z+1];
        //E
        neighbors[1] = World.WorldTiles[X+1, Z];
        //S
        neighbors[2] = World.WorldTiles[X, Z-1];
        //W
        neighbors[3] = World.WorldTiles[X-1, Z];

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
        neighbors[0] = World.WorldTiles[X, Z + 1];
        //E
        neighbors[1] = World.WorldTiles[X + 1, Z];
        //S
        neighbors[2] = World.WorldTiles[X, Z - 1];
        //W
        neighbors[3] = World.WorldTiles[X - 1, Z];
        //NE
        neighbors[4] = World.WorldTiles[X + 1, Z + 1];
        //NW
        neighbors[5] = World.WorldTiles[X - 1, Z + 1];
        //SE
        neighbors[6] = World.WorldTiles[X + 1, Z - 1];
        //SW
        neighbors[7] = World.WorldTiles[X - 1, Z - 1];

        return neighbors;
    }



}