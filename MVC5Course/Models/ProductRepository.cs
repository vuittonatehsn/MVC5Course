using System;
using System.Linq;
using System.Collections.Generic;
	
namespace MVC5Course.Models
{   
	public  class ProductRepository : EFRepository<Product>, IProductRepository
	{
        /// <summary>
        /// �hoverrive �o�h�h�Ƽgbase��all ���L���X�S���Q�R�����Ҧ����
        /// </summary>
        /// <returns></returns>
        public override IQueryable<Product> All()
        {
            return base.All().Where(w=> !w.IsDeleted );
        }
        /// <summary>
        /// �p�G���@�ӭ����ܾ��ڪ���n�n�ݩҦ��s�a�R������ơA�N�A���@�h�P�O
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

        public IQueryable<Product> GetAll���o�Q�����(bool? active = true, bool? showAll = false)
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