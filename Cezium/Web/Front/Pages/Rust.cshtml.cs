using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Cezium.Rust;
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
        public int Item1 { get; set; }
        public int Item2 { get; set; }
    }

    public class StringSchema
    {
        public string Value { get; set; }
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
        using var client = new HttpClient();
        client.BaseAddress = new Uri(FrontHandler.Server);
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        Task<HttpResponseMessage> response = client.GetAsync("/api/settings/rust/Tapping");
        if (response.Result.IsSuccessStatusCode)
        {
            Task<bool> schema = response.Result.Content.ReadFromJsonAsync<bool>();
            return new JsonResult(schema.Result);
        }

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

    public IActionResult OnGetRandomizationX()
    {
        using var client = new HttpClient();
        client.BaseAddress = new Uri(FrontHandler.Server);
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        Task<HttpResponseMessage> response = client.GetAsync("/api/settings/rust/RandomizationX");
        if (response.Result.IsSuccessStatusCode)
        {
            Task<RandomizationSchema> schema = response.Result.Content.ReadFromJsonAsync<RandomizationSchema>();
            if (schema.Result != null)
            {
                return new JsonResult(schema.Result);
            }
        }

        return null;
    }

    public IActionResult OnGetRandomizationY()
    {
        using var client = new HttpClient();
        client.BaseAddress = new Uri(FrontHandler.Server);
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        Task<HttpResponseMessage> response = client.GetAsync("/api/settings/rust/RandomizationY");
        if (response.Result.IsSuccessStatusCode)
        {
            Task<RandomizationSchema> schema = response.Result.Content.ReadFromJsonAsync<RandomizationSchema>();
            if (schema.Result != null)
            {
                return new JsonResult(schema.Result);
            }
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
            $"/api/settings/rust/RandomizationX?min={data.Item1}&max={data.Item2}",
            null);
        postTask.Wait();
    }

    public void OnPostRandomizationY([FromBody] RandomizationSchema data)
    {
        using var client = new HttpClient();
        client.BaseAddress = new Uri(FrontHandler.Server);
        var postTask = client.PostAsync(
            $"/api/settings/rust/RandomizationY?min={data.Item1}&max={data.Item2}",
            null);
        postTask.Wait();
    }

    public void OnPostGun([FromBody] StringSchema data)
    {
        using var client = new HttpClient();
        client.BaseAddress = new Uri(FrontHandler.Server);

        Task<HttpResponseMessage> postTask;
        if (data.Value.ToLower().Contains("empty"))
        {
            postTask = client.PostAsync(
                "/api/settings/rust/Gun", null);
        }
        else
        {
            Enum.TryParse(data.Value, out RustSettings.Guns gun);
            postTask = client.PostAsync(
                $"/api/settings/rust/Gun?gun={(int) gun}",
                null);
        }

        postTask.Wait();
    }

    public void OnPostScope([FromBody] StringSchema data)
    {
        using var client = new HttpClient();
        client.BaseAddress = new Uri(FrontHandler.Server);

        Task<HttpResponseMessage> postTask;
        if (data.Value.ToLower().Contains("empty"))
        {
            postTask = client.PostAsync(
                "/api/settings/rust/Scope", null);
        }
        else
        {
            Enum.TryParse(data.Value, out RustSettings.Scope scope);
            postTask = client.PostAsync(
                $"/api/settings/rust/Scope?scope={(int) scope}",
                null);
        }

        postTask.Wait();
    }

    public void OnPostAttachment([FromBody] StringSchema data)
    {
        using var client = new HttpClient();
        client.BaseAddress = new Uri(FrontHandler.Server);

        Task<HttpResponseMessage> postTask;
        if (data.Value.ToLower().Contains("empty"))
        {
            postTask = client.PostAsync(
                "/api/settings/rust/Attachment", null);
        }
        else
        {
            Enum.TryParse(data.Value, out RustSettings.Attachment attachment);
            postTask = client.PostAsync(
                $"/api/settings/rust/Attachment?attachment={(int) attachment}",
                null);
        }

        postTask.Wait();
    }

    #endregion
}