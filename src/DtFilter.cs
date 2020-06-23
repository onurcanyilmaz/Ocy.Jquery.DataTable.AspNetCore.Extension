using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace src
{
    public class DtFilter<T> : IDtFilter<T> where T : class
    {
        public List<T> Filter(List<T> data, DtRequest request, out int filteredCount)
        {
            if (data.Any())
            {
                var type = typeof(T);
                var propertynames = new List<PropertyType>();
                foreach (PropertyInfo p in type.GetProperties())
                {
                    propertynames.Add(new PropertyType { Name = p.Name, Type = p.PropertyType });
                }
                foreach (var param in request.Columns)
                {
                    if (!String.IsNullOrEmpty(param.Search.Value) && param.Search.Value != "-")
                    {
                        var date = new DateTime();
                        var date2 = new DateTime();
                        var time = new TimeSpan();
                        var time2 = new TimeSpan();
                        int number;
                        int number2;
                        decimal deci;
                        decimal deci2;
                        float flo;
                        float flo2;
                        var theprop = propertynames.Where(x => x.Name == param.Data).FirstOrDefault();
                        if (param.Search.Value.Split('-').Count() == 2 && theprop.Type == typeof(int) && Int32.TryParse(param.Search.Value.Split('-').ElementAt(0).Trim(), out number) && Int32.TryParse(param.Search.Value.Split('-').ElementAt(1).Trim(), out number2))
                        {
                            data = data.Where(f => f.GetProperty(theprop.Name) != null ? (Convert.ToInt32(f.GetProperty(theprop.Name)) >= number && Convert.ToInt32(f.GetProperty(theprop.Name)) <= number2) : f.GetProperty(theprop.Name) != null).ToList();
                        }
                        else if (param.Search.Value.Split('-').Count() == 2 && theprop.Type == typeof(decimal) && Decimal.TryParse(param.Search.Value.Split('-').ElementAt(0).Trim(), out deci) && Decimal.TryParse(param.Search.Value.Split('-').ElementAt(1).Trim(), out deci2))
                        {
                            data = data.Where(f => (f.GetProperty(theprop.Name)) != null ? (Convert.ToInt32(f.GetProperty(theprop.Name)) >= deci && Convert.ToInt32(f.GetProperty(theprop.Name)) <= deci2) : (f.GetProperty(theprop.Name)) != null).ToList();
                        }
                        else if (param.Search.Value.Split('-').Count() == 2 && theprop.Type == typeof(float) && float.TryParse(param.Search.Value.Split('-').ElementAt(0).Trim(), out flo) && float.TryParse(param.Search.Value.Split('-').ElementAt(1).Trim(), out flo2))
                        {
                            data = data.Where(f => (f.GetProperty(theprop.Name)) != null ? (Convert.ToDouble(f.GetProperty(theprop.Name)) >= flo && Convert.ToDouble(f.GetProperty(theprop.Name)) <= flo2) : (f.GetProperty(theprop.Name)) != null).ToList();
                        }
                        else if (param.Search.Value.Split('-').Count() == 2 && theprop.Type == typeof(DateTime) && DateTime.TryParse(param.Search.Value.Split('-').ElementAt(0).Trim(), out date) && DateTime.TryParse(param.Search.Value.Split('-').ElementAt(1).Trim(), out date2))
                        {
                            data = data.Where(f => (f.GetProperty(theprop.Name)) != null ? (Check.GetDateZeroTime(Convert.ToDateTime(f.GetProperty(theprop.Name))) >= date && Check.GetDateZeroTime(Convert.ToDateTime(f.GetProperty(theprop.Name))) <= date2) : (f.GetProperty(theprop.Name)) != null).ToList();
                        }
                        else if (param.Search.Value.Split('-').Count() == 2 && theprop.Type == typeof(TimeSpan) && TimeSpan.TryParse(param.Search.Value.Split('-').ElementAt(0).Trim(), out time) && TimeSpan.TryParse(param.Search.Value.Split('-').ElementAt(1).Trim(), out time2))
                        {
                            data = data.Where(f => (f.GetProperty(theprop.Name)) != null ? (TimeSpan.Parse(f.GetProperty(theprop.Name).ToString()) >= time && TimeSpan.Parse(f.GetProperty(theprop.Name).ToString()) <= time2) : (f.GetProperty(theprop.Name)) != null).ToList();
                        }
                        else
                        {
                            if (DateTime.TryParse(param.Search.Value, out date) && !param.Search.Value.Contains(':'))
                            {
                                param.Search.Value = Convert.ToDateTime(param.Search.Value).ToString();
                            }
                            if (theprop.Type == typeof(int))
                            {
                                data = data.Where(f => (f.GetProperty(theprop.Name)) != null ? (f.GetProperty(theprop.Name)).ToString().ToLower().Trim().Equals(param.Search.Value.ToLower().Trim()) : (f.GetProperty(theprop.Name)) != null).ToList();
                            }
                            else
                            {
                                data = data.Where(f => (f.GetProperty(theprop.Name)) != null ? (f.GetProperty(theprop.Name)).ToString().ToLower().Contains(param.Search.Value.ToLower()) : (f.GetProperty(theprop.Name)) != null).ToList();
                            }
                        }
                    }
                }
                foreach (var item in request.Order)
                {
                    var order_field = type.GetProperty(propertynames.ElementAt(item.Column).Name);
                    if (item.Dir == "asc")
                    {
                        data = data.OrderBy(x => order_field.GetValue(x, null)).ToList();
                    }
                    else
                    {
                        data = data.OrderByDescending(x => order_field.GetValue(x, null)).ToList();
                    }
                }
            }
            filteredCount = data.Count;
            return data.Skip(request.Start).Take(request.Length).ToList();
        }
    }
}