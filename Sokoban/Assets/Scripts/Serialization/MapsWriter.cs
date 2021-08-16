using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Used for making sample map JSONs
public class MapsWriter : MonoBehaviour
{
    private List<MapData> AllMaps = new List<MapData>();

    [SerializeField]
    private MapData mapData0 = new MapData
    {
        mapWidth = 8,
        mapHeight = 5,
        mapTilesData = new List<MapTileData>()
        {
            new MapTileData(0, EnumTileType.Wall), new MapTileData(1, EnumTileType.Wall), new MapTileData(2, EnumTileType.Wall), new MapTileData(3, EnumTileType.Wall), new MapTileData(4, EnumTileType.Wall), new MapTileData(5, EnumTileType.Wall), new MapTileData(6, EnumTileType.None), new MapTileData(7, EnumTileType.None),
            new MapTileData(8, EnumTileType.Wall), new MapTileData(9, EnumTileType.Target), new MapTileData(10, EnumTileType.Grass), new MapTileData(11, EnumTileType.Grass), new MapTileData(12, EnumTileType.Grass), new MapTileData(13, EnumTileType.Wall), new MapTileData(14, EnumTileType.Wall), new MapTileData(15, EnumTileType.Wall),
            new MapTileData(16, EnumTileType.Wall), new MapTileData(17, EnumTileType.Grass), new MapTileData(18, EnumTileType.Box), new MapTileData(19, EnumTileType.Grass), new MapTileData(20, EnumTileType.Box), new MapTileData(21, EnumTileType.Grass), new MapTileData(22, EnumTileType.Grass), new MapTileData(23, EnumTileType.Wall),
            new MapTileData(24, EnumTileType.Wall), new MapTileData(25, EnumTileType.Grass), new MapTileData(26, EnumTileType.Target), new MapTileData(27, EnumTileType.Wall), new MapTileData(28, EnumTileType.Player), new MapTileData(29, EnumTileType.Grass), new MapTileData(30, EnumTileType.Grass), new MapTileData(31, EnumTileType.Wall),
            new MapTileData(32, EnumTileType.Wall), new MapTileData(33, EnumTileType.Wall), new MapTileData(34, EnumTileType.Wall), new MapTileData(35, EnumTileType.Wall), new MapTileData(36, EnumTileType.Wall), new MapTileData(37, EnumTileType.Wall), new MapTileData(38, EnumTileType.Wall), new MapTileData(39, EnumTileType.Wall)
        }
    };

