using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colin.Extensions
{
    public static class IOExtension
    {
        public static byte[ ] ReadBytes( this FileStream stream, int index, int length = 0 )
        {
            stream.Seek( index, SeekOrigin.Begin );
            var bytes = new byte[length == 0 ? stream.Length - index : Math.Min( stream.Length - index, length )];
            for( var i = 0; i < bytes.Length; i++ )
                bytes[i] = (byte)stream.ReadByte( );
            return bytes;
        }
    }
}