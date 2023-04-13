﻿/* Poly2Tri
 * Copyright (c) 2009-2010, Poly2Tri Contributors
 * http://code.google.com/p/poly2tri/
 *
 * All rights reserved.
 *
 * Redistribution and use in source and binary forms, with or without modification,
 * are permitted provided that the following conditions are met:
 *
 * * Redistributions of source code must retain the above copyright notice,
 *   this list of conditions and the following disclaimer.
 * * Redistributions in binary form must reproduce the above copyright notice,
 *   this list of conditions and the following disclaimer in the documentation
 *   and/or other materials provided with the distribution.
 * * Neither the name of Poly2Tri nor the names of its contributors may be
 *   used to endorse or promote products derived from this software without specific
 *   prior written permission.
 *
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
 * "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
 * LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR
 * A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR
 * CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL,
 * EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO,
 * PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR
 * PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF
 * LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING
 * NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
 * SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 */

using Colin.Common.SceneComponents.Physics.Tools.Triangulation.Delaunay.Polygon;
using Colin.Common.SceneComponents.Physics.Utilities;

namespace Colin.Common.SceneComponents.Physics.Tools.Triangulation.Delaunay.Util
{
    internal class PolygonGenerator
    {
        private static readonly Random _rng = new Random( );

        public static Polygon.Polygon RandomCircleSweep( double scale, int vertexCount )
        {
            double radius = scale / 4;

            PolygonPoint[ ] points = new PolygonPoint[vertexCount];
            for( int i = 0; i < vertexCount; i++ )
            {
                do
                {
                    if( i % 250 == 0 )
                        radius += scale / 2 * (0.5 - _rng.NextDouble( ));
                    else if( i % 50 == 0 )
                        radius += scale / 5 * (0.5 - _rng.NextDouble( ));
                    else
                        radius += 25 * scale / vertexCount * (0.5 - _rng.NextDouble( ));

                    radius = radius > scale / 2 ? scale / 2 : radius;
                    radius = radius < scale / 10 ? scale / 10 : radius;
                } while( radius < scale / 10 || radius > scale / 2 );

                points[i] = new PolygonPoint( radius * Math.Cos( MathConstants.TwoPi * i / vertexCount ), radius * Math.Sin( MathConstants.TwoPi * i / vertexCount ) );
            }
            return new Polygon.Polygon( points );
        }

        public static Polygon.Polygon RandomCircleSweep2( double scale, int vertexCount )
        {
            double radius = scale / 4;
            PolygonPoint[ ] points = new PolygonPoint[vertexCount];

            for( int i = 0; i < vertexCount; i++ )
            {
                do
                {
                    radius += scale / 5 * (0.5 - _rng.NextDouble( ));
                    radius = radius > scale / 2 ? scale / 2 : radius;
                    radius = radius < scale / 10 ? scale / 10 : radius;
                } while( radius < scale / 10 || radius > scale / 2 );

                points[i] = new PolygonPoint( radius * Math.Cos( MathConstants.TwoPi * i / vertexCount ), radius * Math.Sin( MathConstants.TwoPi * i / vertexCount ) );
            }
            return new Polygon.Polygon( points );
        }
    }
}