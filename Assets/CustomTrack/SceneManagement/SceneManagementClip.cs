using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.SceneManagement;

namespace ZeniZeni.CustomTrack
{
    [Serializable]
    public class SceneManagementClip : PlayableAsset, ITimelineClipAsset
    {
        public string m_scene;
        public LoadSceneMode m_mode;

        public ClipCaps clipCaps
        {
            get { return ClipCaps.None; }
        }

        public override Playable CreatePlayable (PlayableGraph graph, GameObject owner)
        {
            
            var bh = new SceneManagementBehaviour();
            return ScriptPlayable<SceneManagementBehaviour>.Create (graph, bh);
        }
    }
}
