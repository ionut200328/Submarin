#version 330 core
out vec4 FragColor;

in vec3 Normal;  
in vec3 FragPos;  
in vec2 TexCoord;

// Texture samplers
uniform sampler2D texture1;
uniform sampler2D texture2;
uniform sampler2D texture3;
uniform sampler2D texture4;
uniform sampler2D texture5;

// Flag to enable/disable texture
uniform bool useTexture;  

void main()
{
    // If using textures, sample the textures and apply them to FragColor
    if (useTexture) {
        vec4 textureColor1 = texture(texture1, TexCoord);
        vec4 textureColor2 = texture(texture2, TexCoord);
        vec4 textureColor3 = texture(texture3, TexCoord);
        vec4 textureColor4 = texture(texture4, TexCoord);
        vec4 textureColor5 = texture(texture5, TexCoord);

        FragColor = textureColor1 * textureColor2 * textureColor3 * textureColor4 * textureColor5;
    } else {
        // If not using textures, set a fixed color (e.g., red)
        FragColor = vec4(1.0, 0.0, 0.0, 1.0);
    }
}
