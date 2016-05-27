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
    public class ProductInPreviewController : TableController<ProductInPreview>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            ProductPortfolioAzureMobileApp20160430120128Context context = new ProductPortfolioAzureMobileApp20160430120128Context();
            DomainManager = new EntityDomainManager<ProductInPreview>(context, Request);
        }

        // GET tables/ProductInPreview
        public IQueryable<ProductInPreview> GetAllProductInPreview()
        {
            return Query(); 
        }

        // GET tables/ProductInPreview/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<ProductInPreview> GetProductInPreview(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/ProductInPreview/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<ProductInPreview> PatchProductInPreview(string id, Delta<ProductInPreview> patch)
        {
            return null;
            // return UpdateAsync(id, patch);
        }

        // POST tables/ProductInPreview
        public async Task<IHttpActionResult> PostProductInPreview(ProductInPreview item)
        {
            return null;
            //ProductInPreview current = await InsertAsync(item);
            //return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/ProductInPreview/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteProductInPreview(string id)
        {
            return null;
            // return DeleteAsync(id);
        }
    }
}
