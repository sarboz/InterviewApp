using System.Threading.Tasks;

namespace InterviewApp.Core.ViewModels;

public class BaseViewModel
{
    public string Title { get; set; }

    public virtual Task OnAppearing()
    {
       return  Task.CompletedTask;
    }
}