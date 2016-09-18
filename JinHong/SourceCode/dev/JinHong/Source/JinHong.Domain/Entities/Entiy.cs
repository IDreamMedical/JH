using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JinHong.Domain.Entities
{

    public abstract class Entitiy : Entity<int>, IEntity
    {


        #region Properties
        public int Id { get; set; }

        #endregion

    }
}
