using Microsoft.AspNetCore.Mvc;

namespace TO.WebApp.MVC.Extensions;

public class SummaryViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync() => View();
}
