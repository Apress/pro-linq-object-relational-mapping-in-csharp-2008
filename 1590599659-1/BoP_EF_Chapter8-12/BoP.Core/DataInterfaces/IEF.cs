using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Objects.DataClasses;
using System.Data.Metadata.Edm;


namespace BoP.Core.DataInterfaces
{
    public interface IEF : IEntityWithRelationships, IEntityWithChangeTracker, IEntityWithKey
    {
    }
}
