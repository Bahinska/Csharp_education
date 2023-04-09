using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vp5_state_pattern
{
    internal class ModerationState : State
    {
        public override void Edit()
        {
            _product.Edit();
        }

        public override void NextStage()
        {
            _product.TransitionTo(new PublishedState());
        }
    }
}
