using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AsyncAndAwait.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var _stopwatch = Stopwatch.StartNew();
            /*-------------------------Synchronous Programming Result--------------------------*/
            _stopwatch.Start();
            int gLength = Contentlength("https://www.google.co.in/");
            int yLength = Contentlength("https://in.yahoo.com/");
            int mLength = Contentlength("https://www.microsoft.com/en-in");
            int sLength = Contentlength("https://stackoverflow.com/");
            int temp = Contentlength("https://navbharattimes.indiatimes.com/");
            int temp1 = Contentlength("https://themoviesflix.co/");
            _stopwatch.Stop();

            ViewBag.Time = _stopwatch.Elapsed;
            ViewBag.ElapsedTicks = _stopwatch.ElapsedTicks;
            ViewBag.GLength = gLength;
            ViewBag.YLength = yLength;
            ViewBag.MLength = mLength;

            /*-------------------------Asynchronous Programming Result--------------------------*/
            _stopwatch = Stopwatch.StartNew();
            _stopwatch.Start();
            Task<int> gLengthTask = ContentlengthAsync("https://www.google.co.in/");
            Task<int> yLengthTask = ContentlengthAsync("https://in.yahoo.com/");
            Task<int> mLengthTask = ContentlengthAsync("https://www.microsoft.com/en-in");
            Task<int> sLengthTask = ContentlengthAsync("https://stackoverflow.com/");
            Task<int> tempTask = ContentlengthAsync("https://navbharattimes.indiatimes.com/");
            Task<int> temp1Task = ContentlengthAsync("https://themoviesflix.co/");
            var result= await Task.WhenAll(gLengthTask, yLengthTask, mLengthTask, sLengthTask, tempTask, temp1Task);
            _stopwatch.Stop();

            ViewBag.TimeAsync = _stopwatch.Elapsed;
            ViewBag.ElapsedTicksAsync = _stopwatch.ElapsedTicks;
            ViewBag.GLengthAsync = gLengthTask.Result; // result[0];
            ViewBag.YLengthAsync = result[1];
            ViewBag.MLengthAsync = result[2];

            return View();
        }

        public int Contentlength(string url)
        {
            WebClient webClient = new WebClient();
            string googleString = webClient.DownloadString(url);
            return googleString.Length;
        }
        public async Task<int> ContentlengthAsync(string url)
        {
            WebClient webClient = new WebClient();
            string googleString =await webClient.DownloadStringTaskAsync(url);
            return googleString.Length;
        }
    }
}
