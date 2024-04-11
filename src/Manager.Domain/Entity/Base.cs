using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Domain.Entity
{
    public abstract class Base
    {
        public Guid Id { get; set; }

        internal List<string> _errors;

        public IReadOnlyCollection<string> Errors => _errors;

        public abstract bool Validate();
    }
}
