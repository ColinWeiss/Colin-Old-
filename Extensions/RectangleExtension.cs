﻿using Microsoft.Xna.Framework.Input;

namespace Colin.Extensions
{
    public static class RectangleExtension
    {
        /// <summary>
        /// 获取矩形左边缘的中心位置.
        /// </summary>
        public static Vector2 GetLeftCenter( this Rectangle rect )
        {
            return new Vector2( rect.Left, rect.Y + rect.Height / 2.0f );
        }

        /// <summary>
        /// 获取矩形右边缘的中心位置.
        /// </summary>
        public static Vector2 GetRightCenter( this Rectangle rect )
        {
            return new Vector2( rect.Right, rect.Y + rect.Height / 2.0f );
        }

        /// <summary>
        /// 获取矩形上边缘的中心位置.
        /// </summary>
        public static Vector2 GetTopCenter( this Rectangle rect )
        {
            return new Vector2( rect.X + rect.Width / 2.0f, rect.Top );
        }

        /// <summary>
        /// 获取矩形下边缘的中心位置.
        /// </summary>
        public static Vector2 GetBottomCenter( this Rectangle rect )
        {
            return new Vector2( rect.X + rect.Width / 2.0f, rect.Bottom );
        }

        /// <summary>
        /// 判断光标是否与指定的 <seealso cref="Rectangle"/> 重合.
        /// </summary>
        /// <param name="rectangle">指定的 <seealso cref="Rectangle"/>.</param>
        /// <returns>如若是, 返回 <see href="true"/>, 否则返回 <see href="false"/>.</returns>
        public static bool IntersectMouse( this Rectangle rectangle )
        {
            return rectangle.Intersects( new Rectangle( Mouse.GetState( ).Position, Point.Zero ) );
        }

        /// <summary>
        /// 判断上一帧光标是否与指定的 <seealso cref="Rectangle"/> 重合.
        /// </summary>
        /// <param name="rectangle">指定的 <seealso cref="Rectangle"/>.</param>
        /// <returns>如若是, 返回 <see href="true"/>, 否则返回 <see href="false"/>.</returns>
        public static bool IntersectMouseLast( this Rectangle rectangle )
        {
            return rectangle.Intersects( new Rectangle( Mouse.GetState( ).Position, Point.Zero ) );
        }

    }
}