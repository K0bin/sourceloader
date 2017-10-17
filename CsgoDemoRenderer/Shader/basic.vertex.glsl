#version 450 core
layout(location = 0)
in vec3 position;
layout(location = 1)
in vec3 normal;
layout(location = 2)
in vec2 texCoord;

uniform mat4 mvp;
uniform vec3 cameraPos;

out vec4 _position;
out vec4 _normal;
out vec2 _texCoord;

void main(void) {
	_position = vec4(position, 1.0);
	_normal = vec4(normal, 1.0);
	_texCoord = texCoord;
	gl_Position = mvp * vec4(position, 1.0);
}
