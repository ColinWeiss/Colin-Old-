﻿using System.Runtime.Serialization;

namespace Colin.Common.UserInterfaces.Events
{
    /// <summary>
    /// 容器事件.
    /// </summary>
    [Serializable]
    [DataContract( IsReference = true, Name = "ContainerEvent" )]
    public class ContainerEvent : EventArgs
    {
        public string Name;

        public Container Container;

        public ContainerEvent( Container container )
        {
            Container = container;
        }

    }
}