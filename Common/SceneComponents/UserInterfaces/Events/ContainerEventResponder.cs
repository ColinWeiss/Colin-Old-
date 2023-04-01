using Microsoft.Xna.Framework.Input;
using System.Runtime.Serialization;

namespace Colin.Common.UserInterfaces.Events
{
    /// <summary>
    /// 容器事件响应器.
    /// </summary>
    [Serializable]
    [DataContract( Name = "EventResponder" )]
    public class ContainerEventResponder
    {
        /// <summary>
        /// 该响应器所绑定的容器.
        /// </summary>
        public Container Container;

        public ContainerEventResponder( Container container ) => Container = container;

        /// <summary>
        /// 事件: 发生于鼠标悬停于该容器上持续激活交互状态时.
        /// </summary>
        public EventHandler<ContainerEvent> MouseHoverOn = ( s, e ) => { };

        /// <summary>
        /// 事件: 发生于鼠标与之交互, 左键单击按下时.
        /// </summary>
        public EventHandler<ContainerEvent> MouseLeftClickBefore = ( s, e ) => { };

        /// <summary>
        /// 事件: 发生于鼠标与之交互, 左键长按时.
        /// </summary>
        public EventHandler<ContainerEvent> MouseLeftDown = ( s, e ) => { };

        /// <summary>
        /// 事件: 发生于鼠标与之交互, 左键单击抬起时.
        /// </summary>
        public EventHandler<ContainerEvent> MouseLeftClickAfter = ( s, e ) => { };

        /// <summary>
        /// 事件: 发生于鼠标与之交互, 左键松开时.
        /// </summary>
        public EventHandler<ContainerEvent> MouseLeftUp = ( s, e ) => { };

        /// <summary>
        /// 事件: 发生于鼠标与之交互, 右键单击按下时.
        /// </summary>
        public EventHandler<ContainerEvent> MouseRightClickBefore = ( s, e ) => { };

        /// <summary>
        /// 事件: 发生于鼠标与之交互, 右键长按时.
        /// </summary>
        public EventHandler<ContainerEvent> MouseRightDown = ( s, e ) => { };

        /// <summary>
        /// 事件: 发生于鼠标与之交互, 右键单击抬起时.
        /// </summary>
        public EventHandler<ContainerEvent> MouseRightClickAfter = ( s, e ) => { };

        /// <summary>
        /// 事件: 发生于鼠标与之交互, 右键松开时.
        /// </summary>
        public EventHandler<ContainerEvent> MouseRightUp = ( s, e ) => { };

        /// <summary>
        /// 事件: 发生于鼠标双键松开时.
        /// </summary>
        public EventHandler<ContainerEvent> MouseRelease = ( s, e ) => { };

        /// <summary>
        /// 事件: 发生于容器开始拖拽状态时.
        /// </summary>
        public EventHandler<ContainerEvent> DragStart = ( s, e ) => { };

        /// <summary>
        /// 事件: 发生于容器拖拽状态时.
        /// </summary>
        public EventHandler<ContainerEvent> Dragging = ( s, e ) => { };

        /// <summary>
        /// 事件: 发生于容器结束拖拽状态时.
        /// </summary>
        public EventHandler<ContainerEvent> DragEnd = ( s, e ) => { };

        /// <summary>
        /// 事件: 发生于鼠标滑轮滑动时.
        /// </summary>
        public EventHandler<ContainerEvent> MousePulleySliding = ( s, e ) => { };

        /// <summary>
        /// 事件: 发生于容器启用交互状态时.
        /// </summary>
        public EventHandler<ContainerEvent> ActivationStart = ( s, e ) => { };

        /// <summary>
        /// 事件: 发生于容器结束交互状态时.
        /// </summary>
        public EventHandler<ContainerEvent> ActivationEnd = ( s, e ) => { };

        /// <summary>
        /// 指示拖拽状态.
        /// </summary>
        public bool DraggingState = false;

        MouseState _mouseState = new MouseState( );

        MouseState _mouseStateLast = new MouseState( );

