using System;
using Microsoft.AspNetCore.Blazor;
using Microsoft.AspNetCore.Components;

namespace Blazor.Client.Services
{
    // From: https://www.telerik.com/blogs/creating-a-reusable-javascript-free-blazor-modal

    public interface IModalSerivce
    {
        event Action<string, RenderFragment, Action, Action> OnShow;
		event Action OnClose; 

		void Show(string title, Type contentType, Action positiveAction, Action negativeAction);
        void Close();
    }


    public class ModalService : IModalSerivce
    {
        public event Action<string, RenderFragment, Action, Action> OnShow;
		public event Action OnClose; 


		public void Show(string title, Type contentType, Action positiveAction, Action negativeAction)
        {
            if (contentType.BaseType != typeof(ComponentBase))
			{
				throw new ArgumentException($"{contentType.FullName} must be a Blazor Component");
			}
			
			var content = new RenderFragment(x => 
            {
                x.OpenComponent(1, contentType);
                x.AddAttribute(2, "PositiveAction", positiveAction);
                x.AddAttribute(3, "NegativeAction", negativeAction);
                x.AddAttribute(4, "CloseAction", () => Close());
                x.CloseComponent(); 
            });
			OnShow?.Invoke(title, content, positiveAction, negativeAction);
        }


        public void Close()
        {
            OnClose?.Invoke();
        }
    }
}