using System;
using System.Numerics;
using System.Threading.Tasks;

namespace NBodyProblemSimulation;

public class Simulation
{
    public readonly Universe Universe;
    private ISimulationAlgorithmStrategy _algorithm;
    private ISimulationAlgorithmStrategy _algorithm3d;
    private ISimulationAlgorithmStrategy _algorithm2d;
    public bool Running { get; set; }

    public Simulation()
    {
        Universe = Universe.GetSolarSystem();
        _algorithm = new Algorithm3D();
        _algorithm3d = _algorithm;
        _algorithm2d = new Algorithm2D();
        Running = false;
    }

    public async void Simulate()
    {
        int fps = 10;
        while(Running)
        {
            _algorithm.Execute(Universe);
            await Task.Delay(fps);
        }
    }

    public void Set2DGravity()
    {
        _algorithm = _algorithm2d;
    }
    
    public void Set3DGravity()
    {
        _algorithm = _algorithm3d;
    }

    public void Reset()
    {
        Running = false;
        Universe.CelestialObjects = Universe.GetSolarSystem().CelestialObjects;
    }
}