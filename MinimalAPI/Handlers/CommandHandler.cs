using Microsoft.EntityFrameworkCore;
using MinimalAPI.Contexts;
using MinimalAPI.Models;
using System.Reflection.Metadata;

namespace MinimalAPI.Handlers
{
    public class CommandHandler
    {
        private readonly Context _context;

        public CommandHandler(Context context)
        {
            _context = context;
        }

        public async Task<int> Handle(UpdateCreateCommand command)
        {
            var equipment = new Equipment
            {
                Name = command.Name,
                Description = command.Description,
                Code = command.Code,
                Parameters = command.Parameters?.Select(p => new Parameters
                {
                    ParameterName = p.ParameterName,
                    ParameterDescription = p.ParameterDescription
                }).ToList()
            };

            _context.Equipments.Add(equipment);
            await _context.SaveChangesAsync();

            return equipment.Id;
        }

        public async Task Handle(int id, UpdateCreateCommand command)
        {
            var equipment = new Equipment { Id = id };
            _context.Attach(equipment);

            equipment.Name = command.Name;
            equipment.Description = command.Description;
            equipment.Code = command.Code;

            if(equipment.Parameters != null) _context.Parameters.RemoveRange(equipment.Parameters);

            equipment.Parameters = command.Parameters?.Select(p => new Parameters
            {
                ParameterName = p.ParameterName,
                ParameterDescription = p.ParameterDescription
            }).ToList();

            _context.Entry(equipment).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task Handle(int id)
        {
            var equipment = new Equipment { Id = id };
            _context.Attach(equipment);

            _context.Entry(equipment).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }
    }
}
