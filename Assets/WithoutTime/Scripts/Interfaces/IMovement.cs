using UnityEngine;

namespace Dplds.Core
{
    public interface IMovement
    {
        AudioClip[] StepsClips { get;}
        float SpeedWalk { get;}
        float SpeedRun { get;}
        int TypeGround { get; set; }
        void PlaySoundStep();
    }
}
