using System;
using UnityEngine;
using UnityEngine.Perception.Randomization.Parameters;
using UnityEngine.Perception.Randomization.Randomizers;
using UnityEngine.Perception.Randomization.Samplers;

// Add this Component to any GameObject that you would like to be randomized. This class must have an identical name to
// the .cs file it is defined in.
public class CameraRandomizerTag : RandomizerTag {}

[Serializable]
[AddRandomizerMenu("Camera Randomizer")]
public class CameraRandomizer : Randomizer
{
    // Sample FloatParameter that can generate random floats in the [0,360) range. The range can be modified using the
    // Inspector UI of the Randomizer.
    public FloatParameter x_pos = new()
    {
        value = new UniformSampler(-18, 18)
    };
    public FloatParameter y_pos = new()
    {
        value = new UniformSampler(1, 20)
    };
    public FloatParameter z_pos = new()
    {
        value = new UniformSampler(13, 39)
    };
        public FloatParameter x_rot = new()
    {
        value = new UniformSampler(0, 90)
    };
    public FloatParameter y_rot = new()
    {
        value = new UniformSampler(0, 360)
    };
    public FloatParameter z_rot = new()
    {
        value = new UniformSampler(-30, 30)
    };

    protected override void OnIterationStart()
    {
        int mode = UnityEngine.Random.Range(0,3);
        Vector3 pos = new Vector3(0, 0, 0);
        Vector3 rot = new Vector3(0, 0, 0);
        
        // modes:
        // overhead (x y z pos, y rot)
        // rotation (x rot, y rot, z rot, zoom dist)
        // side (y rot, x pos, z pos)
        if (mode == 0){ // overhead view
            pos = new Vector3(x_pos.Sample(), y_pos.Sample(), z_pos.Sample());
            rot = new Vector3(90, y_rot.Sample(), 0);
        }
        else if (mode == 1){ // rotation view
            pos = new Vector3(x_pos.Sample(), y_pos.Sample(), z_pos.Sample());
            rot = new Vector3(x_rot.Sample(), y_rot.Sample(), z_rot.Sample());
        }
        else if (mode == 2){ // side view
            pos = new Vector3(x_pos.Sample(), 0.1f, z_pos.Sample());
            rot = new Vector3(0, y_rot.Sample(), 0);
        }

        var tags = tagManager.Query<CameraRandomizerTag>();
        foreach (var tag in tags){
            tag.transform.rotation = Quaternion.Euler(rot.x, rot.y, rot.z);
            tag.transform.position = pos;
            // tag.transform.rotation = Quaternion.Euler(0f, rotation.Sample(), 0f);

        }
            
    }
}
