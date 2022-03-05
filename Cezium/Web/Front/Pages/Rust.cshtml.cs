using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
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
        return new JsonResult(FrontHandler.RustHandler.Settings.State);
    }

    public IActionResult OnGetDebug()
    {
        return new JsonResult(FrontHandler.RustHandler.Settings.DebugState);
    }

    public IActionResult OnGetFov()
    {
        return new JsonResult(FrontHandler.RustHandler.Settings.Fov);
    }

    public IActionResult OnGetSensitivity()
    {
        return new JsonResult(FrontHandler.RustHandler.Settings.Sensitivity);
    }

    public IActionResult OnGetSmoothness()
    {
        return new JsonResult(FrontHandler.RustHandler.Settings.Smoothness);
    }

    public IActionResult OnGetHorizontal()
    {
        return new JsonResult(FrontHandler.RustHandler.Settings.RecoilModifier.Item1);
    }

    public IActionResult OnGetVertical()
    {
        return new JsonResult(FrontHandler.RustHandler.Settings.RecoilModifier.Item2);
    }

    public IActionResult OnGetInfiniteAmmo()
    {
        return new JsonResult(FrontHandler.RustHandler.Settings.InfiniteAmmo);
    }

    public IActionResult OnGetTapping()
    {
        return new JsonResult(FrontHandler.RustHandler.Settings.Tapping);
    }

    public IActionResult OnGetRandomization()
    {
        return new JsonResult(FrontHandler.RustHandler.Settings.Randomization);
    }

    public IActionResult OnGetReverseRandomization()
    {
        return new JsonResult(FrontHandler.RustHandler.Settings.ReverseRandomization);
    }

    public IActionResult OnGetRandomizationX()
    {
        return new JsonResult(JsonSerializer.Serialize(FrontHandler.RustHandler.Settings.RandomizationX));
    }

    public IActionResult OnGetRandomizationY()
    {
        return new JsonResult(JsonSerializer.Serialize(FrontHandler.RustHandler.Settings.RandomizationY));
    }

    public IActionResult OnGetGun()
    {
        return new JsonResult(FrontHandler.RustHandler.Settings.Gun.Item1.ToString());
    }

    public IActionResult OnGetScope()
    {
        if (FrontHandler.RustHandler.Settings.GunScope == null)
        {
            return new JsonResult("EmptyScope");
        }

        return new JsonResult(FrontHandler.RustHandler.Settings.GunScope.ToString());
    }

    public IActionResult OnGetAttachment()
    {
        if (FrontHandler.RustHandler.Settings.GunAttachment == null)
        {
            return new JsonResult("EmptyAttachment");
        }

        return new JsonResult(FrontHandler.RustHandler.Settings.GunAttachment.ToString());
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