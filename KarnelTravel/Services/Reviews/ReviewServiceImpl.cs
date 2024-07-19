using CloudinaryDotNet;
using KarnelTravel.Models;

namespace KarnelTravel.Services.Reviews;

public class ReviewServiceImpl : IReviewService
{
    private DatabaseContext db;
    public ReviewServiceImpl(DatabaseContext _db)
    {
        db = _db;
    }
    public bool addReview(Review review)
    {
        try
        {
            db.Reviews.Add(review);
            return db.SaveChanges() > 0;
        }
        catch
        {
            return false;
        }
    }
}
