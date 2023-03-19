using UnityEngine;

namespace Platformer
{
    public sealed class QuestCoinModel : IQuestModel
    {
        public bool TryComplete(GameObject player)
        {
            return player.CompareTag("QuestCoin");
        }
    }
}