    [SerializeField]
    private MapData mapData1 = new MapData
    {
        mapWidth = 11,
        mapHeight = 9,
        mapTilesData = new List<MapTileData>()
        {
            new MapTileData(0, EnumTileType.Wall), new MapTileData(1, EnumTileType.Wall), new MapTileData(2, EnumTileType.Wall), new MapTileData(3, EnumTileType.Wall), new MapTileData(4, EnumTileType.Wall), new MapTileData(5, EnumTileType.None), new MapTileData(6, EnumTileType.None), new MapTileData(7, EnumTileType.None), new MapTileData(8, EnumTileType.None), new MapTileData(9, EnumTileType.None), new MapTileData(10, EnumTileType.None),
            new MapTileData(11, EnumTileType.Wall), new MapTileData(12, EnumTileType.Grass), new MapTileData(13, EnumTileType.Player), new MapTileData(14, EnumTileType.Grass), new MapTileData(15, EnumTileType.Wall), new MapTileData(16, EnumTileType.Wall), new MapTileData(17, EnumTileType.Wall), new MapTileData(18, EnumTileType.Wall), new MapTileData(19, EnumTileType.Wall), new MapTileData(20, EnumTileType.Wall), new MapTileData(21, EnumTileType.Wall),
            new MapTileData(22, EnumTileType.Wall), new MapTileData(23, EnumTileType.Grass), new MapTileData(24, EnumTileType.Box), new MapTileData(25, EnumTileType.Grass), new MapTileData(26, EnumTileType.Grass), new MapTileData(27, EnumTileType.Grass), new MapTileData(28, EnumTileType.Grass), new MapTileData(29, EnumTileType.Grass), new MapTileData(30, EnumTileType.Box), new MapTileData(31, EnumTileType.Grass), new MapTileData(32, EnumTileType.Wall),
            new MapTileData(33, EnumTileType.Wall), new MapTileData(34, EnumTileType.Wall), new MapTileData(35, EnumTileType.Box), new MapTileData(36, EnumTileType.Wall), new MapTileData(37, EnumTileType.Wall), new MapTileData(38, EnumTileType.Wall), new MapTileData(39, EnumTileType.Wall), new MapTileData(40, EnumTileType.Target), new MapTileData(41, EnumTileType.Wall), new MapTileData(42, EnumTileType.Grass), new MapTileData(43, EnumTileType.Wall),
            new MapTileData(44, EnumTileType.Wall), new MapTileData(45, EnumTileType.Grass), new MapTileData(46, EnumTileType.Grass), new MapTileData(47, EnumTileType.Grass), new MapTileData(48, EnumTileType.Grass), new MapTileData(49, EnumTileType.Grass), new MapTileData(50, EnumTileType.Target), new MapTileData(51, EnumTileType.Target), new MapTileData(52, EnumTileType.Target), new MapTileData(53, EnumTileType.Grass), new MapTileData(54, EnumTileType.Wall),
            new MapTileData(55, EnumTileType.Wall), new MapTileData(56, EnumTileType.Grass), new MapTileData(57, EnumTileType.Box), new MapTileData(58, EnumTileType.Grass), new MapTileData(59, EnumTileType.Box), new MapTileData(60, EnumTileType.Grass), new MapTileData(61, EnumTileType.Wall), new MapTileData(62, EnumTileType.Target), new MapTileData(63, EnumTileType.Wall), new MapTileData(64, EnumTileType.Wall), new MapTileData(65, EnumTileType.Wall),
            new MapTileData(66, EnumTileType.Wall), new MapTileData(67, EnumTileType.Wall), new MapTileData(68, EnumTileType.Wall), new MapTileData(69, EnumTileType.Wall), new MapTileData(70, EnumTileType.Wall), new MapTileData(71, EnumTileType.Grass), new MapTileData(72, EnumTileType.Grass), new MapTileData(73, EnumTileType.Grass), new MapTileData(74, EnumTileType.Grass), new MapTileData(75, EnumTileType.Wall), new MapTileData(76, EnumTileType.None),
            new MapTileData(77, EnumTileType.None), new MapTileData(78, EnumTileType.None), new MapTileData(79, EnumTileType.None), new MapTileData(80, EnumTileType.None), new MapTileData(81, EnumTileType.Wall), new MapTileData(82, EnumTileType.Wall), new MapTileData(83, EnumTileType.Grass), new MapTileData(84, EnumTileType.Grass), new MapTileData(85, EnumTileType.Grass), new MapTileData(86, EnumTileType.Wall), new MapTileData(87, EnumTileType.None),
            new MapTileData(88, EnumTileType.None), new MapTileData(89, EnumTileType.None), new MapTileData(90, EnumTileType.None), new MapTileData(91, EnumTileType.None), new MapTileData(92, EnumTileType.None), new MapTileData(93, EnumTileType.Wall), new MapTileData(94, EnumTileType.Wall), new MapTileData(95, EnumTileType.Wall), new MapTileData(96, EnumTileType.Wall), new MapTileData(97, EnumTileType.Wall), new MapTileData(98, EnumTileType.None)
        }
    };

