using Microsoft.EntityFrameworkCore;

namespace EfCoreApp.Data
{
    public class DataContext :DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base(options)
        {
               
        }
        public DbSet<Kurs> Kurslar => Set<Kurs>();
        public DbSet<KursKayit> KursKayitlari => Set<KursKayit>();
        public DbSet<Ogrenci> Ogrencis => Set<Ogrenci>();
        public DbSet<Ogretmen> Ogretmenler => Set<Ogretmen>();
    }
}
