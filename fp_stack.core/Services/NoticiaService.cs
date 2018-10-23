using fp_stack.core.Models;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace fp_stack.core.Services
{
    public class NoticiaService
    {
        public IMemoryCache _memoryCache;
        public NoticiaService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public IEnumerable<Noticia> GetItens(int total)
        {
            var key = "chave_de_noticias";

            var retorno = new List<Noticia>();

            if (!_memoryCache.TryGetValue(key, out retorno))
            {
                //consultar do banco
                Task.Delay(200).Wait();

                #region fake data
                retorno = new List<Noticia>();
                retorno.Add(new Noticia()
                {
                    Id = 1,
                    Titulo = "Google Pixel USB-C earbuds review",
                    Url = "#",
                    Imagem = "/assets/noticias/pixell_2.0.jpg",
                    DescricaoImagem = "descrição completa da imagem"
                });
                retorno.Add(new Noticia()
                {
                    Id = 1,
                    Titulo = "Apple unveils repair pricing for iPhone XR, screen fix costs $199",
                    Url = "#",
                    Imagem = "/assets/noticias/DSC_1786.0.0.jpg",
                    DescricaoImagem = "descrição completa da imagem"
                });
                retorno.Add(new Noticia()
                {
                    Id = 1,
                    Titulo = "YouTube is investing $20M in educational content, creators",
                    Url = "#",
                    Imagem = "/assets/noticias/Screen_Shot_2018_10_22_at_2.22.59_PM.0.png",
                    DescricaoImagem = "descrição completa da imagem"
                });
                retorno.Add(new Noticia()
                {
                    Id = 1,
                    Titulo = "Former Oculus CEO Brendan Iribe is leaving Facebook",
                    Url = "#",
                    Imagem = "/assets/noticias/DSC00275.0.0.jpg",
                    DescricaoImagem = "descrição completa da imagem"
                });
                retorno.Add(new Noticia()
                {
                    Id = 1,
                    Titulo = "Asus’ new Zen AiO 27 desktop has a hidden wireless charger in the base",
                    Url = "#",
                    Imagem = "/assets/noticias/asus.0.png",
                    DescricaoImagem = "descrição completa da imagem"
                });
                retorno.Add(new Noticia()
                {
                    Id = 1,
                    Titulo = "GOOGLE HOME HUB REVIEW: THE BEST DIGITAL PHOTO FRAME",
                    Url = "#",
                    Imagem = "/assets/noticias/dseifert_181018_3039_1325.0.jpg",
                    DescricaoImagem = "descrição completa da imagem"
                });
                retorno.Add(new Noticia()
                {
                    Id = 1,
                    Titulo = "Hackers accessed records of 75,000 people in government health insurance system breach",
                    Url = "#",
                    Imagem = "/assets/noticias/healthcare1_640.0.jpg",
                    DescricaoImagem = "descrição completa da imagem"
                });
                retorno.Add(new Noticia()
                {
                    Id = 1,
                    Titulo = "RAZER PHONE 2 REVIEW: IT GLOWS, BUT IT DOESN’T SHINE",
                    Url = "#",
                    Imagem = "/assets/noticias/akrales_181017_3028_0009.1540080382.jpg",
                    DescricaoImagem = "descrição completa da imagem"
                });
                retorno.Add(new Noticia()
                {
                    Id = 1,
                    Titulo = "Facebook introduces ‘Hunt for False News’ series in attempt to be transparent about misinformation",
                    Url = "#",
                    Imagem = "/assets/noticias/facebook.0.jpg",
                    DescricaoImagem = "descrição completa da imagem"
                });
                #endregion fake

                //    var cacheEntrieOptions = new MemoryCacheEntryOptions()
                //.SetSlidingExpiration(DateTime.Now.AddMinutes(30));

                var cacheEntrieOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(DateTime.Now.AddMinutes(30));

                _memoryCache.Set(key, retorno, cacheEntrieOptions);
            }


            return retorno;
        }
    }
}
