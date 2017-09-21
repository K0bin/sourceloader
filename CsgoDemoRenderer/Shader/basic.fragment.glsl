#version 450 core
out vec4 color;

in vec4 _position;
in vec4 _normal;

void main() {
	vec4 lightDir = vec4(0.1,-1,0,1);

	//float light = dot(_normal, lightDir);
	float light = dot(lightDir, _normal);
	color = vec4(light, light, light, 1.0);

	//color = vec4(0.5, 1.0, 0.0, 1.0);
	//color = _position;
	//color = vec4(0.5, 1.0, _position.z / 10, 1.0);
	//gl_fragColor = vec4(0.5, 1.0, 0.0, 1.0);
}
