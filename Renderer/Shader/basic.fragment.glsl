#version 450 core
out vec4 color;

in vec4 _position;
in vec4 _normal;
in vec2 _texCoord;
in vec2 _lightTexCoord;

uniform sampler2D tex;
uniform sampler2D light;

void main() {
	vec4 lightDir = normalize(vec4(1,-1,-1,1));

	float ambient = 0.3;
	float lightIntensity =  clamp(ambient + dot(_normal, lightDir), ambient, 1.0);
	vec4 texColor = texture(tex, _texCoord);
	vec4 lightColor = texture(light, _lightTexCoord);
	//color = vec4(texColor.x * lightColor.x, texColor.y * lightColor.y, texColor.z * lightColor.z, 1.0);
	color = texture(tex, _texCoord) * lightIntensity;

	//color = vec4(0.5, 1.0, 0.0, 1.0);
	//color = _position;
	//color = vec4(0.5, 1.0, _position.z / 10, 1.0);
	//gl_fragColor = vec4(0.5, 1.0, 0.0, 1.0);
}