    [SerializeField]
    private MapData mapData2 = new MapData
    {
        mapWidth = 17,
        mapHeight = 12,
        mapTilesData = new List<MapTileData>()
        {
            new MapTileData(0, EnumTileType.None), new MapTileData(1, EnumTileType.None), new MapTileData(2, EnumTileType.Wall), new MapTileData(3, EnumTileType.Wall), new MapTileData(4, EnumTileType.Wall), new MapTileData(5, EnumTileType.Wall), new MapTileData(6, EnumTileType.Wall), new MapTileData(7, EnumTileType.Wall), new MapTileData(8, EnumTileType.Wall), new MapTileData(9, EnumTileType.Wall), new MapTileData(10, EnumTileType.Wall), new MapTileData(11, EnumTileType.Wall), new MapTileData(12, EnumTileType.None), new MapTileData(13, EnumTileType.None), new MapTileData(14, EnumTileType.None), new MapTileData(15, EnumTileType.None), new MapTileData(16, EnumTileType.None), 
            new MapTileData(17, EnumTileType.None), new MapTileData(18, EnumTileType.None), new MapTileData(19, EnumTileType.Wall), new MapTileData(20, EnumTileType.Grass), new MapTileData(21, EnumTileType.Grass), new MapTileData(22, EnumTileType.Grass), new MapTileData(23, EnumTileType.Grass), new MapTileData(24, EnumTileType.Wall), new MapTileData(25, EnumTileType.Grass), new MapTileData(26, EnumTileType.Grass), new MapTileData(27, EnumTileType.Grass), new MapTileData(28, EnumTileType.Wall), new MapTileData(29, EnumTileType.None), new MapTileData(30, EnumTileType.None), new MapTileData(31, EnumTileType.None), new MapTileData(32, EnumTileType.None), new MapTileData(33, EnumTileType.None), 
            new MapTileData(34, EnumTileType.None), new MapTileData(35, EnumTileType.Wall), new MapTileData(36, EnumTileType.Wall), new MapTileData(37, EnumTileType.Grass), new MapTileData(38, EnumTileType.Grass), new MapTileData(39, EnumTileType.Grass), new MapTileData(40, EnumTileType.Box), new MapTileData(41, EnumTileType.Grass), new MapTileData(42, EnumTileType.Grass), new MapTileData(43, EnumTileType.Grass), new MapTileData(44, EnumTileType.Grass), new MapTileData(45, EnumTileType.Wall), new MapTileData(46, EnumTileType.Wall), new MapTileData(47, EnumTileType.Wall), new MapTileData(48, EnumTileType.Wall), new MapTileData(49, EnumTileType.Wall), new MapTileData(50, EnumTileType.Wall), 
            new MapTileData(51, EnumTileType.Wall), new MapTileData(52, EnumTileType.Wall), new MapTileData(53, EnumTileType.Grass), new MapTileData(54, EnumTileType.Box), new MapTileData(55, EnumTileType.Box), new MapTileData(56, EnumTileType.Box), new MapTileData(57, EnumTileType.Wall), new MapTileData(58, EnumTileType.Wall), new MapTileData(59, EnumTileType.Wall), new MapTileData(60, EnumTileType.Wall), new MapTileData(61, EnumTileType.Grass), new MapTileData(62, EnumTileType.Wall), new MapTileData(63, EnumTileType.Wall), new MapTileData(64, EnumTileType.Grass), new MapTileData(65, EnumTileType.Grass), new MapTileData(66, EnumTileType.Grass), new MapTileData(67, EnumTileType.Wall), 
            new MapTileData(68, EnumTileType.Wall), new MapTileData(69, EnumTileType.Grass), new MapTileData(70, EnumTileType.Box), new MapTileData(71, EnumTileType.Grass), new MapTileData(72, EnumTileType.Grass), new MapTileData(73, EnumTileType.Grass), new MapTileData(74, EnumTileType.Grass), new MapTileData(75, EnumTileType.Wall), new MapTileData(76, EnumTileType.Wall), new MapTileData(77, EnumTileType.Grass), new MapTileData(78, EnumTileType.Grass), new MapTileData(79, EnumTileType.Grass), new MapTileData(80, EnumTileType.Grass), new MapTileData(81, EnumTileType.Grass), new MapTileData(82, EnumTileType.Wall), new MapTileData(83, EnumTileType.Grass), new MapTileData(84, EnumTileType.Wall), 
            new MapTileData(85, EnumTileType.Wall), new MapTileData(86, EnumTileType.Grass), new MapTileData(87, EnumTileType.Box), new MapTileData(88, EnumTileType.Grass), new MapTileData(89, EnumTileType.Wall), new MapTileData(90, EnumTileType.Wall), new MapTileData(91, EnumTileType.Target), new MapTileData(92, EnumTileType.Target), new MapTileData(93, EnumTileType.Target), new MapTileData(94, EnumTileType.Grass), new MapTileData(95, EnumTileType.Grass), new MapTileData(96, EnumTileType.Wall), new MapTileData(97, EnumTileType.Wall), new MapTileData(98, EnumTileType.Grass), new MapTileData(99, EnumTileType.Grass), new MapTileData(100, EnumTileType.Grass), new MapTileData(101, EnumTileType.Wall),
            new MapTileData(102, EnumTileType.Wall), new MapTileData(103, EnumTileType.Grass), new MapTileData(104, EnumTileType.Grass), new MapTileData(105, EnumTileType.Box), new MapTileData(106, EnumTileType.Player), new MapTileData(107, EnumTileType.Wall), new MapTileData(108, EnumTileType.Target), new MapTileData(109, EnumTileType.Box), new MapTileData(110, EnumTileType.Target), new MapTileData(111, EnumTileType.Wall), new MapTileData(112, EnumTileType.Wall), new MapTileData(113, EnumTileType.Wall), new MapTileData(114, EnumTileType.Wall), new MapTileData(115, EnumTileType.Grass), new MapTileData(116, EnumTileType.Wall), new MapTileData(117, EnumTileType.Grass), new MapTileData(118, EnumTileType.Wall),
            new MapTileData(119, EnumTileType.Wall), new MapTileData(120, EnumTileType.Wall), new MapTileData(121, EnumTileType.Wall), new MapTileData(122, EnumTileType.Grass), new MapTileData(123, EnumTileType.Grass), new MapTileData(124, EnumTileType.Wall), new MapTileData(125, EnumTileType.Target), new MapTileData(126, EnumTileType.Target), new MapTileData(127, EnumTileType.Target), new MapTileData(128, EnumTileType.Grass), new MapTileData(129, EnumTileType.Grass), new MapTileData(130, EnumTileType.Grass), new MapTileData(131, EnumTileType.Wall), new MapTileData(132, EnumTileType.Grass), new MapTileData(133, EnumTileType.Box), new MapTileData(134, EnumTileType.Grass), new MapTileData(135, EnumTileType.Wall),
            new MapTileData(136, EnumTileType.None), new MapTileData(137, EnumTileType.None), new MapTileData(138, EnumTileType.Wall), new MapTileData(139, EnumTileType.Wall), new MapTileData(140, EnumTileType.Wall), new MapTileData(141, EnumTileType.Wall), new MapTileData(142, EnumTileType.Wall), new MapTileData(143, EnumTileType.Wall), new MapTileData(144, EnumTileType.Grass), new MapTileData(145, EnumTileType.Wall), new MapTileData(146, EnumTileType.Wall), new MapTileData(147, EnumTileType.Grass), new MapTileData(148, EnumTileType.Wall), new MapTileData(149, EnumTileType.Grass), new MapTileData(150, EnumTileType.Wall), new MapTileData(151, EnumTileType.Wall), new MapTileData(152, EnumTileType.Wall),
            new MapTileData(153, EnumTileType.None), new MapTileData(154, EnumTileType.None), new MapTileData(155, EnumTileType.None), new MapTileData(156, EnumTileType.None), new MapTileData(157, EnumTileType.None), new MapTileData(158, EnumTileType.None), new MapTileData(159, EnumTileType.None), new MapTileData(160, EnumTileType.Wall), new MapTileData(161, EnumTileType.Grass), new MapTileData(162, EnumTileType.Grass), new MapTileData(163, EnumTileType.Grass), new MapTileData(164, EnumTileType.Grass), new MapTileData(165, EnumTileType.Grass), new MapTileData(166, EnumTileType.Grass), new MapTileData(167, EnumTileType.Grass), new MapTileData(168, EnumTileType.Wall), new MapTileData(169, EnumTileType.None),
            new MapTileData(170, EnumTileType.None), new MapTileData(171, EnumTileType.None), new MapTileData(172, EnumTileType.None), new MapTileData(173, EnumTileType.None), new MapTileData(174, EnumTileType.None), new MapTileData(175, EnumTileType.None), new MapTileData(176, EnumTileType.None), new MapTileData(177, EnumTileType.Wall), new MapTileData(178, EnumTileType.Grass), new MapTileData(179, EnumTileType.Grass), new MapTileData(180, EnumTileType.Grass), new MapTileData(181, EnumTileType.Wall), new MapTileData(182, EnumTileType.Grass), new MapTileData(183, EnumTileType.Grass), new MapTileData(184, EnumTileType.Grass), new MapTileData(185, EnumTileType.Wall), new MapTileData(186, EnumTileType.None),
            new MapTileData(187, EnumTileType.None), new MapTileData(188, EnumTileType.None), new MapTileData(189, EnumTileType.None), new MapTileData(190, EnumTileType.None), new MapTileData(191, EnumTileType.None), new MapTileData(192, EnumTileType.None), new MapTileData(193, EnumTileType.None), new MapTileData(194, EnumTileType.Wall), new MapTileData(195, EnumTileType.Wall), new MapTileData(196, EnumTileType.Wall), new MapTileData(197, EnumTileType.Wall), new MapTileData(198, EnumTileType.Wall), new MapTileData(199, EnumTileType.Wall), new MapTileData(200, EnumTileType.Wall), new MapTileData(201, EnumTileType.Wall), new MapTileData(202, EnumTileType.Wall), new MapTileData(203, EnumTileType.None)
        }
    };

