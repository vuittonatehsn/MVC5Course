using System;
using System.Linq;
using System.Collections.Generic;
	
namespace MVC5Course.Models
{   
	public  class ProductRepository : EFRepository<Product>, IProductRepository
	{
        /// <summary>
        /// 多overrive 這層去複寫base的all 讓他取出沒有被刪除的所有資料
        /// </summary>
        /// <returns></returns>
        public override IQueryable<Product> All()
        {
            return base.All().Where(w=> !w.IsDeleted );
        }
        /// <summary>
        /// 如果有一個頁面很機巴的剛好要看所有連帶刪除的資料，就再給一層判別
        /// </summary>
        /// <param name="showAll"></param>
        /// <returns></returns>
        public IQueryable<Product> All (bool showAll){
            if (showAll)
                return base.All();
            else
                return this.All();
        }


        public Product GetById(int? id)
        {
            return this.All().FirstOrDefault(r => r.ProductId == id);
        }

        public IQueryable<Product> GetAll取得十筆資料(bool? active = true, bool? showAll = false)
        {
            IQueryable<Product> all = this.All();
            if (showAll.Value)
            {
                all = base.All();
            }
            return all.Where(r => r.Active.HasValue & r.Active == active)
                .OrderByDescending(x => x.ProductId).Take(20);
        }
        public override void Delete (Product p)
        {
            p.IsDeleted = true;
          
        }
	}

	public  interface IProductRepository : IRepository<Product>
	{

	}
}