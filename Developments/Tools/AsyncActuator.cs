using System.Threading.Tasks;

namespace Colin.Developments.Tools
{
    /// <summary>
    /// 一个异步执行器.
    /// </summary>
    public class AsyncActuator
    {
        private float _progress;

        /// <summary>
        /// 指示当前任务的执行进度.
        /// <br>这个值在非异步的执行模式下没有用.</br>
        /// </summary>
        public float Progress => _progress;

        /// <summary>
        /// 执行指定加载任务.
        /// <br>[!] 异步.</br>
        /// </summary>
        /// <param name="action">带有进度标识的加载任务.</param>
        /// <param name="onCompleted">加载完成后执行.</param>
        public async void ActuateAsync( IdentProgressAction action, IdentProgressAction onCompleted )
        {
            await Task.Run( ( ) =>
            {
                action.Invoke( ref _progress );
                onCompleted.Invoke( ref _progress );
            } );
        }
    }
}