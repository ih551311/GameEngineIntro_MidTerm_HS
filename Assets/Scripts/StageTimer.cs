using UnityEngine;

public class StageTimer : MonoBehaviour
{
    [Header("디버그")]
    public bool showDebugLog = false;

    public float ElapsedTime { get; private set; }
    private float startTime;
    private bool isRunning = false;

    /// <summary>
    /// 타이머 시작 (스테이지 시작 시 호출)
    /// </summary>
    public void StartTimer()
    {
        startTime = Time.time;
        isRunning = true;
        ElapsedTime = 0f;

        if (showDebugLog)
            Debug.Log("[StageTimer] 타이머 시작");
    }

    /// <summary>
    /// 타이머 정지 (골인 지점에서 호출)
    /// </summary>
    public void StopTimer()
    {
        if (isRunning)
        {
            ElapsedTime = Time.time - startTime;
            isRunning = false;

            if (showDebugLog)
                Debug.Log($"[StageTimer] 타이머 정지 - 경과 시간: {ElapsedTime:F2}초");
        }
    }

    private void Update()
    {
        if (isRunning)
        {
            ElapsedTime = Time.time - startTime;
        }
    }

    /// <summary>
    /// 현재 경과 시간을 mm:ss.xx 형식으로 반환
    /// </summary>
    public string GetFormattedTime()
    {
        int minutes = Mathf.FloorToInt(ElapsedTime / 60f);
        float seconds = ElapsedTime % 60f;
        return $"{minutes:00}:{seconds:00.00}";
    }
}