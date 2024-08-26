﻿using PezzaApi;

namespace PezzaApi.Customer.DTO
{
    public class CustomerDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string? Address { get; set; }

        public string? Email { get; set; }

        public string? Cellphone { get; set; }

        public CustomerDTO()
        {
        }

        public CustomerDTO(Customer customer)
        {
            Id = customer.Id;
            Name = customer.Name;
            Address = customer.Address;
            Email = customer.Email;
            Cellphone = customer.Cellphone;
        }
    }
}