using UnityEngine;

namespace Plarformer
 {
    [CreateAssetMenu(fileName = "QuestStoryConfig", menuName = "Configs / QuestSystem / QuestStoryConfig", order = 1)]
    public class QuestStoryConfig : ScriptableObject
    {
        public QuestConfig[] questConfigs;
        public QuestType type;
    }
}
