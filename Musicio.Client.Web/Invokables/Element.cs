using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace Musicio.Client.Web.Invokables
{
    public class Element
    {
        protected readonly string id;
        private readonly IJSRuntime _jsRuntime;

        public Element(string id, IJSRuntime jsRuntime)
        {
            this.id = id;
            _jsRuntime = jsRuntime;
        }

        public async Task<int> GetOffsetWidth()
        {
            return await _jsRuntime.InvokeAsync<int>("get_offsetWidth", id);
        }

        public async Task<int> GetOffsetLeft()
        {
            return await _jsRuntime.InvokeAsync<int>("get_offsetLeft", id);
        }
    }
}
