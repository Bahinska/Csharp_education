using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vp5_state_pattern
{
    public abstract class State
    {
        protected Product _product;

        public void SetProduct(Product product)
        {
            this._product = product;
        }
        public abstract void Edit();
        public abstract void NextStage();
    }
}
