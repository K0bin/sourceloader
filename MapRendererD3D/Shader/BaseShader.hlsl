struct VS_IN
{
	float3 Position : POSITION; // Dem Compiler wird bekannt gegeben, was die Variable "bedeutet". Hier: Das ist eine Position
	float3 Normal: NORMAL; // Die Vertex-Normale, wird für die Beleuchtung verwendet
	float2 TexCoords: TEXCOORD; // Texturkoordinaten
};

struct PS_IN
{
	float4 Pos : SV_POSITION;
	float3 Normal : NORMAL;
	float2 Texcoord : TEXCOORD;
	//float4 WorldPos : POSITION;
};

float4x4 world;
float4x4 viewProj;

PS_IN VS(VS_IN _in)
{
	PS_IN _out;
	_out.Normal = mul(float4(_in.Normal, 0), world).xyz;
	//_out.WorldPos = mul(float4(_in.Position.x, _in.Position.y, _in.Position.z, 1), world);
	float4 worldPos = mul(float4(_in.Position, 1), world);
	_out.Pos = mul(worldPos, viewProj);
	_out.Texcoord = _in.TexCoords;
	return _out;
}

float4 PS(PS_IN In) : SV_TARGET
{
	float4 color;
	color.x = 1.0f;
	color.y = 1.0f;
	color.z = 1.0f;
	color.w = 1.0f;
	return color;
}