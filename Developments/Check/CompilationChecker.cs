using Colin.Developments.Check.Attributes;
using System.Reflection;

namespace Colin.Developments.Check
{
    public class CompilationChecker : IProgramChecker
    {
        public void Check( )
        {
            Assembly assembly = Assembly.GetCallingAssembly( );
            var types = assembly.GetTypes( ).Where( t => t.GetCustomAttribute<MustOverrideMemberClassAttribute>( true ) != null );
            foreach( var type in types )
            {
                var methods = type.GetMethods( ).Where( m => m.GetCustomAttribute<MustOverrideAttribute>( true ) != null );
                foreach( var m in methods )
                {
                    if( m.DeclaringType.Name != type.Name )
                    {
                        throw new Exception( string.Concat( type.FullName, " 必须重写 ", m.Name, " 方法." ) );
                    }
                }
                var properties = type.GetProperties( ).Where( m => m.GetCustomAttribute<MustOverrideAttribute>( true ) != null );
                foreach( var p in properties )
                {
                    if( p.DeclaringType.Name != type.Name )
                    {
                        throw new Exception( string.Concat( type.FullName, " 必须重写 ", p.Name, " 属性." ) );
                    }
                }
            }
        }
    }
}