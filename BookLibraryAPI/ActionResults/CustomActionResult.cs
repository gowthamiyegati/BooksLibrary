using BookLibraryAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BookLibraryAPI.ActionResults
{
    public class CustomActionResult : IActionResult
    {
        private readonly CustomActionResultVM _customActionResultVM;

        public CustomActionResult(CustomActionResultVM customActionResultVM)
        {
            _customActionResultVM = customActionResultVM;
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            var objectResult = new ObjectResult(_customActionResultVM.Exception ?? _customActionResultVM.Publisher as object)
            {
                StatusCode = _customActionResultVM.Exception != null ? StatusCodes.Status500InternalServerError : StatusCodes.Status200OK
            };

            await objectResult.ExecuteResultAsync(context);
        }
    }
}
