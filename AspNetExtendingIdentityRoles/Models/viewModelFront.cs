using PageWebMic.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PageWebMic.Models
{
    public class viewModelFront
    {

        private paginaMICEntities2 db = new paginaMICEntities2();

        List<articulos_contenido> todosLosArticulos;
        public static List<List<T>> Split<T>(List<T> collection, int size)
        {
            var chunks = new List<List<T>>();
            var chunkCount = collection.Count() / size;

            if (collection.Count % size > 0)
                chunkCount++;

            for (var i = 0; i < size; i++)
                chunks.Add(collection.Skip(i *size).Take(chunkCount).ToList());

            return chunks;
        }
        public  viewModelFront() {

            todosLosArticulos = db.articulos_contenido.ToList();
        }
        public List<articulos_contenido> articulosFront {
             get
            {
                return todosLosArticulos.Where(x=> x.id_title == "Noticias").First().articulos_contenido1.OrderByDescending(x=>x.id).Take(5).ToList();
            }
        }

        public List<articulos_contenido> viceministeriosFront
        {
            get
            {
                return todosLosArticulos.Where(x => x.id_title == "Viceministerios").First().articulos_contenido1.OrderByDescending(x => x.id).Take(5).ToList();
            }
        }

        public  List<List<articulos_contenido>> transparenciaFront
        {
            get
            {
                var lista  = todosLosArticulos.Where(x => x.id_title == "Transparencia").First().articulos_contenido1.ToList();
                var lsitas   =  viewModelFront.Split(lista,3);
                return lsitas;
            }
        }
        public  List<slideshow> slideShowPresentation {
            get {
                return db.slideshow.ToList();
            }
        }
        public cambio_combustible combustibleActual
        {
            get
            {
                return db.cambio_combustible.OrderByDescending(u => u.id).FirstOrDefault();
            }
        }

        public List<banners> bannersFront {

            get {
                return db.banners.Where(x => x.active ==true).ToList();
            }
        }


    }
}