Shader "Custom/VerticalRotatedSkybox" {
    Properties {
        _Tex ("Cubemap", Cube) = "" { }
        _Rotation ("Rotation", Range(0, 360)) = 0
        _VerticalRotation ("Vertical Rotation", Range(0, 360)) = 0
    }
    SubShader {
        Tags {"Queue" = "Background" }
        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t {
                float4 vertex : POSITION;
            };

            struct v2f {
                float4 vertex : SV_POSITION;
                float3 texcoord : TEXCOORD0;
            };

            samplerCUBE _Tex;
            float _Rotation;
            float _VerticalRotation;

            v2f vert (appdata_t v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.texcoord = v.vertex.xyz;

                // Horizontal rotation
                float s = sin(_Rotation * UNITY_PI / 180.0);
                float c = cos(_Rotation * UNITY_PI / 180.0);
                float3x3 rotMatrix = float3x3(c, 0, -s, 0, 1, 0, s, 0, c);

                // Vertical rotation
                float sv = sin(_VerticalRotation * UNITY_PI / 180.0);
                float cv = cos(_VerticalRotation * UNITY_PI / 180.0);
                float3x3 vertRotMatrix = float3x3(1, 0, 0, 0, cv, sv, 0, -sv, cv);

                o.texcoord = mul(rotMatrix, o.texcoord);
                o.texcoord = mul(vertRotMatrix, o.texcoord);

                return o;
            }

            half4 frag (v2f i) : SV_Target {
                return texCUBE(_Tex, i.texcoord);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
