using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Windows;

namespace NBodyProblemSimulation;

public class CelestialObject : INotifyPropertyChanged
{
    public CelestialObject(string name, float x, float y, float mass)
    {
        _name = name;
        Location = new Vector2(x, y);
        Mass = mass;
        Velocity = new Vector2(0, 0);
        Acceleration = new Vector2(0, 0);
    }
    
    public CelestialObject(string name, float x, float y, float vx, float vy, float mass)
    {
        _name = name;
        Location = new Vector2(x, y);
        Velocity = new Vector2(vx, vy);
        Acceleration = new Vector2(0, 0);
        Mass = mass;
    }
    
    private string _name;  
    public string Name {  
        get => _name;
        set {  
            _name = value;  
            OnPropertyChanged();  
        }  
    }

    private Vector2 _location;

    public Vector2 Location
    {
        get => _location;
        set
        {
            _location = value;
            OnPropertyChanged(nameof(X));
            OnPropertyChanged(nameof(Y));
        }
    }

    private Vector2 _velocity;
    public Vector2 Velocity 
    {
        get => _velocity;
        set
        {
            _velocity = value;
            OnPropertyChanged(nameof(VX));
            OnPropertyChanged(nameof(VY));
        }
    }
    public Vector2 Acceleration { get; set; }
  
    public float X {  
        get => _location.X;
        set
        {
            _location = _location with { X = value };
            OnPropertyChanged();
        }  
    }  
    
    public float Y {  
        get => _location.Y;
        set {  
            _location = _location with { Y = value };
            OnPropertyChanged();
        }  
    } 
    
    public float VX {  
        get => _velocity.X;
        set
        {
            _velocity = _velocity with { X = value };
            OnPropertyChanged();
        }  
    }  
    
    public float VY {  
        get => _velocity.Y;
        set {  
            _velocity = _velocity with { Y = value };
            OnPropertyChanged();
        }  
    }  
    
    private float _mass;  
    public float Mass {  
        get {  
            return _mass;  
        }  
        set {  
            _mass = value;  
            OnPropertyChanged();
            OnPropertyChanged(nameof(Radius));  
        }  
    }

    public float Radius => Single.Log2(Mass)+5;

    //
    // public event PropertyChangedEventHandler PropertyChanged;  
    // private void OnPropertyRaised(string propertyname) {  
    //     if (PropertyChanged != null) {  
    //         PropertyChanged(this, new PropertyChangedEventArgs(propertyname));  
    //     }  
    // }  
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}