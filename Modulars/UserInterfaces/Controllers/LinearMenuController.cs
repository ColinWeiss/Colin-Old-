using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colin.Modulars.UserInterfaces.Controllers
{
    public class LinearMenuController : DivisionController
    {
        /// <summary>
        /// 指示项间隔.
        /// </summary>
        public int DivInterval = 0;
        public Direction Direction = Direction.Portrait;
        public Direction Alignment = Direction.Center;
        public Direction Toward = Direction.Down;
        public LinearMenuController( Division division ) : base( division ) { }
        public override void Layout( ref LayoutStyle layout )
        {
            Division.Layout.Width = 0;
            Division.Layout.Height = 0;
            Division.ForEach( CalculateLayout );
            if( Direction == Direction.Portrait )
                Division.ForEach( Portrait );
            else if( Direction == Direction.Transverse )
                Division.ForEach( Transverse );
            lastDiv = null;
            base.Layout( ref layout );
        }
        private Division lastDiv;
        private void CalculateLayout( Division division )
        {
            switch( Direction )
            {
                case Direction.Portrait:
                    if( Division.Layout.Width < division.Layout.Width )
                        Division.Layout.Width = division.Layout.Width;
                    Division.Layout.Height += division.Layout.Height + DivInterval;
                    break;
                case Direction.Transverse:
                    if( Division.Layout.Height < division.Layout.Height )
                        Division.Layout.Height = division.Layout.Height;
                    Division.Layout.Width += division.Layout.Width + DivInterval;
                    break;
            }
        }
        private void Portrait( Division division )
        {
            if( lastDiv != null )
            {
                switch( Toward )
                {
                    case Direction.Down:
                        division.Layout.Top = lastDiv.Layout.Top + lastDiv.Layout.Height + DivInterval;
                        break;
                    case Direction.Up:
                        division.Layout.Top = lastDiv.Layout.Top - division.Layout.Height - DivInterval;
                        break;
                }
            }
            else if( Toward == Direction.Up )
                division.Layout.Top = Division.Layout.Height - division.Layout.Height;
            switch( Alignment )
            {
                case Direction.Left:
                    division.Layout.Left = 0;
                    break;
                case Direction.Right:
                    division.Layout.Left = division.Parent.Layout.Width - division.Layout.Width;
                    break;
                case Direction.Center:
                    division.Layout.Left = Division.Layout.Width / 2 - division.Layout.Width / 2;
                    break;
            }
            lastDiv = division;
        }
        private void Transverse( Division division )
        {
            if( lastDiv != null )
            {
                switch( Toward )
                {
                    case Direction.Right:
                        division.Layout.Left = lastDiv.Layout.Left + lastDiv.Layout.Width + DivInterval;
                        break;
                    case Direction.Left:
                        division.Layout.Left = lastDiv.Layout.Left - division.Layout.Width - DivInterval;
                        break;
                }
            }
            else if( Toward == Direction.Left )
                division.Layout.Left = Division.Layout.Width - division.Layout.Width;
            switch( Alignment )
            {
                case Direction.Up:
                    division.Layout.Top = 0;
                    break;
                case Direction.Down:
                    division.Layout.Top = division.Parent.Layout.Height - division.Layout.Height;
                    break;
                case Direction.Center:
                    division.Layout.Top = Division.Layout.Height / 2 - division.Layout.Height / 2;
                    break;
            }
            lastDiv = division;
        }
    }
}