using System;

namespace Platformer
{
    public sealed class QuestController : IQuest
    {
        public event EventHandler<IQuest> QuestCompleted;
        private InteractiveObjectView _player;
        private QuestObjectView _questView;
        private bool _active;
        private IQuestModel _model; 

        public bool IsCompleted { get; private set; }

        public QuestController(InteractiveObjectView player, IQuestModel model, QuestObjectView questObjectView)
        {
            _player = player;
            _active = false;
            _model = model;
            _questView = questObjectView;
        }

        private void OnContact(QuestObjectView questItem)
        {
            if (questItem != null)
            {
                if (_model.TryComplete(questItem.gameObject))
                {
                    if (questItem == _questView)
                    {
                        Completed();
                    }
                }
            }
        }

        private void Completed()
        {
            if(!_active) return;
            _active = false;
            _player.OnQuestComplete -= OnContact;
            _questView.ProcessComplete();
            QuestCompleted?.Invoke(this, this);
        }

        public void Reset()
        {
            if(_active) return;
            _active = true;
            _player.OnQuestComplete += OnContact;
            _questView.ProcessActivate();
        }

        public void Dispose()
        {
            _player.OnQuestComplete -= OnContact;
        }
    }
}
