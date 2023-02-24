using System;
using System.Numerics;

namespace NBodyProblemSimulation;

public class Algorithm2D : ISimulationAlgorithmStrategy
{
    public async void Execute(Universe universe)
    {
        float softening = 0.1f;
        foreach (var celestialObject in universe.CelestialObjects)
        {
            celestialObject.Velocity += celestialObject.Acceleration * universe.dt/2.0f;
            celestialObject.Location += celestialObject.Velocity * universe.dt;
        }
        
        foreach (var celObj1 in universe.CelestialObjects)
        {
            Vector2 acceleration = Vector2.Zero;
            foreach (var celObj2 in universe.CelestialObjects)
            {
                if (celObj1 == celObj2) continue;

                Vector2 rVec = celObj2.Location - celObj1.Location;
                float invDist3 = Single.Pow(rVec.Length() + softening, -2f); // fix 1.5f
                Vector2 part = celObj2.Mass * invDist3 * rVec;
                acceleration += part;
            }

            acceleration *= universe.G;
            celObj1.Acceleration = acceleration;
        }

        foreach (var celestialObject in universe.CelestialObjects)
        {
            celestialObject.Velocity += celestialObject.Acceleration * universe.dt/2.0f;
        }
    }
}