using UnityEngine;
using UnityEngine.Profiling;
using Unity.Profiling;

// Q5: On-screen FPS / frame time / memory display. Press F1 to print a line you can copy into the table.
public class PerformanceLogger : MonoBehaviour
{
    float timer, frames, fps, ms;
    ProfilerRecorder drawCalls, batches, setPass;

    void OnEnable()
    {
        drawCalls = ProfilerRecorder.StartNew(ProfilerCategory.Render, "Draw Calls Count");
        batches   = ProfilerRecorder.StartNew(ProfilerCategory.Render, "Batches Count");
    }
    void OnDisable() { drawCalls.Dispose(); batches.Dispose(); }

    void Update()
    {
        timer += Time.unscaledDeltaTime; frames++;
        if (timer >= 0.5f) { fps = frames / timer; ms = 1000f / fps; timer = 0; frames = 0; }
        if (UnityEngine.InputSystem.Keyboard.current.f1Key.wasPressedThisFrame)
            Debug.Log($"[PERF] FPS={fps:F1} Frame={ms:F2}ms Mem(total)={Profiler.GetTotalAllocatedMemoryLong()/1048576f:F1}MB " +
                      $"Mono={Profiler.GetMonoUsedSizeLong()/1048576f:F1}MB DrawCalls={drawCalls.LastValue}");
    }

    void OnGUI()
    {
        GUI.Label(new Rect(10, 40, 400, 25), $"FPS: {fps:F1}  ({ms:F2} ms)");
        GUI.Label(new Rect(10, 60, 400, 25), $"Memory: {Profiler.GetTotalAllocatedMemoryLong()/1048576f:F1} MB  Draw calls: {drawCalls.LastValue}");
    }
}
