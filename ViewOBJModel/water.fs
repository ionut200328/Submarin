#version 330 core
out vec4 FragColor;

in vec2 TexCoord;

// Function to generate a water ripple effect using texture coordinates and time
float waveEffect(vec2 uv, float time)
{
    // Frequency and amplitude of the waves
    float freq = 2.0;
    float amp = 0.1;

    // Calculate the wave effect
    float wave = sin(uv.x * freq + time) * sin(uv.y * freq + time) * amp;
    return wave;
}

void main()
{
    vec2 uv = TexCoord;
    float time = 5.0 * 0.01 * float(gl_FragCoord.x + gl_FragCoord.y);

    // Generate the water ripple effect
    float wave = waveEffect(uv, time);

    // Apply color to simulate the water effect
    vec3 color = vec3(0.0, 0.3, 0.7); // Water color
    color += vec3(0.0, wave, wave * 0.5); // Add the ripple effect

    FragColor = vec4(color, 1.0);
}