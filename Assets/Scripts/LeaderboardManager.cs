using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;

/// <summary>
/// 리더보드 한 개 기록
/// </summary>
[Serializable]
public class LeaderboardEntry
{
    public string playerName;
    public float clearTime;
    public string dateTime;
}

/// <summary>
/// 한 스테이지의 리더보드
/// </summary>
[Serializable]
public class StageLeaderboard
{
    public string stageId;
    public List<LeaderboardEntry> entries = new List<LeaderboardEntry>();
}

/// <summary>
/// 전체 리더보드 저장 데이터
/// </summary>
[Serializable]
public class LeaderboardSaveData
{
    public List<StageLeaderboard> stageLeaderboards = new List<StageLeaderboard>();
}

/// <summary>
/// 스테이지별 클리어 타임 리더보드 매니저 (싱글톤)
/// 점수 없이 순수 시간 기준 (짧을수록 상위)
/// </summary>
public class LeaderboardManager : MonoBehaviour
{
    public static LeaderboardManager Instance { get; private set; }

    [Header("설정")]
    [Tooltip("스테이지당 최대 저장 기록 수")]
    public int maxEntriesPerStage = 10;

    private LeaderboardSaveData saveData;
    private string savePath;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            savePath = Path.Combine(Application.persistentDataPath, "leaderboards.json");
            Load();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Load()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            saveData = JsonUtility.FromJson<LeaderboardSaveData>(json);
        }
        if (saveData == null)
            saveData = new LeaderboardSaveData();
    }

    private void Save()
    {
        string json = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(savePath, json);
    }

    /// <summary>
    /// 리더보드에 기록 추가 (클리어 타임 기준)
    /// </summary>
    public void AddEntry(string stageId, string playerName, float clearTime)
    {
        if (string.IsNullOrEmpty(playerName)) playerName = "Player";

        var stageBoard = saveData.stageLeaderboards
            .FirstOrDefault(s => s.stageId == stageId);

        if (stageBoard == null)
        {
            stageBoard = new StageLeaderboard { stageId = stageId };
            saveData.stageLeaderboards.Add(stageBoard);
        }

        var newEntry = new LeaderboardEntry
        {
            playerName = playerName,
            clearTime = clearTime,
            dateTime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm")
        };

        stageBoard.entries.Add(newEntry);

        // 정렬: 클리어 타임 짧은 순 (오름차순)
        stageBoard.entries = stageBoard.entries
            .OrderBy(e => e.clearTime)
            .Take(maxEntriesPerStage)
            .ToList();

        Save();
        Debug.Log($"[Leaderboard] {stageId} 기록 저장: {playerName} - {clearTime:F2}초");
    }

    /// <summary>
    /// 특정 스테이지 상위 N개 기록 가져오기
    /// </summary>
    public List<LeaderboardEntry> GetTopEntries(string stageId, int count = 10)
    {
        var stageBoard = saveData.stageLeaderboards
            .FirstOrDefault(s => s.stageId == stageId);
        return stageBoard?.entries.Take(count).ToList() ?? new List<LeaderboardEntry>();
    }

    /// <summary>
    /// 모든 리더보드 데이터 초기화 (테스트용)
    /// </summary>
    public void ClearAllData()
    {
        saveData = new LeaderboardSaveData();
        Save();
        Debug.Log("리더보드 전체 초기화 완료");
    }
}