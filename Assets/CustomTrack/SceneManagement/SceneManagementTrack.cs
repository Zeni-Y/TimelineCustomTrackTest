using System.Linq;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace ZeniZeni.CustomTrack
{
    [TrackColor(0.3523021f, 1f, 0f)]
    [TrackClipType(typeof(SceneManagementClip))]
    public class SceneManagementTrack : TrackAsset
    {
        
        public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
        {
            
            var mixer = ScriptPlayable<SceneManagementMixer>.Create(graph, inputCount);
            var director = go.GetComponent<PlayableDirector>();
            if (director != null)
            {
                SceneManagementMixer bh = mixer.GetBehaviour();
                bh.m_clips = GetClips();
                
                //Clipを二つ以上作成したら一つだけになるようにClipを新しい順に消す
                if (bh.m_clips.Count() >= 2)
                {
                    for (int i = 0; i < bh.m_clips.Count()-1; i++)
                    {
                        timelineAsset.DeleteClip(bh.m_clips.Last());
                    }
                    Debug.Log("You can put only one SceneManagementClip in this Track.");
                }
                bh.m_playableDirector = director;
            }
            
            return mixer;
        }
    }
}
