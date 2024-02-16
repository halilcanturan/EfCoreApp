using System.ComponentModel.DataAnnotations;

namespace EfCoreApp.Data
{
    public class KursViewModel
    {
        public int KursId { get; set; }
        [Required(ErrorMessage ="Boş bırakılamaz.")]
        [StringLength(50,ErrorMessage ="En fazla 50 Karakter")]
        public string? Baslik { get; set; }

        [Required(ErrorMessage = "Boş bırakılamaz.")]
        public int OgretmenId { get; set; }

        public ICollection<KursKayit> KursKayitlari { get; set; } = new List<KursKayit>();
    }
}
