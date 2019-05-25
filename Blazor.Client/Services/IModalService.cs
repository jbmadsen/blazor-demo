using System;
using Microsoft.AspNetCore.Blazor;
using Microsoft.AspNetCore.Components;

namespace Blazor.Client.Services
{
    // From: https://www.telerik.com/blogs/creating-a-reusable-javascript-free-blazor-modal

    public interface IModalSerivce
    {
        event Action<string, RenderFragment> OnShow;
		event Action OnClose; 

		void Show(string title, Type contentType);
        void Close();
    }


    public class ModalService : IModalSerivce
    {
        public event Action<string, RenderFragment> OnShow;
		public event Action OnClose; 


		public void Show(string title, Type contentType)
        {
            if (contentType.BaseType != typeof(ComponentBase))
			{
				throw new ArgumentException($"{contentType.FullName} must be a Blazor Component");
			}
			
			var content = new RenderFragment(x => 
            { 
                x.OpenComponent(1, contentType); 
                x.CloseComponent(); 
            });
			OnShow?.Invoke(title, content);
        }


        public void Close()
        {
            OnClose?.Invoke();
        }
    }
}