Shader "Hidden/DummyShader"
{
	Properties
	{

	}
	SubShader
	{
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			struct appdata
			{

			};

			struct v2f
			{

			};
			
			v2f vert (appdata v)
			{
				v2f o;
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				return 0;
			}
			ENDCG
		}
	}
}
