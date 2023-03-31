﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colin.Developments
{
    /// <summary>
    /// 一个数组的非常基本的二次封装, 当它达到容量时会自动扩展.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class FastList<T>
    {
        /// <summary>
        /// 直接访问后备缓冲区.
        /// <br>[!] 不要使用 <see cref="Buffer.Length"/></br>
        /// <br>[!] 使用 <see cref="FastList{T}.Length"/>.</br>
        /// </summary>
        public T[ ] Buffer;

        /// <summary>
        /// 获取缓冲区中已填充项目的长度.
        /// <br>[!] 不要自己改动它的值.</br>
        /// </summary>
        public int Length = 0;

        public FastList( int size )
        {
            Buffer = new T[size];
        }

        public FastList( ) : this( 5 )
        {
        }

        /// <summary>
        /// 尽管建议仅直接访问缓冲区, 但提供了便于访问的功能.
        /// </summary>
        /// <param name="index">索引.</param>
        public T this[int index] => Buffer[index];

        /// <summary>
        /// 清除列表并清空缓冲区中的所有项.
        /// </summary>
        public void Clear( )
        {
            Array.Clear( Buffer, 0, Length );
            Length = 0;
        }

        /// <summary>
        /// 令 <see cref="Length"/> 归零, 只是它不会使缓冲区中的所有项为 <see langword="null"/>, 在处理结构时很有用.
        /// </summary>
        public void Reset( )
        {
            Length = 0;
        }

        /// <summary>
        /// 将项目添加到列表中.
        /// </summary>
        public void Add( T item )
        {
            if( Length == Buffer.Length )
                Array.Resize( ref Buffer, Math.Max( Buffer.Length << 1, 10 ) );
            Buffer[Length++] = item;
        }

        /// <summary>
        /// 从列表中删除项目.
        /// </summary>
        /// <param name="item">要删除的项目.</param>
        public void Remove( T item )
        {
            var comp = EqualityComparer<T>.Default;
            for( var i = 0; i < Length; ++i )
            {
                if( comp.Equals( Buffer[i], item ) )
                {
                    RemoveAt( i );
                    return;
                }
            }
        }

        /// <summary>
        /// 从列表中删除给定索引处的项目.
        /// </summary>
        public void RemoveAt( int index )
        {
            Insist.IsTrue( index < Length, "Index out of range!" );

            Length--;
            if( index < Length )
                Array.Copy( Buffer, index + 1, Buffer, index, Length - index );
            Buffer[Length] = default( T );
        }

        /// <summary>
        /// 从列表中删除给定索引处的项目，但不维护列表顺序.
        /// </summary>
        /// <param name="index">索引.</param>
        public void RemoveAtWithSwap( int index )
        {
            Insist.IsTrue( index < Length, "Index out of range!" );

            Buffer[index] = Buffer[Length - 1];
            Buffer[Length - 1] = default( T );
            --Length;
        }

        /// <summary>
        /// 检查项目是否在列表中.
        /// </summary>
        /// <param name="item">要检查的项目.</param>
        public bool Contains( T item )
        {
            var comp = EqualityComparer<T>.Default;
            for( var i = 0; i < Length; ++i )
            {
                if( comp.Equals( Buffer[i], item ) )
                    return true;
            }

            return false;
        }

        /// <summary>
        /// 如果缓冲区达到最大值，则将分配更多空间来容纳.
        /// </summary>
        public void EnsureCapacity( int additionalItemCount = 1 )
        {
            if( Length + additionalItemCount >= Buffer.Length )
                Array.Resize( ref Buffer, Math.Max( Buffer.Length << 1, Length + additionalItemCount ) );
        }

        /// <summary>
        /// 添加数组中的所有项.
        /// </summary>
        /// <param name="array">数组.</param>
        public void AddRange( IEnumerable<T> array )
        {
            foreach( var item in array )
                Add( item );
        }

        /// <summary>
        /// 按长度对缓冲区中的所有项目进行排序.
        /// </summary>
        public void Sort( )
        {
            Array.Sort( Buffer, 0, Length );
        }

        /// <summary>
        /// 按长度对缓冲区中的所有项目进行排序.
        /// </summary>
        public void Sort( IComparer comparer )
        {
            Array.Sort( Buffer, 0, Length, comparer );
        }

        /// <summary>
        /// 按长度对缓冲区中的所有项目进行排序。.
        /// </summary>
        public void Sort( IComparer<T> comparer )
        {
            Array.Sort( Buffer, 0, Length, comparer );
        }

    }
}