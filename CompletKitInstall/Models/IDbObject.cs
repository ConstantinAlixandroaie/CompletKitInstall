using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompletKitInstall.Models
{
    public interface IDbObject
    {
        public int Id { get; set; }
        IDbObject MakeNew();
        void UpdateFrom(IDbObject obj);

    }
}
