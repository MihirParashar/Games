// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Hidden/BOXOPHOBIC/Texture Packer Blit"
{
	Properties
	{
		[HideInInspector]_MainTex("Dummy Texture", 2D) = "white" {}
		[NoScaleOffset]_Packer_TexR("Packer_TexR", 2D) = "white" {}
		[NoScaleOffset]_Packer_TexG("Packer_TexG", 2D) = "white" {}
		[NoScaleOffset]_Packer_TexB("Packer_TexB", 2D) = "white" {}
		[NoScaleOffset]_Packer_TexA("Packer_TexA", 2D) = "white" {}
		[Space(10)]_Packer_FloatR("Packer_FloatR", Range( 0 , 1)) = 0
		_Packer_FloatG("Packer_FloatG", Range( 0 , 1)) = 0
		_Packer_FloatB("Packer_FloatB", Range( 0 , 1)) = 0
		_Packer_FloatA("Packer_FloatA", Range( 0 , 1)) = 0
		[IntRange][Space(10)]_Packer_ChannelR("Packer_ChannelR", Range( 0 , 4)) = 0
		[IntRange]_Packer_ChannelG("Packer_ChannelG", Range( 0 , 4)) = 0
		[IntRange]_Packer_ChannelB("Packer_ChannelB", Range( 0 , 4)) = 0
		[IntRange]_Packer_ChannelA("Packer_ChannelA", Range( 0 , 4)) = 0
		[Space(10)]_Packer_TexR_Storage("Packer_TexR_Storage", Float) = 0
		_Packer_TexG_Storage("Packer_TexG_Storage", Float) = 0
		_Packer_TexB_Storage("Packer_TexB_Storage", Float) = 0
		_Packer_TexA_Storage("Packer_TexA_Storage", Float) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}

	}
	
	SubShader
	{
		
		
		Tags { "RenderType"="Opaque" "PreviewType"="Plane" }
	LOD 0

		CGINCLUDE
		#pragma target 3.0
		ENDCG
		Blend Off
		Cull Back
		ColorMask RGBA
		ZWrite On
		ZTest LEqual
		Offset 0 , 0
		
		
		
		Pass
		{
			Name "Unlit"
			Tags { "LightMode"="ForwardBase" }
			CGPROGRAM

			

			#ifndef UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX
			//only defining to not throw compilation error over Unity 5.5
			#define UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input)
			#endif
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_instancing
			#include "UnityCG.cginc"
			

			struct appdata
			{
				float4 vertex : POSITION;
				float4 color : COLOR;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				float4 ase_texcoord : TEXCOORD0;
			};
			
			struct v2f
			{
				float4 vertex : SV_POSITION;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
				float4 ase_texcoord : TEXCOORD0;
			};

			uniform sampler2D _MainTex;
			uniform float _Packer_TexR_Storage;
			uniform float _Packer_ChannelR;
			uniform float _Packer_FloatR;
			uniform sampler2D _Packer_TexR;
			uniform float _Packer_TexG_Storage;
			uniform float _Packer_ChannelG;
			uniform float _Packer_FloatG;
			uniform sampler2D _Packer_TexG;
			uniform float _Packer_TexB_Storage;
			uniform float _Packer_ChannelB;
			uniform float _Packer_FloatB;
			uniform sampler2D _Packer_TexB;
			uniform float _Packer_TexA_Storage;
			uniform float _Packer_ChannelA;
			uniform float _Packer_FloatA;
			uniform sampler2D _Packer_TexA;
			inline float GammaToLinearFloat100_g21( float value )
			{
				return GammaToLinearSpaceExact(value);
			}
			
			inline float LinearToGammaFloat102_g21( float value )
			{
				return LinearToGammaSpaceExact(value);
			}
			
			inline float GammaToLinearFloat100_g19( float value )
			{
				return GammaToLinearSpaceExact(value);
			}
			
			inline float LinearToGammaFloat102_g19( float value )
			{
				return LinearToGammaSpaceExact(value);
			}
			
			inline float GammaToLinearFloat100_g20( float value )
			{
				return GammaToLinearSpaceExact(value);
			}
			
			inline float LinearToGammaFloat102_g20( float value )
			{
				return LinearToGammaSpaceExact(value);
			}
			
			inline float GammaToLinearFloat100_g22( float value )
			{
				return GammaToLinearSpaceExact(value);
			}
			
			inline float LinearToGammaFloat102_g22( float value )
			{
				return LinearToGammaSpaceExact(value);
			}
			

			
			v2f vert ( appdata v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				UNITY_TRANSFER_INSTANCE_ID(v, o);

				o.ase_texcoord.xy = v.ase_texcoord.xy;
				
				//setting value to unused interpolator channels and avoid initialization warnings
				o.ase_texcoord.zw = 0;
				float3 vertexValue = float3(0, 0, 0);
				#if ASE_ABSOLUTE_VERTEX_POS
				vertexValue = v.vertex.xyz;
				#endif
				vertexValue = vertexValue;
				#if ASE_ABSOLUTE_VERTEX_POS
				v.vertex.xyz = vertexValue;
				#else
				v.vertex.xyz += vertexValue;
				#endif
				o.vertex = UnityObjectToClipPos(v.vertex);
				return o;
			}
			
			fixed4 frag (v2f i ) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID(i);
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i);
				fixed4 finalColor;
				int Storage114_g21 = (int)_Packer_TexR_Storage;
				int Channel31_g21 = (int)_Packer_ChannelR;
				float ifLocalVar24_g21 = 0;
				if( Channel31_g21 == 0 )
				ifLocalVar24_g21 = _Packer_FloatR;
				float2 uv_Packer_TexR26 = i.ase_texcoord.xy;
				float4 break23_g21 = tex2D( _Packer_TexR, uv_Packer_TexR26 );
				float R39_g21 = break23_g21.r;
				float ifLocalVar13_g21 = 0;
				if( Channel31_g21 == 1 )
				ifLocalVar13_g21 = R39_g21;
				float G40_g21 = break23_g21.g;
				float ifLocalVar12_g21 = 0;
				if( Channel31_g21 == 2 )
				ifLocalVar12_g21 = G40_g21;
				float B41_g21 = break23_g21.b;
				float ifLocalVar11_g21 = 0;
				if( Channel31_g21 == 3 )
				ifLocalVar11_g21 = B41_g21;
				float A42_g21 = break23_g21.a;
				float ifLocalVar17_g21 = 0;
				if( Channel31_g21 == 4 )
				ifLocalVar17_g21 = A42_g21;
				float GRAY135_g21 = ( ( R39_g21 * 0.3 ) + ( G40_g21 * 0.59 ) + ( B41_g21 * 0.11 ) );
				float ifLocalVar62_g21 = 0;
				if( Channel31_g21 == 555 )
				ifLocalVar62_g21 = GRAY135_g21;
				float ifLocalVar154_g21 = 0;
				if( Channel31_g21 == 14 )
				ifLocalVar154_g21 = ( R39_g21 * A42_g21 );
				float ifLocalVar159_g21 = 0;
				if( Channel31_g21 == 24 )
				ifLocalVar159_g21 = ( G40_g21 * A42_g21 );
				float ifLocalVar165_g21 = 0;
				if( Channel31_g21 == 34 )
				ifLocalVar165_g21 = ( B41_g21 * A42_g21 );
				float ifLocalVar147_g21 = 0;
				if( Channel31_g21 == 5554 )
				ifLocalVar147_g21 = ( GRAY135_g21 * A42_g21 );
				float Value112_g21 = ( ifLocalVar24_g21 + ifLocalVar13_g21 + ifLocalVar12_g21 + ifLocalVar11_g21 + ifLocalVar17_g21 + ifLocalVar62_g21 + ifLocalVar154_g21 + ifLocalVar159_g21 + ifLocalVar165_g21 + ifLocalVar147_g21 );
				float ifLocalVar105_g21 = 0;
				if( Storage114_g21 == 0.0 )
				ifLocalVar105_g21 = Value112_g21;
				float value100_g21 = Value112_g21;
				float localGammaToLinearFloat100_g21 = GammaToLinearFloat100_g21( value100_g21 );
				float ifLocalVar93_g21 = 0;
				if( Storage114_g21 == 1.0 )
				ifLocalVar93_g21 = localGammaToLinearFloat100_g21;
				float value102_g21 = Value112_g21;
				float localLinearToGammaFloat102_g21 = LinearToGammaFloat102_g21( value102_g21 );
				float ifLocalVar107_g21 = 0;
				if( Storage114_g21 == 2.0 )
				ifLocalVar107_g21 = localLinearToGammaFloat102_g21;
				float R74 = ( ifLocalVar105_g21 + ifLocalVar93_g21 + ifLocalVar107_g21 );
				int Storage114_g19 = (int)_Packer_TexG_Storage;
				int Channel31_g19 = (int)_Packer_ChannelG;
				float ifLocalVar24_g19 = 0;
				if( Channel31_g19 == 0 )
				ifLocalVar24_g19 = _Packer_FloatG;
				float2 uv_Packer_TexG51 = i.ase_texcoord.xy;
				float4 break23_g19 = tex2D( _Packer_TexG, uv_Packer_TexG51 );
				float R39_g19 = break23_g19.r;
				float ifLocalVar13_g19 = 0;
				if( Channel31_g19 == 1 )
				ifLocalVar13_g19 = R39_g19;
				float G40_g19 = break23_g19.g;
				float ifLocalVar12_g19 = 0;
				if( Channel31_g19 == 2 )
				ifLocalVar12_g19 = G40_g19;
				float B41_g19 = break23_g19.b;
				float ifLocalVar11_g19 = 0;
				if( Channel31_g19 == 3 )
				ifLocalVar11_g19 = B41_g19;
				float A42_g19 = break23_g19.a;
				float ifLocalVar17_g19 = 0;
				if( Channel31_g19 == 4 )
				ifLocalVar17_g19 = A42_g19;
				float GRAY135_g19 = ( ( R39_g19 * 0.3 ) + ( G40_g19 * 0.59 ) + ( B41_g19 * 0.11 ) );
				float ifLocalVar62_g19 = 0;
				if( Channel31_g19 == 555 )
				ifLocalVar62_g19 = GRAY135_g19;
				float ifLocalVar154_g19 = 0;
				if( Channel31_g19 == 14 )
				ifLocalVar154_g19 = ( R39_g19 * A42_g19 );
				float ifLocalVar159_g19 = 0;
				if( Channel31_g19 == 24 )
				ifLocalVar159_g19 = ( G40_g19 * A42_g19 );
				float ifLocalVar165_g19 = 0;
				if( Channel31_g19 == 34 )
				ifLocalVar165_g19 = ( B41_g19 * A42_g19 );
				float ifLocalVar147_g19 = 0;
				if( Channel31_g19 == 5554 )
				ifLocalVar147_g19 = ( GRAY135_g19 * A42_g19 );
				float Value112_g19 = ( ifLocalVar24_g19 + ifLocalVar13_g19 + ifLocalVar12_g19 + ifLocalVar11_g19 + ifLocalVar17_g19 + ifLocalVar62_g19 + ifLocalVar154_g19 + ifLocalVar159_g19 + ifLocalVar165_g19 + ifLocalVar147_g19 );
				float ifLocalVar105_g19 = 0;
				if( Storage114_g19 == 0.0 )
				ifLocalVar105_g19 = Value112_g19;
				float value100_g19 = Value112_g19;
				float localGammaToLinearFloat100_g19 = GammaToLinearFloat100_g19( value100_g19 );
				float ifLocalVar93_g19 = 0;
				if( Storage114_g19 == 1.0 )
				ifLocalVar93_g19 = localGammaToLinearFloat100_g19;
				float value102_g19 = Value112_g19;
				float localLinearToGammaFloat102_g19 = LinearToGammaFloat102_g19( value102_g19 );
				float ifLocalVar107_g19 = 0;
				if( Storage114_g19 == 2.0 )
				ifLocalVar107_g19 = localLinearToGammaFloat102_g19;
				float G78 = ( ifLocalVar105_g19 + ifLocalVar93_g19 + ifLocalVar107_g19 );
				int Storage114_g20 = (int)_Packer_TexB_Storage;
				int Channel31_g20 = (int)_Packer_ChannelB;
				float ifLocalVar24_g20 = 0;
				if( Channel31_g20 == 0 )
				ifLocalVar24_g20 = _Packer_FloatB;
				float2 uv_Packer_TexB57 = i.ase_texcoord.xy;
				float4 break23_g20 = tex2D( _Packer_TexB, uv_Packer_TexB57 );
				float R39_g20 = break23_g20.r;
				float ifLocalVar13_g20 = 0;
				if( Channel31_g20 == 1 )
				ifLocalVar13_g20 = R39_g20;
				float G40_g20 = break23_g20.g;
				float ifLocalVar12_g20 = 0;
				if( Channel31_g20 == 2 )
				ifLocalVar12_g20 = G40_g20;
				float B41_g20 = break23_g20.b;
				float ifLocalVar11_g20 = 0;
				if( Channel31_g20 == 3 )
				ifLocalVar11_g20 = B41_g20;
				float A42_g20 = break23_g20.a;
				float ifLocalVar17_g20 = 0;
				if( Channel31_g20 == 4 )
				ifLocalVar17_g20 = A42_g20;
				float GRAY135_g20 = ( ( R39_g20 * 0.3 ) + ( G40_g20 * 0.59 ) + ( B41_g20 * 0.11 ) );
				float ifLocalVar62_g20 = 0;
				if( Channel31_g20 == 555 )
				ifLocalVar62_g20 = GRAY135_g20;
				float ifLocalVar154_g20 = 0;
				if( Channel31_g20 == 14 )
				ifLocalVar154_g20 = ( R39_g20 * A42_g20 );
				float ifLocalVar159_g20 = 0;
				if( Channel31_g20 == 24 )
				ifLocalVar159_g20 = ( G40_g20 * A42_g20 );
				float ifLocalVar165_g20 = 0;
				if( Channel31_g20 == 34 )
				ifLocalVar165_g20 = ( B41_g20 * A42_g20 );
				float ifLocalVar147_g20 = 0;
				if( Channel31_g20 == 5554 )
				ifLocalVar147_g20 = ( GRAY135_g20 * A42_g20 );
				float Value112_g20 = ( ifLocalVar24_g20 + ifLocalVar13_g20 + ifLocalVar12_g20 + ifLocalVar11_g20 + ifLocalVar17_g20 + ifLocalVar62_g20 + ifLocalVar154_g20 + ifLocalVar159_g20 + ifLocalVar165_g20 + ifLocalVar147_g20 );
				float ifLocalVar105_g20 = 0;
				if( Storage114_g20 == 0.0 )
				ifLocalVar105_g20 = Value112_g20;
				float value100_g20 = Value112_g20;
				float localGammaToLinearFloat100_g20 = GammaToLinearFloat100_g20( value100_g20 );
				float ifLocalVar93_g20 = 0;
				if( Storage114_g20 == 1.0 )
				ifLocalVar93_g20 = localGammaToLinearFloat100_g20;
				float value102_g20 = Value112_g20;
				float localLinearToGammaFloat102_g20 = LinearToGammaFloat102_g20( value102_g20 );
				float ifLocalVar107_g20 = 0;
				if( Storage114_g20 == 2.0 )
				ifLocalVar107_g20 = localLinearToGammaFloat102_g20;
				float B79 = ( ifLocalVar105_g20 + ifLocalVar93_g20 + ifLocalVar107_g20 );
				int Storage114_g22 = (int)_Packer_TexA_Storage;
				int Channel31_g22 = (int)_Packer_ChannelA;
				float ifLocalVar24_g22 = 0;
				if( Channel31_g22 == 0 )
				ifLocalVar24_g22 = _Packer_FloatA;
				float2 uv_Packer_TexA60 = i.ase_texcoord.xy;
				float4 break23_g22 = tex2D( _Packer_TexA, uv_Packer_TexA60 );
				float R39_g22 = break23_g22.r;
				float ifLocalVar13_g22 = 0;
				if( Channel31_g22 == 1 )
				ifLocalVar13_g22 = R39_g22;
				float G40_g22 = break23_g22.g;
				float ifLocalVar12_g22 = 0;
				if( Channel31_g22 == 2 )
				ifLocalVar12_g22 = G40_g22;
				float B41_g22 = break23_g22.b;
				float ifLocalVar11_g22 = 0;
				if( Channel31_g22 == 3 )
				ifLocalVar11_g22 = B41_g22;
				float A42_g22 = break23_g22.a;
				float ifLocalVar17_g22 = 0;
				if( Channel31_g22 == 4 )
				ifLocalVar17_g22 = A42_g22;
				float GRAY135_g22 = ( ( R39_g22 * 0.3 ) + ( G40_g22 * 0.59 ) + ( B41_g22 * 0.11 ) );
				float ifLocalVar62_g22 = 0;
				if( Channel31_g22 == 555 )
				ifLocalVar62_g22 = GRAY135_g22;
				float ifLocalVar154_g22 = 0;
				if( Channel31_g22 == 14 )
				ifLocalVar154_g22 = ( R39_g22 * A42_g22 );
				float ifLocalVar159_g22 = 0;
				if( Channel31_g22 == 24 )
				ifLocalVar159_g22 = ( G40_g22 * A42_g22 );
				float ifLocalVar165_g22 = 0;
				if( Channel31_g22 == 34 )
				ifLocalVar165_g22 = ( B41_g22 * A42_g22 );
				float ifLocalVar147_g22 = 0;
				if( Channel31_g22 == 5554 )
				ifLocalVar147_g22 = ( GRAY135_g22 * A42_g22 );
				float Value112_g22 = ( ifLocalVar24_g22 + ifLocalVar13_g22 + ifLocalVar12_g22 + ifLocalVar11_g22 + ifLocalVar17_g22 + ifLocalVar62_g22 + ifLocalVar154_g22 + ifLocalVar159_g22 + ifLocalVar165_g22 + ifLocalVar147_g22 );
				float ifLocalVar105_g22 = 0;
				if( Storage114_g22 == 0.0 )
				ifLocalVar105_g22 = Value112_g22;
				float value100_g22 = Value112_g22;
				float localGammaToLinearFloat100_g22 = GammaToLinearFloat100_g22( value100_g22 );
				float ifLocalVar93_g22 = 0;
				if( Storage114_g22 == 1.0 )
				ifLocalVar93_g22 = localGammaToLinearFloat100_g22;
				float value102_g22 = Value112_g22;
				float localLinearToGammaFloat102_g22 = LinearToGammaFloat102_g22( value102_g22 );
				float ifLocalVar107_g22 = 0;
				if( Storage114_g22 == 2.0 )
				ifLocalVar107_g22 = localLinearToGammaFloat102_g22;
				float A80 = ( ifLocalVar105_g22 + ifLocalVar93_g22 + ifLocalVar107_g22 );
				float4 appendResult48 = (float4(R74 , G78 , B79 , A80));
				
				
				finalColor = appendResult48;
				return finalColor;
			}
			ENDCG
		}
	}
	
	
	
}
/*ASEBEGIN
Version=17401
1927;7;1906;1014;923.8132;299.5258;1;True;False
Node;AmplifyShaderEditor.RangedFloatNode;59;2176,192;Float;False;Property;_Packer_FloatB;Packer_FloatB;7;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;64;3328,192;Float;False;Property;_Packer_FloatA;Packer_FloatA;8;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;26;-128,0;Inherit;True;Property;_Packer_TexR;Packer_TexR;1;1;[NoScaleOffset];Create;True;0;0;False;0;-1;None;None;True;0;False;white;Auto;False;Object;-1;MipLevel;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;60;3328,0;Inherit;True;Property;_Packer_TexA;Packer_TexA;4;1;[NoScaleOffset];Create;True;0;0;False;0;-1;None;None;True;0;False;white;Auto;False;Object;-1;MipLevel;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;47;-128,192;Float;False;Property;_Packer_FloatR;Packer_FloatR;5;0;Create;True;0;0;False;1;Space(10);0;0.519;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;67;2176,272;Float;False;Property;_Packer_ChannelB;Packer_ChannelB;11;1;[IntRange];Create;True;0;0;False;0;0;3;0;4;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;140;2176,352;Float;False;Property;_Packer_TexB_Storage;Packer_TexB_Storage;15;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;51;1024,0;Inherit;True;Property;_Packer_TexG;Packer_TexG;2;1;[NoScaleOffset];Create;True;0;0;False;0;-1;None;None;True;0;False;white;Auto;False;Object;-1;MipLevel;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;138;1024,352;Float;False;Property;_Packer_TexG_Storage;Packer_TexG_Storage;14;0;Create;True;0;0;False;0;0;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;68;3328,272;Float;False;Property;_Packer_ChannelA;Packer_ChannelA;12;1;[IntRange];Create;True;0;0;False;0;0;4;0;4;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;50;1024,192;Float;False;Property;_Packer_FloatG;Packer_FloatG;6;0;Create;True;0;0;False;0;0;0.356;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;57;2176,0;Inherit;True;Property;_Packer_TexB;Packer_TexB;3;1;[NoScaleOffset];Create;True;0;0;False;0;-1;None;None;True;0;False;white;Auto;False;Object;-1;MipLevel;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;66;1024,272;Float;False;Property;_Packer_ChannelG;Packer_ChannelG;10;1;[IntRange];Create;True;0;0;False;0;0;2;0;4;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;72;-128,352;Float;False;Property;_Packer_TexR_Storage;Packer_TexR_Storage;13;0;Create;True;0;0;False;1;Space(10);0;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;142;3328,352;Float;False;Property;_Packer_TexA_Storage;Packer_TexA_Storage;16;0;Create;True;0;0;False;0;0;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;65;-128,272;Float;False;Property;_Packer_ChannelR;Packer_ChannelR;9;1;[IntRange];Create;True;0;0;False;1;Space(10);0;1;0;4;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;241;384,0;Inherit;False;Packer Handle Channel;-1;;21;e76e01ea35349c34d9155714d95a561c;0;4;19;COLOR;0,0,0,0;False;25;FLOAT;0;False;10;INT;0;False;56;INT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;243;3840,0;Inherit;False;Packer Handle Channel;-1;;22;e76e01ea35349c34d9155714d95a561c;0;4;19;COLOR;0,0,0,0;False;25;FLOAT;0;False;10;INT;0;False;56;INT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;240;1536,0;Inherit;False;Packer Handle Channel;-1;;19;e76e01ea35349c34d9155714d95a561c;0;4;19;COLOR;0,0,0,0;False;25;FLOAT;0;False;10;INT;0;False;56;INT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;242;2688,0;Inherit;False;Packer Handle Channel;-1;;20;e76e01ea35349c34d9155714d95a561c;0;4;19;COLOR;0,0,0,0;False;25;FLOAT;0;False;10;INT;0;False;56;INT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;74;704,0;Float;False;R;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;80;4160,0;Float;False;A;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;79;3008,0;Float;False;B;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;78;1856,0;Float;False;G;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;146;-128,928;Inherit;False;79;B;1;0;OBJECT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;145;-128,1008;Inherit;False;80;A;1;0;OBJECT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;143;-128,768;Inherit;False;74;R;1;0;OBJECT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;144;-128,848;Inherit;False;78;G;1;0;OBJECT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;155;4480,0;Inherit;True;Property;_MainTex;Dummy Texture;0;1;[HideInInspector];Create;False;0;0;True;0;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;48;128,768;Inherit;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;0;320,768;Float;False;True;-1;2;;0;1;Hidden/BOXOPHOBIC/Texture Packer Blit;0770190933193b94aaa3065e307002fa;True;Unlit;0;0;Unlit;2;True;0;1;False;-1;0;False;-1;0;1;False;-1;0;False;-1;True;0;False;-1;0;False;-1;True;False;True;0;False;-1;True;True;True;True;True;0;False;-1;True;False;255;False;-1;255;False;-1;255;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;True;1;False;-1;True;3;False;-1;True;True;0;False;-1;0;False;-1;True;2;RenderType=Opaque=RenderType;PreviewType=Plane;True;2;0;False;False;False;False;False;False;False;False;False;True;1;LightMode=ForwardBase;False;0;;0;0;Standard;1;Vertex Position,InvertActionOnDeselection;1;0;1;True;False;;0
WireConnection;241;19;26;0
WireConnection;241;25;47;0
WireConnection;241;10;65;0
WireConnection;241;56;72;0
WireConnection;243;19;60;0
WireConnection;243;25;64;0
WireConnection;243;10;68;0
WireConnection;243;56;142;0
WireConnection;240;19;51;0
WireConnection;240;25;50;0
WireConnection;240;10;66;0
WireConnection;240;56;138;0
WireConnection;242;19;57;0
WireConnection;242;25;59;0
WireConnection;242;10;67;0
WireConnection;242;56;140;0
WireConnection;74;0;241;0
WireConnection;80;0;243;0
WireConnection;79;0;242;0
WireConnection;78;0;240;0
WireConnection;48;0;143;0
WireConnection;48;1;144;0
WireConnection;48;2;146;0
WireConnection;48;3;145;0
WireConnection;0;0;48;0
ASEEND*/
//CHKSM=A4273CCF6449746F01CC9E77526C4623284BF6F6