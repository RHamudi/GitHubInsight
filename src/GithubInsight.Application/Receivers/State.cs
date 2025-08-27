using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GithubInsight.Application.Receivers
{
    public class State
    {
        public State(int statusCode, string message, object data)
        {
            StatusCode = statusCode;
            Message = message;
            Data = data;
        }

        public int StatusCode { get; private set; }
        public string Message { get; private set; }
        public object Data { get; private set; }
    }
}
