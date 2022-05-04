#version 330 core

layout (location = 0) in vec3 aVertexPos;

out vec3 texCoords;

uniform mat4 view;
uniform mat4 projection;

void main() {
	texCoords = aVertexPos;
	vec4 pos = projection * view * vec4(aVertexPos, 1.0);
	gl_Position = pos.xyww;
}
