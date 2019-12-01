using System.Collections.Generic;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using System.Linq;
using UnityEngine.SceneManagement;

namespace ZeniZeni.CustomTrack
{
    public class SceneManagementMixer : PlayableBehaviour
    {

        internal PlayableDirector m_playableDirector;

        internal string m_scene;
        internal LoadSceneMode m_mode;
        
        internal IEnumerable<TimelineClip> m_clips;
        private bool oneShot = true;
        
        // NOTE: This function is called at runtime and edit time.  Keep that in mind when setting the values of properties.
        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            if (m_clips.Count() >= 2) return;

            int inputCount = playable.GetInputCount<Playable>();
            if (inputCount == 0) return;
            
            var time = m_playableDirector.time;
            var enumulator = m_clips.GetEnumerator();
            enumulator.MoveNext();
            
            for (int i = 0; i < inputCount; i++, enumulator.MoveNext())
            {
                var clip = enumulator.Current;

                var asset = clip.asset as SceneManagementClip;

                m_scene = asset.m_scene;
                m_mode = asset.m_mode;
                if (clip.start <= time && time <= clip.end  && oneShot)
                {
                    SceneManager.LoadSceneAsync(m_scene, m_mode);
                    oneShot = false;
                }
                else if (time < clip.start || time > clip.end)
                {
                    if (!oneShot) oneShot = true;
                }
            }

        }
    }
}