    [SerializeField]
    private MapData mapData3 = new MapData
    {
        mapWidth = 21,
        mapHeight = 13,
        mapTilesData = new List<MapTileData>()
        {
            new MapTileData(0, EnumTileType.None), new MapTileData(1, EnumTileType.None), new MapTileData(2, EnumTileType.None), new MapTileData(3, EnumTileType.None), new MapTileData(4, EnumTileType.None), new MapTileData(5, EnumTileType.None), new MapTileData(6, EnumTileType.None), new MapTileData(7, EnumTileType.None), new MapTileData(8, EnumTileType.None), new MapTileData(9, EnumTileType.None), new MapTileData(10, EnumTileType.None), new MapTileData(11, EnumTileType.None), new MapTileData(12, EnumTileType.None), new MapTileData(13, EnumTileType.None), new MapTileData(14, EnumTileType.Wall), new MapTileData(15, EnumTileType.Wall), new MapTileData(16, EnumTileType.Wall), new MapTileData(17, EnumTileType.Wall), new MapTileData(18, EnumTileType.None), new MapTileData(19, EnumTileType.None), new MapTileData(20, EnumTileType.None),
            new MapTileData(21, EnumTileType.None), new MapTileData(22, EnumTileType.Wall), new MapTileData(23, EnumTileType.Wall), new MapTileData(24, EnumTileType.Wall), new MapTileData(25, EnumTileType.Wall), new MapTileData(26, EnumTileType.Wall), new MapTileData(27, EnumTileType.Wall), new MapTileData(28, EnumTileType.Wall), new MapTileData(29, EnumTileType.Wall), new MapTileData(30, EnumTileType.Wall), new MapTileData(31, EnumTileType.Wall), new MapTileData(32, EnumTileType.Wall), new MapTileData(33, EnumTileType.Wall), new MapTileData(34, EnumTileType.Wall), new MapTileData(35, EnumTileType.Wall), new MapTileData(36, EnumTileType.Grass), new MapTileData(37, EnumTileType.Grass), new MapTileData(38, EnumTileType.Wall), new MapTileData(39, EnumTileType.None), new MapTileData(40, EnumTileType.None), new MapTileData(41, EnumTileType.None),
            new MapTileData(42, EnumTileType.Wall), new MapTileData(43, EnumTileType.Wall), new MapTileData(44, EnumTileType.Grass), new MapTileData(45, EnumTileType.Box), new MapTileData(46, EnumTileType.Grass), new MapTileData(47, EnumTileType.Box), new MapTileData(48, EnumTileType.Grass), new MapTileData(49, EnumTileType.Box), new MapTileData(50, EnumTileType.Grass), new MapTileData(51, EnumTileType.Box), new MapTileData(52, EnumTileType.Grass), new MapTileData(53, EnumTileType.Box), new MapTileData(54, EnumTileType.Grass), new MapTileData(55, EnumTileType.Grass), new MapTileData(56, EnumTileType.Box), new MapTileData(57, EnumTileType.Grass), new MapTileData(58, EnumTileType.Grass), new MapTileData(59, EnumTileType.Wall), new MapTileData(60, EnumTileType.None), new MapTileData(61, EnumTileType.None), new MapTileData(62, EnumTileType.None),
            new MapTileData(63, EnumTileType.Wall), new MapTileData(64, EnumTileType.Grass), new MapTileData(65, EnumTileType.Grass), new MapTileData(66, EnumTileType.Wall), new MapTileData(67, EnumTileType.Grass), new MapTileData(68, EnumTileType.Wall), new MapTileData(69, EnumTileType.Grass), new MapTileData(70, EnumTileType.Wall), new MapTileData(71, EnumTileType.Grass), new MapTileData(72, EnumTileType.Wall), new MapTileData(73, EnumTileType.Grass), new MapTileData(74, EnumTileType.Wall), new MapTileData(75, EnumTileType.Grass), new MapTileData(76, EnumTileType.Wall), new MapTileData(77, EnumTileType.Grass), new MapTileData(78, EnumTileType.Wall), new MapTileData(79, EnumTileType.Grass), new MapTileData(80, EnumTileType.Wall), new MapTileData(81, EnumTileType.Wall), new MapTileData(82, EnumTileType.Wall), new MapTileData(83, EnumTileType.Wall),
            new MapTileData(84, EnumTileType.Wall), new MapTileData(85, EnumTileType.Grass), new MapTileData(86, EnumTileType.Grass), new MapTileData(87, EnumTileType.Grass), new MapTileData(88, EnumTileType.Grass), new MapTileData(89, EnumTileType.Grass), new MapTileData(90, EnumTileType.Grass), new MapTileData(91, EnumTileType.Grass), new MapTileData(92, EnumTileType.Grass), new MapTileData(93, EnumTileType.Grass), new MapTileData(94, EnumTileType.Grass), new MapTileData(95, EnumTileType.Grass), new MapTileData(96, EnumTileType.Grass), new MapTileData(97, EnumTileType.Grass), new MapTileData(98, EnumTileType.Grass), new MapTileData(99, EnumTileType.Grass), new MapTileData(100, EnumTileType.Grass), new MapTileData(101, EnumTileType.Grass), new MapTileData(102, EnumTileType.Grass), new MapTileData(103, EnumTileType.Grass), new MapTileData(104, EnumTileType.Wall),
            new MapTileData(105, EnumTileType.Wall), new MapTileData(106, EnumTileType.Grass), new MapTileData(107, EnumTileType.Grass), new MapTileData(108, EnumTileType.Wall), new MapTileData(109, EnumTileType.Wall), new MapTileData(110, EnumTileType.Wall), new MapTileData(111, EnumTileType.Wall), new MapTileData(112, EnumTileType.Wall), new MapTileData(113, EnumTileType.Wall), new MapTileData(114, EnumTileType.Wall), new MapTileData(115, EnumTileType.Wall), new MapTileData(116, EnumTileType.Wall), new MapTileData(117, EnumTileType.Wall), new MapTileData(118, EnumTileType.Wall), new MapTileData(119, EnumTileType.Wall), new MapTileData(120, EnumTileType.Wall), new MapTileData(121, EnumTileType.Wall), new MapTileData(122, EnumTileType.Wall), new MapTileData(123, EnumTileType.Grass), new MapTileData(124, EnumTileType.Grass), new MapTileData(125, EnumTileType.Wall),
            new MapTileData(126, EnumTileType.Wall), new MapTileData(127, EnumTileType.Wall), new MapTileData(128, EnumTileType.Grass), new MapTileData(129, EnumTileType.Wall), new MapTileData(130, EnumTileType.Grass), new MapTileData(131, EnumTileType.Grass), new MapTileData(132, EnumTileType.Grass), new MapTileData(133, EnumTileType.Grass), new MapTileData(134, EnumTileType.Grass), new MapTileData(135, EnumTileType.Grass), new MapTileData(136, EnumTileType.Wall), new MapTileData(137, EnumTileType.Grass), new MapTileData(138, EnumTileType.Grass), new MapTileData(139, EnumTileType.Wall), new MapTileData(140, EnumTileType.Wall), new MapTileData(141, EnumTileType.Grass), new MapTileData(142, EnumTileType.Grass), new MapTileData(143, EnumTileType.Wall), new MapTileData(144, EnumTileType.Grass), new MapTileData(145, EnumTileType.Wall), new MapTileData(146, EnumTileType.Wall),
            new MapTileData(147, EnumTileType.None), new MapTileData(148, EnumTileType.Wall), new MapTileData(149, EnumTileType.Grass), new MapTileData(150, EnumTileType.Wall), new MapTileData(151, EnumTileType.Grass), new MapTileData(152, EnumTileType.Grass), new MapTileData(153, EnumTileType.Grass), new MapTileData(154, EnumTileType.Grass), new MapTileData(155, EnumTileType.Grass), new MapTileData(156, EnumTileType.Grass), new MapTileData(157, EnumTileType.Wall), new MapTileData(158, EnumTileType.Grass), new MapTileData(159, EnumTileType.Grass), new MapTileData(160, EnumTileType.Grass), new MapTileData(161, EnumTileType.Grass), new MapTileData(162, EnumTileType.Grass), new MapTileData(163, EnumTileType.Grass), new MapTileData(164, EnumTileType.Wall), new MapTileData(165, EnumTileType.Grass), new MapTileData(166, EnumTileType.Wall), new MapTileData(167, EnumTileType.None),
            new MapTileData(168, EnumTileType.Wall), new MapTileData(169, EnumTileType.Wall), new MapTileData(170, EnumTileType.Grass), new MapTileData(171, EnumTileType.Wall), new MapTileData(172, EnumTileType.Grass), new MapTileData(173, EnumTileType.Grass), new MapTileData(174, EnumTileType.Grass), new MapTileData(175, EnumTileType.Grass), new MapTileData(176, EnumTileType.Grass), new MapTileData(177, EnumTileType.Grass), new MapTileData(178, EnumTileType.Wall), new MapTileData(179, EnumTileType.Grass), new MapTileData(180, EnumTileType.Grass), new MapTileData(181, EnumTileType.Wall), new MapTileData(182, EnumTileType.Wall), new MapTileData(183, EnumTileType.Grass), new MapTileData(184, EnumTileType.Grass), new MapTileData(185, EnumTileType.Wall), new MapTileData(186, EnumTileType.Grass), new MapTileData(187, EnumTileType.Wall), new MapTileData(188, EnumTileType.None),
            new MapTileData(189, EnumTileType.Wall), new MapTileData(190, EnumTileType.Grass), new MapTileData(191, EnumTileType.Grass), new MapTileData(192, EnumTileType.Wall), new MapTileData(193, EnumTileType.Wall), new MapTileData(194, EnumTileType.Wall), new MapTileData(195, EnumTileType.Wall), new MapTileData(196, EnumTileType.Wall), new MapTileData(197, EnumTileType.Wall), new MapTileData(198, EnumTileType.Wall), new MapTileData(199, EnumTileType.Wall), new MapTileData(200, EnumTileType.Wall), new MapTileData(201, EnumTileType.Box), new MapTileData(202, EnumTileType.Wall), new MapTileData(203, EnumTileType.Grass), new MapTileData(204, EnumTileType.Box), new MapTileData(205, EnumTileType.Wall), new MapTileData(206, EnumTileType.Wall), new MapTileData(207, EnumTileType.Grass), new MapTileData(208, EnumTileType.Wall), new MapTileData(209, EnumTileType.None),
            new MapTileData(210, EnumTileType.Wall), new MapTileData(211, EnumTileType.Grass), new MapTileData(212, EnumTileType.Grass), new MapTileData(213, EnumTileType.Target), new MapTileData(214, EnumTileType.Target), new MapTileData(215, EnumTileType.Target), new MapTileData(216, EnumTileType.Target), new MapTileData(217, EnumTileType.Target), new MapTileData(218, EnumTileType.Target), new MapTileData(219, EnumTileType.Target), new MapTileData(220, EnumTileType.Target), new MapTileData(221, EnumTileType.Box), new MapTileData(222, EnumTileType.Player), new MapTileData(223, EnumTileType.Wall), new MapTileData(224, EnumTileType.Grass), new MapTileData(225, EnumTileType.Grass), new MapTileData(226, EnumTileType.Grass), new MapTileData(227, EnumTileType.Grass), new MapTileData(228, EnumTileType.Grass), new MapTileData(229, EnumTileType.Wall), new MapTileData(230, EnumTileType.None),
            new MapTileData(231, EnumTileType.Wall), new MapTileData(232, EnumTileType.Grass), new MapTileData(233, EnumTileType.Grass), new MapTileData(234, EnumTileType.Wall), new MapTileData(235, EnumTileType.Wall), new MapTileData(236, EnumTileType.Wall), new MapTileData(237, EnumTileType.Wall), new MapTileData(238, EnumTileType.Wall), new MapTileData(239, EnumTileType.Wall), new MapTileData(240, EnumTileType.Wall), new MapTileData(241, EnumTileType.Wall), new MapTileData(242, EnumTileType.Wall), new MapTileData(243, EnumTileType.Wall), new MapTileData(244, EnumTileType.Wall), new MapTileData(245, EnumTileType.Wall), new MapTileData(246, EnumTileType.Wall), new MapTileData(247, EnumTileType.Wall), new MapTileData(248, EnumTileType.Grass), new MapTileData(249, EnumTileType.Grass), new MapTileData(250, EnumTileType.Wall), new MapTileData(251, EnumTileType.None),
            new MapTileData(252, EnumTileType.Wall), new MapTileData(253, EnumTileType.Wall), new MapTileData(254, EnumTileType.Wall), new MapTileData(255, EnumTileType.Wall), new MapTileData(256, EnumTileType.None), new MapTileData(257, EnumTileType.None), new MapTileData(258, EnumTileType.None), new MapTileData(259, EnumTileType.None), new MapTileData(260, EnumTileType.None), new MapTileData(261, EnumTileType.None), new MapTileData(262, EnumTileType.None), new MapTileData(263, EnumTileType.None), new MapTileData(264, EnumTileType.None), new MapTileData(265, EnumTileType.None), new MapTileData(266, EnumTileType.None), new MapTileData(267, EnumTileType.None), new MapTileData(268, EnumTileType.Wall), new MapTileData(269, EnumTileType.Wall), new MapTileData(270, EnumTileType.Wall), new MapTileData(271, EnumTileType.Wall), new MapTileData(272, EnumTileType.None)
        }
    };

