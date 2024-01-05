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
uniform sampler2D texture6;
uniform sampler2D texture7;

// Flag to enable/disable texture
uniform bool useTexture;  

void main()
{
    // Ambient lighting
float ambientStrength = 10;
vec3 ambient = ambientStrength * vec3(1.0, 1.0, 1.0);

// Diffuse lighting
vec3 norm = normalize(Normal);
vec3 lightDir = normalize(vec3(0.0, 0.0, 1.0));
float diff = max(dot(norm, lightDir), 0.0);
vec3 diffuse = diff * vec3(1.0, 1.0, 1.0);

// Specular lighting
float specularStrength = 0.5;
vec3 viewDir = normalize(vec3(0.0, 0.0, 1.0));
vec3 reflectDir = reflect(-lightDir, norm);
float spec = pow(max(dot(viewDir, reflectDir), 0.0), 32);
vec3 specular = specularStrength * spec * vec3(1.0, 1.0, 1.0);

vec3 result = (ambient + diffuse + specular) * vec3(1.0,1.0,1.0);
    // If using textures, sample the textures and apply them to FragColor
    if (useTexture) {
        vec4 textureColor1 = texture(texture1, TexCoord);
        vec4 textureColor2 = texture(texture2, TexCoord);
        vec4 textureColor3 = texture(texture3, TexCoord);
        vec4 textureColor4 = texture(texture4, TexCoord);
        vec4 textureColor5 = texture(texture5, TexCoord);
        vec4 textureColor6 = texture(texture6, TexCoord);
        vec4 textureColor7 = texture(texture7, TexCoord);

        FragColor = textureColor1 * textureColor2 * textureColor3 * textureColor4 * textureColor5 * vec4(result, 1.0);
    } else {
        // If not using textures, set a fixed color (e.g., red)
        FragColor = vec4(0.0, 0.0, 0.0, 1.0) * vec4(result, 1.0);
    }
}
