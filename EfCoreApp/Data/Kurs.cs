using System.ComponentModel.DataAnnotations;

namespace EfCoreApp.Data
{
    public class Kurs
    {
        public int KursId { get; set; }
        public string? Baslik { get; set; }

        [Required(ErrorMessage ="Boş geçilemez")]
        public int OgretmenId { get; set; }
        public Ogretmen Ogretmen { get; set; } = null!; //her bir krus için tek bir öğretmen ataması var.
        public ICollection<KursKayit> KursKayitlari { get; set; } = new List<KursKayit>();
    }
}
