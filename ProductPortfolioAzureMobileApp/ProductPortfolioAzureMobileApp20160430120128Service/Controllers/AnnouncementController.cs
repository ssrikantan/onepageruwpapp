using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using ProductPortfolioAzureMobileApp20160430120128Service.DataObjects;
using ProductPortfolioAzureMobileApp20160430120128Service.Models;

namespace ProductPortfolioAzureMobileApp20160430120128Service.Controllers
{
    public class AnnouncementController : TableController<Announcement>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            ProductPortfolioAzureMobileApp20160430120128Context context = new ProductPortfolioAzureMobileApp20160430120128Context();
            DomainManager = new EntityDomainManager<Announcement>(context, Request);
        }

        // GET tables/Announcement
        public IQueryable<Announcement> GetAllAnnouncement()
        {
            return Query(); 
        }

        // GET tables/Announcement/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<Announcement> GetAnnouncement(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/Announcement/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<Announcement> PatchAnnouncement(string id, Delta<Announcement> patch)
        {
            return null;
          //   return UpdateAsync(id, patch);
        }

        // POST tables/Announcement
        public async Task<IHttpActionResult> PostAnnouncement(Announcement item)
        {
            return null;
            //Announcement current = await InsertAsync(item);
            //return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/Announcement/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteAnnouncement(string id)
        {
            return null;
            // return DeleteAsync(id);
        }
    }
}
