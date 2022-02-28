using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Threading;
using Cezium.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cezium.Web.Front.Pages;

public class Rust : PageModel
{
    public void OnGet()
    {
    }

    public class BoolSchema
    {
        public bool Value { get; set; }
    }

    public class DoubleSchema
    {
        public double Value { get; set; }
    }

    public class IntSchema
    {
        public int Value { get; set; }
    }

    public void OnPostState([FromBody] BoolSchema data)
    {
        using var client = new HttpClient();
        client.BaseAddress = new Uri(FrontHandler.Server);
        var postTask = client.PostAsync($"/api/settings/rust/State?state={data.Value}", null);
        postTask.Wait();
    }

    public void OnPostDebug([FromBody] BoolSchema data)
    {
        using var client = new HttpClient();
        client.BaseAddress = new Uri(FrontHandler.Server);
        var postTask = client.PostAsync($"/api/settings/rust/Debug?debug={data.Value}", null);
        postTask.Wait();
    }

    public void OnPostInfiniteAmmo([FromBody] BoolSchema data)
    {
        using var client = new HttpClient();
        client.BaseAddress = new Uri(FrontHandler.Server);
        var postTask = client.PostAsync($"/api/settings/rust/InfiniteAmmo?state={data.Value}", null);
        postTask.Wait();
    }

    public void OnPostTapping([FromBody] BoolSchema data)
    {
        using var client = new HttpClient();
        client.BaseAddress = new Uri(FrontHandler.Server);
        var postTask = client.PostAsync($"/api/settings/rust/Tapping?tapping={data.Value}", null);
        postTask.Wait();
    }

    public void OnPostRandomization([FromBody] BoolSchema data)
    {
        using var client = new HttpClient();
        client.BaseAddress = new Uri(FrontHandler.Server);
        var postTask = client.PostAsync($"/api/settings/rust/Randomization?randomization={data.Value}", null);
        postTask.Wait();
    }

    public void OnPostReverseRandomization([FromBody] BoolSchema data)
    {
        using var client = new HttpClient();
        client.BaseAddress = new Uri(FrontHandler.Server);
        var postTask = client.PostAsync($"/api/settings/rust/ReverseRandomization?reverseRandomization={data.Value}",
            null);
        postTask.Wait();
    }

    public void OnPostFov([FromBody] IntSchema data)
    {
        using var client = new HttpClient();
        client.BaseAddress = new Uri(FrontHandler.Server);
        var postTask = client.PostAsync($"/api/settings/rust/Fov?fov={data.Value}",
            null);
        postTask.Wait();
    }
    
    public void OnPostSensitivity([FromBody] DoubleSchema data)
    {
        using var client = new HttpClient();
        client.BaseAddress = new Uri(FrontHandler.Server);
        var postTask = client.PostAsync($"/api/settings/rust/Sensitivity?sensitivity={data.Value}",
            null);
        postTask.Wait();
    }
    public void OnPostSmoothness([FromBody] IntSchema data)
    {
        using var client = new HttpClient();
        client.BaseAddress = new Uri(FrontHandler.Server);
        var postTask = client.PostAsync($"/api/settings/rust/Smoothness?smoothness={data.Value}",
            null);
        postTask.Wait();
    }
    
    public void OnPostHorizontal([FromBody] DoubleSchema data)
    {
        using var client = new HttpClient();
        client.BaseAddress = new Uri(FrontHandler.Server);
        var postTask = client.PostAsync($"/api/settings/rust/HorizontalModifier?x={data.Value}",
            null);
        postTask.Wait();
    }
    
    public void OnPostVertical([FromBody] DoubleSchema data)
    {
        using var client = new HttpClient();
        client.BaseAddress = new Uri(FrontHandler.Server);
        var postTask = client.PostAsync($"/api/settings/rust/VerticalModifier?y={data.Value}",
            null);
        postTask.Wait();
    }
}