using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankManager : MonoBehaviour
{
    private static RankManager instance;
    public static RankManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<RankManager>();
            return instance;
        }
    }

    private Dictionary<Seasons, SortedList<int, GameResult>> gameResults;

    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad(this);

        gameResults = new Dictionary<Seasons, SortedList<int, GameResult>>();
        gameResults[Seasons.Spring] = new SortedList<int, GameResult>();
        gameResults[Seasons.Summer] = new SortedList<int, GameResult>();
    }

    public void RegisterGameResult(Seasons season, GameResult gameResult)
    {
        SortedList<int, GameResult> list;
        gameResults.TryGetValue(season, out list);
        list.Add(-gameResult.score, gameResult);
    }

    public int GetRank(Seasons season, int score)
    {
        return gameResults[season].IndexOfKey(-score) + 1;
    }
}
