namespace simpatizantes_api.Services
{
    public class AlmacenadorImagenesLocal : IAlmacenadorImagenes
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;

        public AlmacenadorImagenesLocal(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;

        }

        public async Task<string> GuardarImagen(string imgBase64, string container)
        {
            byte[] bytes = Convert.FromBase64String(imgBase64);
            string fileName = Guid.NewGuid().ToString() + ".jpg";
            string folder = Path.Combine(webHostEnvironment.WebRootPath, container);

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            string filePath = Path.Combine(folder, fileName);
            await System.IO.File.WriteAllBytesAsync(filePath, bytes);
            var currentURL = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}";
            var imgURL = Path.Combine(currentURL, container, fileName).Replace("\\", "/");
            return imgURL;
        }      
    }
}
