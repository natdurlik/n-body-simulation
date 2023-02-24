using System.Collections.ObjectModel;

namespace NBodyProblemSimulation;

public class Universe
{
    public ObservableCollection<CelestialObject> CelestialObjects { get; set; }
    public float G;
    public float dt;

    public static Universe GetSampleUniverse()
    {
        var universe = new Universe();
        universe.G = 1;
        universe.dt = 1f;
        universe.CelestialObjects = new ObservableCollection<CelestialObject>
        {
            new("Earth", 45, 23.8f, 20),
            new("Mars", 100.5f, 200, 30.2f),
            new("Venus", 300.5f, 100, 25.2f)
        };
        return universe;
    }
    
    public static Universe GetSampleSolarSystem()
    {
        var universe = new Universe();
        universe.G = 1;
        universe.dt = 1f;
        universe.CelestialObjects = new ObservableCollection<CelestialObject>
        {
            new("Sun", 400, 400, 2000),
            new("Earth", 400, 500,0.6f,0, 40),
        };
        return universe;
    }
    
    public static Universe GetSolarSystem()
    {
        var universe = new Universe();
        universe.G = 6.67e-7f;
        universe.dt = 10f;
        universe.CelestialObjects = new ObservableCollection<CelestialObject>
        {
            new("Sun", 400, 400, 2e7f),
            new("Earth", 400f, 400+150, 0.3f, 0, 60f),
            new("Mars", 400f, 400+150+121, 0.2f, 0, 6f)
            // new("oof", 100, 10, 6.67e-15f)
        };
        return universe;
    }
    
    public static Universe GetRealSolarSystem()
    {
        var universe = new Universe();
        universe.G = 9.66e-30f;
        universe.dt = 3.15e7f;
        universe.CelestialObjects = new ObservableCollection<CelestialObject>
        {
            new("Sun", 400, 400, 1.989e7f),
            new("Earth", 400f, 400+150, 9.5e-13f, 0, 59.72f)
            // new("oof", 100, 10, 6.67e-15f)
        };
        return universe;
    }
}