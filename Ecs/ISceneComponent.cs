﻿using System.ComponentModel;

namespace Colin.Common
{
    /// <summary>
    /// 标识一个可用于场景的模块.
    /// <para>
    /// 不需要在类内对 <see cref="Scene"/> 赋值,
    /// <br>这一操作在 <see cref="SceneComponentList"/> 加入该模块时自动实现.</br>
    /// </para>
    /// </summary>
    public interface ISceneComponent
    {
        /// <summary>
        /// 指示该模块所属的场景.
        /// </summary>
        public Scene Scene { get; set; }

        /// <summary>
        /// 指示对象是否启用逻辑计算.
        /// <br>默认启用.</br>
        /// </summary>
        public bool Enable { get; set; }

        /// <summary>
        /// 在加入 <see cref="SceneComponentList"/> 时执行初始化内容.
        /// </summary>
        public void DoInitialize( );

        /// <summary>
        /// 进行逻辑计算.
        /// </summary>
        public void DoUpdate( GameTime time );


    }
}