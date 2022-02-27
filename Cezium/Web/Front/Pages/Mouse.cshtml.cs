using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using Cezium.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cezium.Web.Front.Pages;

public class Mouse : PageModel
{
    public void OnGet()
    {
    }
    public class StateSchema
    {
        public bool State { get; set; }
    }

    public void OnPostState([FromBody]StateSchema state)
    {
        using var client = new HttpClient();
        client.BaseAddress = new Uri("API server + Port");

        var schema = new StateSchema
        {
            State = state.State
        };

        var postTask = client.PostAsJsonAsync("/api/media/upload", schema);
        postTask.Wait();
        
        
        for (int i = 16 - 1; i >= 0; i--)
        {
            ConsoleUtils.WriteLine($"State: {state.State}");
        }
    }
    
    public IActionResult OnGetState()
    {
        return new JsonResult("True");
    }
    
    // public IActionResult OnPostFindUser()
    // {
    //     for (int i = 16 - 1; i >= 0; i--)
    //     {
    //         ConsoleUtils.WriteLine("AA");
    //     }
    //     return new JsonResult("Founded user");
    // }
}