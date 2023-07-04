using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colin.Modulars.UserInterfaces
{
    /// <summary>
    /// 为划分元素指定布局样式.
    /// </summary>
    public struct LayoutStyle
    {
        public bool IsHidden;

        private bool _needRefreshSizeRelative;
        private bool _needRefreshLocationRelative;

        public int Left;
        public int TotalLeft;
        private float _relativeLeft;
        public float RelativeLeft
        {
            get
            {
                return _relativeLeft;
            }
            set
            {
                _relativeLeft = value;
                _needRefreshLocationRelative = true;
            }
        }

        public int Top;
        public int TotalTop;
        private float _relativeTop;
        public float RelativeTop
        {
            get
            {
                return _relativeTop;
            }
            set
            {
                _relativeTop = value;
                _needRefreshLocationRelative = true;
            }
        }

        public Point Location => new Point(Left, Top);
        public Point TotalLocation => new Point(TotalLeft, TotalTop);
        public Vector2 LocationF => new Vector2(Left, Top);
        public Vector2 TotalLocationF => new Vector2(TotalLeft, TotalTop);

        public int Width;
        private float _relativeWidth;
        public float RelativeWidth
        {
            get
            {
                return _relativeWidth;
            }
            set
            {
                _relativeWidth = value;
                _needRefreshSizeRelative = true;
            }
        }

        public int Height;
        private float _relativeHeight;
        public float RelativeHeight
        {
            get
            {
                return _relativeHeight;
            }
            set
            {
                _relativeHeight = value;
                _needRefreshSizeRelative = true;
            }
        }

        public Point Size => new Point(Width, Height);
        public Vector2 SizeF => new Vector2(Width, Height);

        public Rectangle DefaultRect => new Rectangle(Left, Top, Width, Height);
        public Rectangle DefaultTotalRect => new Rectangle(TotalLeft, TotalTop, Width, Height);

        public Matrix CanvasTransform =>
                Matrix.CreateTranslation(new Vector3(-new Vector2(TotalLeft, TotalTop), 0f)) *
                Matrix.CreateScale(1f) *
                Matrix.CreateRotationZ(0f) *
                Matrix.CreateTranslation(new Vector3(Vector2.Zero, 0f));

        /// <summary>
        /// 事件: 发生在元素大小发生变化时
        /// </summary>
        public event Action OnSizeChanged;

        /// <summary>
        /// 事件: 发生在元素位置发生变化时.
        /// </summary>
        public event Action OnLocationChanged;

        /// <summary>
        /// 对样式进行计算.
        /// </summary>
        /// <param name="parent"></param>
        public void Calculation(LayoutStyle parent)
        {
            TotalLeft = parent.TotalLeft + Left;
            TotalTop = parent.TotalTop + Top;
            if (_needRefreshSizeRelative)
            {
                Width = (int)(parent.Width * RelativeWidth);
                Height = (int)(parent.Height * RelativeHeight);
                OnSizeChanged?.Invoke();
                _needRefreshSizeRelative = false;
            }
            if (_needRefreshLocationRelative)
            {
                Left = (int)(parent.Left * RelativeLeft);
                Top = (int)(parent.Top * RelativeTop);
                OnLocationChanged?.Invoke();
                _needRefreshLocationRelative = false;
            }
        }
    }
}