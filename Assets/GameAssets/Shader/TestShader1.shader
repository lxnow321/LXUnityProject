Shader "Test/TestShader1" {
	Properties{
		_Color("Color", Color) = (0, 0, 0, 1)
		_MainTex("Texture", 2D) = "white"{}
	}
	SubShader {

		Tags { 
			"RenderType"="Opaque" 
			"Queue" = "Geometry"
		}

		Pass{
			CGPROGRAM
			#include "UnityCG.cginc"

			fixed4 _Color;
			sampler2D _MainTex;
			float4 _MainTex_ST;


			#pragma vertex vert
			#pragma fragment frag

			
			struct appdata{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f{
				float4 position : SV_POSITION;
				float2 uv : TEXCOORD0;
			};

			v2f vert(appdata v){
				v2f o;
				o.position = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				// o.uv = v.uv;
				return o;
			}

			fixed4 frag(v2f i) : SV_TARGET{
				// return fixed4(0.5, 0.5, 0, 1);
				// return fixed4(i.position.x, i.position.y, i.position.z, 1);
				// return _Color;
				// return fixed4(i.uv.x, i.uv.y, 0, 1);
				fixed4 col = tex2D(_MainTex, i.uv);
				return col;
			}

			ENDCG
		}
	}
	FallBack "Diffuse"
}
