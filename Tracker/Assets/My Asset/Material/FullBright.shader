Shader"Custom/FullbrightNonTransparent" 
{
    Properties 
    {
        _Color ("Main Color", Color) = (1,1,1,1)
        _MainTex ("Base (RGB)", 2D) = "white" {}
    }
    SubShader 
    {
        Tags { "Queue"="Geometry" "IgnoreProjector"="True" "RenderType"="Opaque" }
LOD 200

        CGPROGRAM
        #pragma surface surf NoLighting

float4 _Color;
sampler2D _MainTex;

struct Input
{
    float2 uv_MainTex;
};

void surf(Input IN, inout SurfaceOutput o)
{
    half4 c = tex2D(_MainTex, IN.uv_MainTex);
    o.Albedo = c.rgb * _Color.rgb;
            // Removed handling of alpha since transparency is not needed
}

fixed4 LightingNoLighting(SurfaceOutput s, fixed3 lightDir, fixed atten)
{
    fixed4 c;
    c.rgb = s.Albedo;
    c.a = 1.0;
    return c;
}
        ENDCG
    } 
FallBack"Diffuse"
}
