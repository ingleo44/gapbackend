using System;
using System.Collections.Generic;
using System.Linq;

namespace medAppointments.Converters
{
    public static class ViewModelConverter
    {
        /// <summary>
        /// Convert a list of entities from list of models to list of viewmodels
        /// </summary>
        /// <param name="model"></param>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDestiny"></typeparam>
        /// <returns></returns>
        public static ICollection<TDestiny> ConvertListViewModel<TSource, TDestiny>(ICollection<TSource> model) where TSource : new() where TDestiny : new()
        {
            if (model == null) return new List<TDestiny>();
            return model.Select(ConvertViewModel<TSource, TDestiny>).ToList();
        }

        /// <summary>
        /// Convert a single entity from model to viewmodel
        /// </summary>
        /// <param name="model"></param>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDestiny"></typeparam>
        /// <returns></returns>
        public static TDestiny ConvertViewModel<TSource, TDestiny>(TSource model) where TDestiny : new()
        {
            var viewModel = new TDestiny();
            viewModel = CopyFields<TSource, TDestiny>(model, viewModel);
            return viewModel;
        }


        /// <summary>
        /// entity to copy entities from a model to a viewmodel
        /// </summary>
        /// <param name="source"></param>
        /// <param name="dest"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TU"></typeparam>
        /// <returns></returns>
        private static TU CopyFields<T, TU>(T source, TU dest)
        {
            var sourceProps = typeof(T).GetProperties().Where(x => x.CanRead).ToList();
            var destProps = typeof(TU).GetProperties()
                .Where(x => x.CanWrite)
                .ToList();

            foreach (var sourceProp in sourceProps)
            {
                if (destProps.All(x => x.Name != sourceProp.Name)) continue;
                {
                    var p = destProps.First(x => x.Name == sourceProp.Name);
                    if (!p.CanWrite) continue;
                    var currentValue = sourceProp.GetValue(source, null);
                    try
                    {
                        p.SetValue(dest, currentValue == null ? "" : sourceProp.GetValue(source, null), null);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }

            }

            return dest;
        }
    }
}