using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Used for creating maps. Will be empty when all maps are created.
[System.Serializable]
public class MapData
{
    public int mapWidth;
    public int mapHeight;
    public List<MapTileData> mapTilesData;
    //public List<MapTileData> mapTilesData = new List<MapTileData>()
    //{
    //        //Map1
    //        //new MapTileData(0, EnumTileType.Wall), new MapTileData(1, EnumTileType.Wall),
    //        //new MapTileData(2, EnumTileType.Wall), new MapTileData(3, EnumTileType.Wall),
    //        //new MapTileData(4, EnumTileType.Wall), new MapTileData(5, EnumTileType.Wall),
    //        //new MapTileData(6, EnumTileType.None), new MapTileData(7, EnumTileType.None),
    //        //new MapTileData(8, EnumTileType.Wall), new MapTileData(9, EnumTileType.Target),
    //        //new MapTileData(10, EnumTileType.Grass), new MapTileData(11, EnumTileType.Grass),
    //        //new MapTileData(12, EnumTileType.Grass), new MapTileData(13, EnumTileType.Wall),
    //        //new MapTileData(14, EnumTileType.Wall), new MapTileData(15, EnumTileType.Wall),
    //        //new MapTileData(16, EnumTileType.Wall), new MapTileData(17, EnumTileType.Grass),
    //        //new MapTileData(18, EnumTileType.Box), new MapTileData(19, EnumTileType.Grass),
    //        //new MapTileData(20, EnumTileType.Box), new MapTileData(21, EnumTileType.Grass),
    //        //new MapTileData(22, EnumTileType.Grass), new MapTileData(23, EnumTileType.Wall),
    //        //new MapTileData(24, EnumTileType.Wall), new MapTileData(25, EnumTileType.Grass),
    //        //new MapTileData(26, EnumTileType.Target), new MapTileData(27, EnumTileType.Wall),
    //        //new MapTileData(28, EnumTileType.Player), new MapTileData(29, EnumTileType.Grass),
    //        //new MapTileData(30, EnumTileType.Grass), new MapTileData(31, EnumTileType.Wall),
    //        //new MapTileData(32, EnumTileType.Wall), new MapTileData(33, EnumTileType.Wall),
    //        //new MapTileData(34, EnumTileType.Wall), new MapTileData(35, EnumTileType.Wall),
    //        //new MapTileData(36, EnumTileType.Wall), new MapTileData(37, EnumTileType.Wall),
    //        //new MapTileData(38, EnumTileType.Wall), new MapTileData(39, EnumTileType.Wall)

    //        //Map2
    //        //new MapTileData(0, EnumTileType.Wall), new MapTileData(1, EnumTileType.Wall), new MapTileData(2, EnumTileType.Wall), new MapTileData(3, EnumTileType.Wall), new MapTileData(4, EnumTileType.Wall), new MapTileData(5, EnumTileType.None), new MapTileData(6, EnumTileType.None), new MapTileData(7, EnumTileType.None), new MapTileData(8, EnumTileType.None), new MapTileData(9, EnumTileType.None), new MapTileData(10, EnumTileType.None),
    //        //new MapTileData(11, EnumTileType.Wall), new MapTileData(12, EnumTileType.Grass), new MapTileData(13, EnumTileType.Player), new MapTileData(14, EnumTileType.Grass), new MapTileData(15, EnumTileType.Wall), new MapTileData(16, EnumTileType.Wall), new MapTileData(17, EnumTileType.Wall), new MapTileData(18, EnumTileType.Wall), new MapTileData(19, EnumTileType.Wall), new MapTileData(20, EnumTileType.Wall), new MapTileData(21, EnumTileType.Wall),
    //        //new MapTileData(22, EnumTileType.Wall), new MapTileData(23, EnumTileType.Grass), new MapTileData(24, EnumTileType.Box), new MapTileData(25, EnumTileType.Grass), new MapTileData(26, EnumTileType.Grass), new MapTileData(27, EnumTileType.Grass), new MapTileData(28, EnumTileType.Grass), new MapTileData(29, EnumTileType.Grass), new MapTileData(30, EnumTileType.Box), new MapTileData(31, EnumTileType.Grass), new MapTileData(32, EnumTileType.Wall),
    //        //new MapTileData(33, EnumTileType.Wall), new MapTileData(34, EnumTileType.Wall), new MapTileData(35, EnumTileType.Box), new MapTileData(36, EnumTileType.Wall), new MapTileData(37, EnumTileType.Wall), new MapTileData(38, EnumTileType.Wall), new MapTileData(39, EnumTileType.Wall), new MapTileData(40, EnumTileType.Target), new MapTileData(41, EnumTileType.Wall), new MapTileData(42, EnumTileType.Grass), new MapTileData(43, EnumTileType.Wall),
    //        //new MapTileData(44, EnumTileType.Wall), new MapTileData(45, EnumTileType.Grass), new MapTileData(46, EnumTileType.Grass), new MapTileData(47, EnumTileType.Grass), new MapTileData(48, EnumTileType.Grass), new MapTileData(49, EnumTileType.Grass), new MapTileData(50, EnumTileType.Target), new MapTileData(51, EnumTileType.Target), new MapTileData(52, EnumTileType.Target), new MapTileData(53, EnumTileType.Grass), new MapTileData(54, EnumTileType.Wall),
    //        //new MapTileData(55, EnumTileType.Wall), new MapTileData(56, EnumTileType.Grass), new MapTileData(57, EnumTileType.Box), new MapTileData(58, EnumTileType.Grass), new MapTileData(59, EnumTileType.Box), new MapTileData(60, EnumTileType.Grass), new MapTileData(61, EnumTileType.Wall), new MapTileData(62, EnumTileType.Target), new MapTileData(63, EnumTileType.Wall), new MapTileData(64, EnumTileType.Wall), new MapTileData(65, EnumTileType.Wall),
    //        //new MapTileData(66, EnumTileType.Wall), new MapTileData(67, EnumTileType.Wall), new MapTileData(68, EnumTileType.Wall), new MapTileData(69, EnumTileType.Wall), new MapTileData(70, EnumTileType.Wall), new MapTileData(71, EnumTileType.Grass), new MapTileData(72, EnumTileType.Grass), new MapTileData(73, EnumTileType.Grass), new MapTileData(74, EnumTileType.Grass), new MapTileData(75, EnumTileType.Wall), new MapTileData(76, EnumTileType.None),
    //        //new MapTileData(77, EnumTileType.None), new MapTileData(78, EnumTileType.None), new MapTileData(79, EnumTileType.None), new MapTileData(80, EnumTileType.None), new MapTileData(81, EnumTileType.Wall), new MapTileData(82, EnumTileType.Wall), new MapTileData(83, EnumTileType.Grass), new MapTileData(84, EnumTileType.Grass), new MapTileData(85, EnumTileType.Grass), new MapTileData(86, EnumTileType.Wall), new MapTileData(87, EnumTileType.None),
    //        //new MapTileData(88, EnumTileType.None), new MapTileData(89, EnumTileType.None), new MapTileData(90, EnumTileType.None), new MapTileData(91, EnumTileType.None), new MapTileData(92, EnumTileType.None), new MapTileData(93, EnumTileType.Wall), new MapTileData(94, EnumTileType.Wall), new MapTileData(95, EnumTileType.Wall), new MapTileData(96, EnumTileType.Wall), new MapTileData(97, EnumTileType.Wall), new MapTileData(98, EnumTileType.None)
    //};
}
