using System.Collections.Generic;
using UnityEngine;

public static class BoardTypeData
{
    public static Dictionary<ItemType, BoardData> boardDatas = new Dictionary<ItemType, BoardData>()
    {
        {
            ItemType.NONE,
            new BoardData(
                ItemType.NONE,
                "NONE",
                Color.white
                )
        },

        {
            ItemType.BEEF_NORMAL,
            new BoardData(
                ItemType.BOARD_BEEFNORMAL,
                "BIFE",
                new Color(0.9622641f, 0.3213598f, 0.3213598f)
                )
        },
        {
            ItemType.BEEF_VEGAN,
            new BoardData(
                ItemType.BOARD_BEEFVEGAN,
                "BIFE VEGAN",
                new Color(0.2996078f, 0.5019608f, 0.2007843f)
                )
        },
        {
            ItemType.FRIED_CHIKEN_FILLET,
            new BoardData(
                ItemType.BOARD_CHIKENFILLET,
                "PANADO FRANGO",
                new Color(0.95f, 0.8391667f, 0.285f)
                )
        },
        {
            ItemType.FRIED_CHIKEN_NUGGET,
            new BoardData(
                ItemType.BOARD_CHIKENNUGGET,
                "NUGGETS",
                new Color(0.9f, 0.4808955f, 0.09f)
                )
        },
        {
            ItemType.FRIED_FISH_FILLET,
            new BoardData(
                ItemType.BOARD_FISHFILLET,
                "FILLET PEIXE",
                new Color(0.2392156f, 0.7065359f, 0.8f)
                )
        },
        {
            ItemType.FRIED_FISH_STICKS,
            new BoardData(
                ItemType.BOARD_FISHSTICKS,
                "DOURADINHOS",
                new Color(0.727338f, 0.4930579f, 0.9433962f)
                )
        },
    };

    public static BoardData GetBoardData(ItemType type)
    {
        BoardData data;
        boardDatas.TryGetValue(type, out data);
        return data;
    }
}


public struct BoardData
{
    public ItemType boardType;
    public string tag;
    public Color color;

    public BoardData(ItemType boardType, string tag, Color color)
    {
        this.boardType = boardType;
        this.tag = tag;
        this.color = color;
    }
}
