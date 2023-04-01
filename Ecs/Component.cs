using Colin.Common.Physics.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Colin.Common
{
    public class Component
    {
        /// <summary>
        /// 指示组件绑定的实体.
        /// </summary>
        public Entity Entity;

        /// <summary>
        /// 获取实体的 <see cref="Entity.Transform"/>.
        /// </summary>
        public Transform2D Transform
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => Entity.Transform;
        }

        bool _enabled = true;
        public bool Enable
        {
            get => Entity != null ? Entity.Enable && _enabled : _enabled;
            set => SetEnabled(value);
        }

        /// <summary>
        /// 发生在组件启用时.
        /// </summary>
        public virtual void OnEnabled() { }

        /// <summary>
        /// 发生在组件禁用时.
        /// </summary>
        public virtual void OnDisabled() { }

        public Component SetEnabled(bool isEnabled)
        {
            if (_enabled != isEnabled)
            {
                _enabled = isEnabled;

                if (_enabled)
                    OnEnabled();
                else
                    OnDisabled();
            }

            return this;
        }

    }
}