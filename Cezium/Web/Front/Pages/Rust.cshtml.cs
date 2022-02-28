﻿using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
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

    public class RandomizationSchema
    {
        public int MinValue { get; set; }
        public int MaxValue { get; set; }
    }

    #region Get
    
    public IActionResult OnGetState()
    {
        using var client = new HttpClient();
        client.BaseAddress = new Uri(FrontHandler.Server);
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        Task<HttpResponseMessage> response = client.GetAsync("/api/settings/rust/State");
        if (response.Result.IsSuccessStatusCode)
        {
            Task<bool> schema = response.Result.Content.ReadFromJsonAsync<bool>();
            return new JsonResult(schema.Result);
        }
        return null;
    }
    
    public IActionResult OnGetDebug()
    {
        using var client = new HttpClient();
        client.BaseAddress = new Uri(FrontHandler.Server);
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        Task<HttpResponseMessage> response = client.GetAsync("/api/settings/rust/Debug");
        if (response.Result.IsSuccessStatusCode)
        {
            Task<bool> schema = response.Result.Content.ReadFromJsonAsync<bool>();
            return new JsonResult(schema.Result);
        }
        return null;
    }
    
    public IActionResult OnGetFov()
    {
        using var client = new HttpClient();
        client.BaseAddress = new Uri(FrontHandler.Server);
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        Task<HttpResponseMessage> response = client.GetAsync("/api/settings/rust/Fov");
        if (response.Result.IsSuccessStatusCode)
        {
            Task<int> schema = response.Result.Content.ReadFromJsonAsync<int>();
            return new JsonResult(schema.Result);
        }
        return null;
    }
    
    public IActionResult OnGetSensitivity()
    {
        using var client = new HttpClient();
        client.BaseAddress = new Uri(FrontHandler.Server);
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        Task<HttpResponseMessage> response = client.GetAsync("/api/settings/rust/Sensitivity");
        if (response.Result.IsSuccessStatusCode)
        {
            Task<double> schema = response.Result.Content.ReadFromJsonAsync<double>();
            return new JsonResult(schema.Result);
        }
        return null;
    }
    
    public IActionResult OnGetSmoothness()
    {
        using var client = new HttpClient();
        client.BaseAddress = new Uri(FrontHandler.Server);
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        Task<HttpResponseMessage> response = client.GetAsync("/api/settings/rust/Smoothness");
        if (response.Result.IsSuccessStatusCode)
        {
            Task<int> schema = response.Result.Content.ReadFromJsonAsync<int>();
            return new JsonResult(schema.Result);
        }
        return null;
    }
    
    public IActionResult OnGetHorizontal()
    {
        using var client = new HttpClient();
        client.BaseAddress = new Uri(FrontHandler.Server);
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        Task<HttpResponseMessage> response = client.GetAsync("/api/settings/rust/HorizontalModifier");
        if (response.Result.IsSuccessStatusCode)
        {
            Task<double> schema = response.Result.Content.ReadFromJsonAsync<double>();
            return new JsonResult(schema.Result);
        }
        return null;
    }
    
    public IActionResult OnGetVertical()
    {
        using var client = new HttpClient();
        client.BaseAddress = new Uri(FrontHandler.Server);
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        Task<HttpResponseMessage> response = client.GetAsync("/api/settings/rust/VerticalModifier");
        if (response.Result.IsSuccessStatusCode)
        {
            Task<double> schema = response.Result.Content.ReadFromJsonAsync<double>();
            return new JsonResult(schema.Result);
        }
        return null;
    }
    
    public IActionResult OnGetInfiniteAmmo()
    {
        using var client = new HttpClient();
        client.BaseAddress = new Uri(FrontHandler.Server);
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        Task<HttpResponseMessage> response = client.GetAsync("/api/settings/rust/InfiniteAmmo");
        if (response.Result.IsSuccessStatusCode)
        {
            Task<bool> schema = response.Result.Content.ReadFromJsonAsync<bool>();
            return new JsonResult(schema.Result);
        }
        return null;
    }
    
    public IActionResult OnGetTapping()
    {
        //TODO: Implement
        // using var client = new HttpClient();
        // client.BaseAddress = new Uri(FrontHandler.Server);
        // client.DefaultRequestHeaders.Accept.Clear();
        // client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        // Task<HttpResponseMessage> response = client.GetAsync("/api/settings/rust/Tapping");
        // if (response.Result.IsSuccessStatusCode)
        // {
        //     Task<bool> schema = response.Result.Content.ReadFromJsonAsync<bool>();
        //     return new JsonResult(schema.Result);
        // }
        return null;
    }
    
    public IActionResult OnGetRandomization()
    {
        using var client = new HttpClient();
        client.BaseAddress = new Uri(FrontHandler.Server);
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        Task<HttpResponseMessage> response = client.GetAsync("/api/settings/rust/Randomization");
        if (response.Result.IsSuccessStatusCode)
        {
            Task<bool> schema = response.Result.Content.ReadFromJsonAsync<bool>();
            return new JsonResult(schema.Result);
        }
        return null;
    }
    
    public IActionResult OnGetReverseRandomization()
    {
        using var client = new HttpClient();
        client.BaseAddress = new Uri(FrontHandler.Server);
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        Task<HttpResponseMessage> response = client.GetAsync("/api/settings/rust/ReverseRandomization");
        if (response.Result.IsSuccessStatusCode)
        {
            Task<bool> schema = response.Result.Content.ReadFromJsonAsync<bool>();
            return new JsonResult(schema.Result);
        }
        return null;
    }
    

    #endregion

    #region Post

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
        var postTask = client.PostAsync($"/api/settings/rust/InfiniteAmmo?infiniteAmmo={data.Value}", null);
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

    public void OnPostRandomizationX([FromBody] RandomizationSchema data)
    {
        using var client = new HttpClient();
        client.BaseAddress = new Uri(FrontHandler.Server);
        var postTask = client.PostAsync(
            $"/api/settings/rust/RandomizationAmountX?min={data.MinValue}&max={data.MaxValue}",
            null);
        postTask.Wait();
    }

    public void OnPostRandomizationY([FromBody] RandomizationSchema data)
    {
        using var client = new HttpClient();
        client.BaseAddress = new Uri(FrontHandler.Server);
        var postTask = client.PostAsync(
            $"/api/settings/rust/RandomizationAmountY?min={data.MinValue}&max={data.MaxValue}",
            null);
        postTask.Wait();
    }

    #endregion
}