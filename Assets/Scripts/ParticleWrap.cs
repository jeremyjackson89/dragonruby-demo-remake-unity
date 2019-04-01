using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleWrap : MonoBehaviour {
    public float distanceY = 5.5f;
    public float distanceX = 9.5f;
    public ParticleSystem particleSystem;
    ParticleSystem.Particle[] particles;

    void LateUpdate() {
        InitializeIfNeeded();
        int liveParticles = particleSystem.GetParticles(particles);
        for (int i = 0; i < liveParticles; i++) {
            if (particles[i].position.x > distanceX) {
                particles[i].position = new Vector3(-distanceX, particles[i].position.y, particles[i].position.z);
            }
            if (particles[i].position.x < -distanceX) {
                particles[i].position = new Vector3(distanceX, particles[i].position.y, particles[i].position.z);
            }
            if (particles[i].position.y > distanceY) {
                particles[i].position = new Vector3(particles[i].position.x, -distanceY, particles[i].position.z);
            }
            if (particles[i].position.y < -distanceY) {
                particles[i].position = new Vector3(particles[i].position.x, distanceY, particles[i].position.z);
            }
        }
        particleSystem.SetParticles(particles, liveParticles);
    }

    void InitializeIfNeeded() {
        if (particles == null || particles.Length < particleSystem.main.maxParticles) {
            particles = new ParticleSystem.Particle[particleSystem.main.maxParticles];
        }
    }
}