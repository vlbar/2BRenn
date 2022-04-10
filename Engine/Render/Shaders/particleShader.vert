#version 330 core

layout (location = 0) in vec4 aVertexPos; // <vec2 position, vec2 texCoords>
layout (location = 1) in vec2 aRotationSize; // <float angle, float size>
layout (location = 2) in vec3 aOffset;
layout (location = 3) in vec4 aBaseColor;

out vec2 uv;
out vec4 color;

uniform mat4 view;
uniform mat4 projection;

void main() {
	uv = aVertexPos.zw;
	color = aBaseColor;

	mat4 rotationZ = mat4(cos(aRotationSize.x), -sin(aRotationSize.x), 0.0, 0.0,
						  sin(aRotationSize.x),   cos(aRotationSize.x), 0.0, 0.0,
						  0.0, 0.0, 1.0, 0.0,
					      0.0, 0.0, 0.0, 1.0);

	mat4 translate = mat4(1.0, 0.0, 0.0, aOffset.x,
					      0.0, 1.0, 0.0, aOffset.y,
					      0.0, 0.0, 1.0, aOffset.z,
					      0.0, 0.0, 0.0, 1.0);

	mat4 modelView = translate * view;
  
	modelView[0][0] = 1.0; 
	modelView[0][1] = 0.0; 
	modelView[0][2] = 0.0; 

	modelView[1][0] = 0.0; 
	modelView[1][1] = 1.0; 
	modelView[1][2] = 0.0; 

	modelView[2][0] = 0.0; 
	modelView[2][1] = 0.0; 
	modelView[2][2] = 1.0;

	gl_Position = vec4(vec3(aVertexPos.xy, 0.0) * aRotationSize.y, 1.0) * rotationZ * modelView * projection;
}
