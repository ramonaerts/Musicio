using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace Musicio.Client.Web.Invokables
{
    public class Functions
    {
        private readonly IJSRuntime _jsRuntime;
        private static Func<Task> _action;

        public Functions(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        [JSInvokable]
        public static void ExecuteTimeoutFunc()
        {
            _action.Invoke();
        }

        public void SetTimeout(Func<Task> action, int time)
        {
            _action = action;
            _jsRuntime.InvokeAsync<bool>("functions.setTimeout", time);
        }
    }
}
