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
    public class FeatureInPreviewController : TableController<FeatureInPreview>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            ProductPortfolioAzureMobileApp20160430120128Context context = new ProductPortfolioAzureMobileApp20160430120128Context();
            DomainManager = new EntityDomainManager<FeatureInPreview>(context, Request);
        }

        // GET tables/FeatureInPreview
        public IQueryable<FeatureInPreview> GetAllFeatureInPreview()
        {
            return Query(); 
        }

        // GET tables/FeatureInPreview/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<FeatureInPreview> GetFeatureInPreview(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/FeatureInPreview/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<FeatureInPreview> PatchFeatureInPreview(string id, Delta<FeatureInPreview> patch)
        {
            return null;
           //  return UpdateAsync(id, patch);
        }

        // POST tables/FeatureInPreview
        public async Task<IHttpActionResult> PostFeatureInPreview(FeatureInPreview item)
        {
            return null;
            //FeatureInPreview current = await InsertAsync(item);
            //return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/FeatureInPreview/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteFeatureInPreview(string id)
        {
            return null;
           //  return DeleteAsync(id);
        }
    }
}
