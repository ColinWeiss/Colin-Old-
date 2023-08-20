﻿using Colin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colin.Common
{
    /// <summary>
    /// 场景管理器.
    /// </summary>
    public sealed class SceneManager : ISingleton
    {
        public SceneManager( ) { }

        public Dictionary<Type, Scene> Scenes = new Dictionary<Type, Scene>( );

        public T CreateScene<T>( ) where T : Scene, new()
        {
            T scene = new T( );
            if( !Scenes.ContainsKey( typeof( T ) ) )
                Scenes.Add( typeof( T ), scene );
            else
            {
                Scenes.Remove( typeof( T ) );
                Scenes.Add( typeof( T ), scene );
            }
            return scene;
        }
        public void SetScene<T>( ) where T : Scene, new()
        {
            if( Scenes.TryGetValue( typeof( T ), out Scene gotScene ) )
                EngineInfo.Engine.SetScene( gotScene );
            else
                EngineInfo.Engine.SetScene( CreateScene<T>( ) );
        }
    }
}