using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Colin
{
    /// <summary>
    /// 提供帧时信息.
    /// </summary>
    public static class Time
    {
        /// <summary>
        ///游戏运行的总时间.
        /// </summary>
        public static float TotalTime;

        /// <summary>
        ///从上一帧到当前帧的时间增量, 按时间缩放.
        /// </summary>
        public static float DeltaTime;

        /// <summary>
        /// <see cref="DeltaTime"/> 的未缩放版本, 不受时间刻度的影响.
        /// </summary>
        public static float UnscaledDeltaTime;

        /// <summary>
        /// 次要增量, 当您需要同时缩放两个不同的增量时使用的时间.
        /// </summary>
        public static float AltDeltaTime;

        /// <summary>
        /// <see cref="DeltaTime"/> 的时间尺度.
        /// </summary>
        public static float TimeScale = 1f;

        /// <summary>
        /// <see cref="AltDeltaTime"/> 的时间尺度.
        /// </summary>
        public static float AltTimeScale = 1f;

        /// <summary>
        ///已经过的帧数.
        /// </summary>
        public static uint FrameCount;

        /// <summary>
        /// <see cref="DeltaTime"/> 的最大值, 这有助于防止在拖动游戏窗口或游戏失败时破坏物理.
        /// </summary>
        public static float MaxDeltaTime = float.MaxValue;

        internal static void Update(float dt)
        {
            if (dt > MaxDeltaTime)
                dt = MaxDeltaTime;
            TotalTime += dt;
            DeltaTime = dt * TimeScale;
            AltDeltaTime = dt * AltTimeScale;
            UnscaledDeltaTime = dt;
            FrameCount++;
        }

    }
}