    public bool useScriptableObjectsToCreateMap;
    public List<ScriptableObjectTest> scriptableObjectMaps;

    private LevelDataCollection levelCollection = new LevelDataCollection();
    private List<LevelData> levelData = new List<LevelData>();

    public void SaveAllMapsToJSON()
    {
        bool mapsAlreadyExist = true;
        int mapID = -1;

        foreach (MapData mapData in AllMaps)
        {
            mapID++;

            if (!System.IO.File.Exists(GameFilePaths.MapFileName(mapID)))
            {
                levelData.Add(new LevelData
                {
                    LevelName = string.Format("Level {0}", mapID),
                    MapID = mapID,
                    IsCompleted = false,
                    NumbersPlayed = 0,
                    BestCompletedTimeInSeconds = -1
                });

                string mapJSON = JsonUtility.ToJson(mapData);
                System.IO.File.WriteAllText(GameFilePaths.MapFileName(mapID), mapJSON);

                mapsAlreadyExist = false;
            }
        }

        if(!mapsAlreadyExist)
        {
            levelCollection.LevelsData = levelData;
            LevelController.Instance.CreateInitialLevelCollectionFromMaps(levelCollection);
        }
    }

    public void SaveAllMapsToJSONFromScriptableObjects()
    {
        bool mapsAlreadyExist = true;
        int mapID = -1;

        foreach (ScriptableObjectTest scriptableObjectData in scriptableObjectMaps)
        {
            mapID++;

            if (!System.IO.File.Exists(GameFilePaths.MapFileName(mapID)))
            {
                levelData.Add(new LevelData
                {
                    LevelName = string.Format("Level {0}", mapID),
                    MapID = mapID,
                    IsCompleted = false,
                    NumbersPlayed = 0,
                    BestCompletedTimeInSeconds = -1
                });

                string mapJSON = JsonUtility.ToJson(scriptableObjectData.mapData);
                System.IO.File.WriteAllText(GameFilePaths.MapFileName(mapID), mapJSON);

                mapsAlreadyExist = false;
            }
        }

        if (!mapsAlreadyExist)
        {
            levelCollection.LevelsData = levelData;
            LevelController.Instance.CreateInitialLevelCollectionFromMaps(levelCollection);
        }
    }

    private void Awake()
    {
        if(!useScriptableObjectsToCreateMap)
        {
            AllMaps.Add(mapData0);
            AllMaps.Add(mapData1);
            AllMaps.Add(mapData2);
            AllMaps.Add(mapData3);
            SaveAllMapsToJSON();
        }
        else
        {
            SaveAllMapsToJSONFromScriptableObjects();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
