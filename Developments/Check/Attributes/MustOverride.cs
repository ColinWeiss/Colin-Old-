namespace Colin.Developments.Check.Attributes
{
    /// <summary>
    /// 指示该方法/属性必须被重写, 否则无法通过编译.
    /// </summary>
    [AttributeUsage( AttributeTargets.Method | AttributeTargets.Property, Inherited = true )]
    public class MustOverrideAttribute : Attribute { }

    /// <summary>
    /// 指示该类内拥有必须被重写的成员.
    /// </summary>
    [AttributeUsage( AttributeTargets.Class, Inherited = true )]
    public class MustOverrideMemberClassAttribute : Attribute { }

}