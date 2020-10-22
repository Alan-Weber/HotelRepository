using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicalLayer
{
    public class BaseValidator<T>
    {
        private List<string> errors = new List<string>();

        public void AddError(string error)
        {
            if (error != "")
            {
                errors.Add(error);
            }
        }

        public virtual Response Validate(T item)
        {
            Response response = CheckError();
            this.errors.Clear();
            return response;
        }

        private Response CheckError()
        {
            Response r = new Response();
            if (errors.Count != 0)
            {
                StringBuilder builder = new StringBuilder();
                //for (int i = 0; i < errors.Count; i++)
                //{
                //    builder.AppendLine(errors[i]);
                //}
                foreach (string item in errors)
                {
                    builder.AppendLine(item);
                }
                r.Message = builder.ToString();
                r.Success = false;
                return r;
            }
            r.Success = true;
            return r;
        }
    }
}
