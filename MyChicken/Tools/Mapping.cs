using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyChicken.Tools
{
    public static class Mapping<T, U>
        where T : class
        where U : class
    {
        public static U DefautMapping(T t)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<T, U>());
            var mapper = config.CreateMapper();
            U model = mapper.Map<U>(t);
            return model;
        }

        //public static List<U> DefautListMapping(List<T> t)
        //{
        //    var config = new MapperConfiguration(cfg => cfg.CreateMap<T, U>());
        //    var mapper = config.CreateMapper();
        //    foreach (var elt in t as IEnumerable<T>)
        //    yield return mapper.Map<U>(elt);
        //}
    }
}