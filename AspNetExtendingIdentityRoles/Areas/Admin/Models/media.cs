//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PageWebMic.Areas.Admin.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class media
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public media()
        {
            this.articulos_contenido_media = new HashSet<articulos_contenido_media>();
        }
    
        public int id { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public string type { get; set; }
        public Nullable<int> size { get; set; }
        public string thumb { get; set; }
        public Nullable<bool> isServer { get; set; }
        public int articulo_contenido_id { get; set; }
        public string delete_url { get; set; }
        public string type_name { get; set; }
    
        public virtual articulos_contenido articulos_contenido1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<articulos_contenido_media> articulos_contenido_media { get; set; }
    }
}
