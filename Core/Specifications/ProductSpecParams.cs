using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class ProductSpecParams
    {

        //Page Index
        public int PageIndex { get; set; } = 1;

        //Page Size
        private const int MaxPageSize = 50;
       
        private int _pageSize = 6;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

        //Brands
        private List<string> _brands = [];
        public List<string> Brands
        {
            get => _brands;
            set
            {
                _brands = value.SelectMany(x => x.Split(',',
                    StringSplitOptions.RemoveEmptyEntries)).ToList();
            }
        }

        //Types
        private List<string> _types = [];
        public List<string> Types
        {
            get => _types;
            set
            {
                _types = value.SelectMany(x => x.Split(',',
                    StringSplitOptions.RemoveEmptyEntries)).ToList();
            }
        }

        //Sort
        public string? Sort { get; set; }

        //Search
        private string? _search;
        public string Search
        {
            get => _search ?? "";
            set => _search = value.ToLower();
        }


    }
}
