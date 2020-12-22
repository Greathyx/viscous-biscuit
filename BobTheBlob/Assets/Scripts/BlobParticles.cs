using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobParticles {
    public BlobParticles(ParticleSystem ps){
        var shape = ps.shape;
        var emission = ps.emission;
        var main = ps.main;
        var collision = ps.collision;

        // standard settings
        main.startLifetime = 2f;
        main.startSpeed = 0.6f;
        main.startSize = 3f;

        // set particle emission shape
        shape.shapeType = ParticleSystemShapeType.Circle;

        // set emission type
        emission.rateOverTime = 6;
        
        // collision variables
        /*
        collision.enabled = true;
        collision.type = ParticleSystemCollisionType.World;
        collision.mode = ParticleSystemCollisionMode.Collision2D;
        
        collision.dampen = 0.9f;
        collision.bounce = 0.1f;
        */
    }
}
