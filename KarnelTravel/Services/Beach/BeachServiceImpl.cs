

using KarnelTravel.Models;

namespace KarnelTravel.Services.Beach;

public class BeachServiceImpl : IBeachService
{
    private DatabaseContext db;
    public BeachServiceImpl(DatabaseContext _db) { 
        db = _db;
    }

    public bool create(Models.Beach beach)
    {
        try
        {
            db.Beaches.Add(beach);
            return db.SaveChanges() > 0;
        }
        catch
        {
            return false;
        }
    }
}