        public void UpdateEvent( )
        {
            ContainerEvent containerEvent = new ContainerEvent( Container );
            if( Container.InteractiveInfo.Activation )
            {
                if( !Container.InteractiveInfo.ActivationLast )
                {
                    containerEvent.Name = string.Concat( "Event_Container_", Container.Name, "_Activation_ActivationStart" );
                    ActivationStart.Invoke( this, containerEvent );
                }
                containerEvent.Name = string.Concat( "Event_Container_", Container.Name, "_Mouse_MouseHoverOn" );
                MouseHoverOn.Invoke( this, containerEvent );
                if( _mouseState.LeftButton == ButtonState.Pressed && _mouseStateLast.LeftButton == ButtonState.Released )
                {
                    UserInterface.CurrentFocu = Container;
                    containerEvent.Name = string.Concat( "Event_Container_", Container.Name, "_Mouse_MouseLeftClickBefore" );
                    MouseLeftClickBefore.Invoke( this, containerEvent );
                    if( Container.InteractiveInfo.CanDrag && Container.InteractiveInfo.ActivationLast )
                    {
                        containerEvent.Name = string.Concat( "Event_Container_", Container.Name, "_Mouse_DragStart" );
                        DragStart.Invoke( this, containerEvent );
                        DraggingState = true;
                    }
                }
                if( _mouseState.LeftButton == ButtonState.Pressed && _mouseStateLast.LeftButton == ButtonState.Pressed )
                {
                    containerEvent.Name = string.Concat( "Event_Container_", Container.Name, "_Mouse_MouseLeftDown" );
                    MouseLeftDown.Invoke( this, containerEvent );
                }
                if( _mouseState.LeftButton == ButtonState.Released && _mouseStateLast.LeftButton == ButtonState.Pressed )
                {
                    containerEvent.Name = string.Concat( "Event_Container_", Container.Name, "_Mouse_MouseLeftClickAfter" );
                    MouseLeftClickAfter.Invoke( this, containerEvent );
                }
                if( _mouseState.LeftButton == ButtonState.Released && _mouseStateLast.LeftButton == ButtonState.Released )
                {
                    containerEvent.Name = string.Concat( "Event_Container_", Container.Name, "_Mouse_MouseLeftUp" );
                    MouseLeftUp.Invoke( this, containerEvent );
                    DraggingState = false;
                }
                if( _mouseState.RightButton == ButtonState.Pressed && _mouseStateLast.RightButton == ButtonState.Released )
                {
                    containerEvent.Name = string.Concat( "Event_Container_", Container.Name, "_Mouse_MouseRightClickBefore" );
                    MouseRightClickBefore.Invoke( this, containerEvent );
                }
                if( _mouseState.RightButton == ButtonState.Pressed && _mouseStateLast.RightButton == ButtonState.Pressed )
                {
                    containerEvent.Name = string.Concat( "Event_Container_", Container.Name, "_Mouse_MouseRightDown" );
                    MouseRightDown.Invoke( this, containerEvent );
                }
                if( _mouseState.RightButton == ButtonState.Released && _mouseStateLast.RightButton == ButtonState.Pressed )
                {
                    containerEvent.Name = string.Concat( "Event_Container_", Container.Name, "_Mouse_MouseRightClickAfter" );
                    MouseRightClickAfter.Invoke( this, containerEvent );
                }
                if( _mouseState.RightButton == ButtonState.Released && _mouseStateLast.RightButton == ButtonState.Released )
                {
                    containerEvent.Name = string.Concat( "Event_Container_", Container.Name, "_Mouse_MouseRightUp" );
                    MouseRightUp.Invoke( this, containerEvent );
                }
                if( _mouseState.ScrollWheelValue != _mouseStateLast.ScrollWheelValue )
                {
                    containerEvent.Name = string.Concat( "Event_Container_", Container.Name, "_Mouse_MousePulleySliding" );
                    MousePulleySliding.Invoke( this, containerEvent );
                }
            }
        }

        public void UpdateIndependentEvent( )
        {
            _mouseStateLast = _mouseState;
            _mouseState = Mouse.GetState( );
            ContainerEvent containerEvent = new ContainerEvent( Container );
            if( UserInterface.CurrentFocu == Container )
                Container.InteractiveInfo.Focus = true;
            else
                Container.InteractiveInfo.Focus = false;
            if( Container.InteractiveInfo.ActivationLast )
            {
                if( !Container.InteractiveInfo.Activation )
                {
                    containerEvent.Name = string.Concat( "Event_Container_", Container.Name, "_Activation_ActivationEnd" );
                    ActivationEnd.Invoke( this, containerEvent );
                }
            }
            if( DraggingState && Container.InteractiveInfo.CanDrag )
            {
                containerEvent.Name = string.Concat( "Event_Container_", Container.Name, "_Mouse_Dragging" );
                Dragging.Invoke( this, containerEvent );
            }
            if( _mouseState.LeftButton == ButtonState.Released && Container.InteractiveInfo.CanDrag )
            {
                DraggingState = false;
                containerEvent.Name = string.Concat( "Event_Container_", Container.Name, "_Mouse_DragEnd" );
                DragEnd.Invoke( this, containerEvent );
            }
        }
    }
}