#version 450 core
out vec4 color;

in vec4 _position;
in vec4 _normal;
in vec2 _texCoord;
in vec2 _lightTexCoord;

uniform sampler2D tex;
uniform sampler2D light;

void main() {
	vec4 lightDir = vec4(0.1,-1,0,1);

	//float light = dot(_normal, lightDir);
	//float light = dot(lightDir, _normal);
	vec4 texColor = texture(tex, _texCoord);
	vec4 lightColor = texture(light, _lightTexCoord);
	//color = vec4(texColor.x * lightColor.x, texColor.y * lightColor.y, texColor.z * lightColor.z, 1.0);
	color = texture(tex, _texCoord);

	//color = vec4(0.5, 1.0, 0.0, 1.0);
	//color = _position;
	//color = vec4(0.5, 1.0, _position.z / 10, 1.0);
	//gl_fragColor = vec4(0.5, 1.0, 0.0, 1.0);
}
