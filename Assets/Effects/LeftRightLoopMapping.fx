#if OPENGL
#define SV_POSITION POSITION
#define VS_SHADERMODEL vs_3_0
#define PS_SHADERMODEL ps_3_0
#else
#define VS_SHADERMODEL vs_4_0_level_9_1
#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

//映射图在底图上的重复次数.
//若映射图本身小于底图且重复绘制后不足以铺满底图,
//则会进行拉伸.
float2 DrawCount;

//映射图在底图上的偏移绘制量.
float2 Offset;

//映射使用的纹理.
Texture2D MappingTexture;

Texture2D SpriteTexture;

sampler2D SpriteTextureSampler = sampler_state
{
    Texture = <SpriteTexture>;
};
sampler2D MappingTextureSampler = sampler_state
{
    Texture = <MappingTexture>;
};

struct VertexShaderOutput
{
    float4 Position : SV_POSITION;
    float4 Color : COLOR0;
    float2 TextureCoordinates : TEXCOORD0;
};

float4 MainPS( VertexShaderOutput input ) : COLOR
{
    float2 uv = input.TextureCoordinates * DrawCount + Offset;
    float4 MappingColor = tex2D( MappingTextureSampler, uv );
    MappingColor = max( 0, sign( -uv.y * (uv.y - 1) ) ) * MappingColor;
    return tex2D( SpriteTextureSampler, input.TextureCoordinates ) * MappingColor;
}

technique SpriteDrawing
{
    pass P0
    {
        PixelShader = compile PS_SHADERMODEL MainPS( );
    }
};