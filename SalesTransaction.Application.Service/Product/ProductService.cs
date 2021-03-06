﻿using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SalesTransaction.Application.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using SalesTransaction.Application.Model.Product;

namespace SalesTransaction.Application.Service.Product
{
    public class ProductService : IProductService
    {
        private DataAccessHelper _dah;
        private readonly string _connectionString;
        private readonly int _commandTimeout;
        private IConfiguration _configuration;


        public ProductService(IConfiguration configuration)
        {
            _configuration = configuration;
            dynamic connectionString = _configuration.GetSection("ConnectionString");
            _connectionString = connectionString["DefaultConnection"];

            if (_connectionString != null)
            {
                _dah = new DataAccessHelper(_connectionString);
            }

            _commandTimeout = Convert.ToInt32(connectionString["CommandTimeout"]);
        }

        public bool AddProduct(MvProduct product)
        {
            using (var connection = _dah.GetConnection())
            {
                var jsonNew = JsonConvert.SerializeObject(product);
                var command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SpProductProductRateTsk_Json";
                command.Parameters.Add("@json", SqlDbType.NVarChar).Value = jsonNew;
                command.CommandTimeout = _commandTimeout;

                int rows = command.ExecuteNonQuery();

                if (rows > 0)
                {
                    return true;
                }
                return false;


            }
        }
        public dynamic GetAllProductDetail()
        {
            using (var con = _dah.GetConnection())
            {
                var cmd = con.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SpAllProductDetailSel";
                cmd.CommandTimeout = _commandTimeout;

                using (SqlDataReader sqlrdr = cmd.ExecuteReader())
                {
                    try
                    {
                        if (sqlrdr.HasRows)
                        {
                            return _dah.GetJson(sqlrdr);
                        }
                        else
                        {
                            return null;
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }


            }
        }

        public bool EditProduct(MvEditProduct product)
        {
            using (var con = _dah.GetConnection())
            {
                var jsonNew = JsonConvert.SerializeObject(product);
                var cmd = con.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SpProductProductRateUpdTsk_Json";
                cmd.Parameters.Add("@Json", SqlDbType.NChar).Value = jsonNew;
                cmd.CommandTimeout = _commandTimeout;

                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    return true;
                }
                return false;
            }
        }
    }
}
