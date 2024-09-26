Shader "Custom/FOVShader"
{
    Properties
    {
        _MainColor("Main Color", Color) = (1,1,1,1)
        _ConeAngle("Cone Angle", Range(0, 180)) = 30
        _ConeDistance("Cone Distance", Range(0, 100)) = 10
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
            };

            float _ConeAngle;
            float _ConeDistance;

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                return o;
            }

            fixed4 _MainColor;

            half4 frag(v2f i) : SV_Target
            {
                float halfAngle = _ConeAngle / 2;
                float distance = length(i.pos);
                float angle = atan2(i.pos.y, i.pos.x) * (180 / 3.14159);
    
                if (angle >= -halfAngle && angle <= halfAngle && distance <= _ConeDistance)
                    return _MainColor;
               
                // Default return statement
                return float4(0, 0, 0, 0);

                return float4(0, 0, 0, 0);
            }

            
            ENDCG
        }
    }
}
