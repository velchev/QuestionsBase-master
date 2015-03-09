using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    using System.Data.Entity;

    public class BaseContext<TContext>:DbContext where TContext:DbContext
    {
        static BaseContext()
        {
            Database.SetInitializer<TContext>(null);
        }

        protected BaseContext()
            : base("QuestionBaseConnectionString")
        {
            
        }
    }
}
