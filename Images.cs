using System;
using System.Net.Cache;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SnakeGame
{
    public static class Images
    {
        private static readonly ImageSource Empty = LoadImage("Empty.png");
        private static readonly ImageSource Body = LoadImage("Body.png");
        private static readonly ImageSource Head = LoadImage("Head.png");
        private static readonly ImageSource Food = LoadImage("Food.png");
        private static readonly ImageSource DeadBody = LoadImage("DeadBody.png");
        private static readonly ImageSource DeadHead = LoadImage("DeadHead.png");


        private static ImageSource LoadImage(string filename)
        {
            return new BitmapImage(new Uri($"Assets/{filename}", UriKind.Relative),
                new RequestCachePolicy(RequestCacheLevel.CacheOnly));
        }
    }
}
