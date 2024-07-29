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

        public async Task<int> Handle(CreateCommand command)
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

        public async Task<bool> Handle(int id, UpdateCommand command)
        {
            var equipment = await _context.Equipments.Include(e => e.Parameters)
                                                     .FirstOrDefaultAsync(e => e.Id == id);

            if (equipment == null)
                return false;

            equipment.Name = command.Name;
            equipment.Description = command.Description;
            equipment.Code = command.Code;

            _context.Parameters.RemoveRange(equipment.Parameters);
            equipment.Parameters = command.Parameters?.Select(p => new Parameters
            {
                ParameterName = p.ParameterName,
                ParameterDescription = p.ParameterDescription
            }).ToList();

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
