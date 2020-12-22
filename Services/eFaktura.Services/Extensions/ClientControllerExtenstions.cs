using eFaktura.Core.Models;
using EFaktura.Core.Entities;
using System.Collections.Generic;

namespace eFaktura.Services.Extensions
{
    public static class ClientControllerExtenstions
    {
        public static List<Client> Transform(this List<ClientEntity> entities)
        {
            List<Client> clients = new List<Client>();
            entities.ForEach(item => 
            {
                clients.Add(new Client
                {
                    Id = item.Id,
                    Name = item.Name,
                    PdvNumber = item.PdvNumber,
                    IdNumber = item.IdNumber
                });
            });

            return clients;
        }
    }
}
