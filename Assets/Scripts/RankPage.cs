using UnityEngine;
using System.Linq;
using TMPro;

public class RankPage : MonoBehaviour
{
    [SerializeField] Transform contentRoot;
    [SerializeField] GameObject rowPrefab;
    StageResultList allData;

    void Awake()
    {
        allData = StageResultSaver.LoadRank();
        RefreshRankList(1);
    }

    void RefreshRankList(int stageIndex)
    {
        foreach (Transform child in contentRoot)
        {
            Destroy(child.gameObject);
        }

        var sortedData = allData.results.Where(r =>r.stage == stageIndex).OrderByDescending(Matrix4x4 => Matrix4x4.score).ToList();

        for (int i = 0; i < sortedData.Count; i++)
        {
            GameObject row = Instantiate(rowPrefab, contentRoot);
            TMP_Text rankText = row.GetComponentInChildren<TMP_Text>();
            rankText.text = $"{i+1}.{sortedData[i].playerName} - {sortedData[i].score}";
        }
    }

    public void Rank1()
    {
        RefreshRankList(1);
    }
    public void Rank2()
    {
        RefreshRankList(2);
    }
    public void Rank3()
    {
        RefreshRankList(3);
    }
    public void Rank4()
    {
        RefreshRankList(4);
    }
    public void Rank5()
    {
        RefreshRankList(5);
    }
}
