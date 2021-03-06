﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesTransaction.Application.Model.Customer;
using SalesTransaction.Application.Service.Customer;
using SalesTransaction.Application.WebApi.Areas.Base;

namespace SalesTransaction.Application.WebApi.Areas.Customer
{
    public class CustomerController : BaseController
    {
        private ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public IActionResult AllCustomerDetail()
        {
            try
            {
                dynamic jsonString = _customerService.GetAllCustomerDetail();
                return Ok(jsonString);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public IActionResult AddCustomer([FromBody] MvCustomer customer)
        {
            try
            {
                var added = _customerService.AddCustomer(customer);
                if (!added)
                {
                    return BadRequest();
                }
                return Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public IActionResult EditCustomer([FromBody] MvEditCustomer customer)
        {
            try
            {
                var edited = _customerService.EditCustomer(customer);
                if (!edited)
                {
                    return BadRequest();
                }
                return Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
