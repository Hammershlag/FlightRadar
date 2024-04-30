using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OOD_24L_01180689.src.dataStorage;
using OOD_24L_01180689.src.dto.entities;
using OOD_24L_01180689.src.dto.entities.airports;
using OOD_24L_01180689.src.dto.entities.cargo;
using OOD_24L_01180689.src.dto.entities.flights;
using OOD_24L_01180689.src.dto.entities.people;
using OOD_24L_01180689.src.dto.entities.planes;
using OOD_24L_01180689.src.miscellaneous;

namespace OOD_24L_01180689.src.console.commands.command
{
    public class DisplayCommand : Command
    {
        public List<string> object_fields = new List<string>();
        public ConditionsList conditionsList = new ConditionsList();
        private List<Entity> valid_entities = new List<Entity>();
        private Dictionary<string, int> field_max_len = new Dictionary<string, int>();

        public DisplayCommand(string object_class, List<string> object_fields, ConditionsList conditionsList)
        {
            this.object_class = object_class;
            this.object_fields = object_fields;
            this.conditionsList = conditionsList;
        }

        public override bool Execute()
        {
            //TODO now is ignoring conditions and implement using dictionary
            CreateValidEntities();
            CalculateMaxLength();

            PrintHeader();
            PrintValidEntities();

            return true;
        }

        private void CalculateMaxLength()
        {
            foreach (var field in object_fields)
            {
                field_max_len[field] = field.Length;
            }
            foreach (var field in object_fields)
            {
                foreach (var valid in valid_entities)
                {
                    string value = valid.GetFieldValue(field);
                    int valueLength = value?.Length ?? 0;

                    if (!field_max_len.ContainsKey(field) || field_max_len[field] < valueLength)
                    {
                        field_max_len[field] = valueLength;
                    }
                }
            }
        }

        private void PrintValidEntities()
        {
            foreach (var valid in valid_entities)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var field in object_fields)
                {
                    string value = valid.GetFieldValue(field);
                    int padding = (field_max_len[field] - (value?.Length ?? 0)) / 2;

                    sb.Append(new string(' ', padding * 2));

                    if ((field_max_len[field] - (value?.Length ?? 0)) % 2 != 0)
                        sb.Append(' ');

                    sb.Append(value);

                    sb.Append("  |  ");
                }
                Console.WriteLine(sb.ToString().ToCharArray());
            }
        }



        private void CreateValidEntities()
        {
            foreach (var obj in DataStorage.GetInstance.GetObjectList())
            {
                if (this.object_class == "Airport")
                {
                    if (CheckType<Airport>.Check(obj, out Airport airport))
                    {
                        if (!conditionsList.Check(airport)) continue;
                        valid_entities.Add(airport);
                    }
                }
                else if (this.object_class == "Cargo")
                {
                    if (CheckType<Cargo>.Check(obj, out Cargo cargo))
                    {
                        if(!conditionsList.Check(cargo)) continue;
                        valid_entities.Add(cargo);
                    }
                }
                else if (this.object_class == "Flight")
                {
                    if (CheckType<Flight>.Check(obj, out Flight flight))
                    {
                        if(!conditionsList.Check(flight)) continue;
                        valid_entities.Add(flight);
                    }
                }
                else if (this.object_class == "Crew")
                {
                    if (CheckType<Crew>.Check(obj, out Crew crew))
                    {
                        if(!conditionsList.Check(crew)) continue;
                        valid_entities.Add(crew);
                    }
                }
                else if (this.object_class == "Passenger")
                {
                    if (CheckType<Passenger>.Check(obj, out Passenger passenger))
                    {
                        if(!conditionsList.Check(passenger)) continue;
                        valid_entities.Add(passenger);
                    }
                }
                else if (this.object_class == "CargoPlane")
                {
                    if (CheckType<CargoPlane>.Check(obj, out CargoPlane cargoPLane))
                    {
                        if(!conditionsList.Check(cargoPLane)) continue;
                        valid_entities.Add(cargoPLane);
                    }
                }
                else if (this.object_class == "PassengerPlane")
                {
                    if (CheckType<PassengerPlane>.Check(obj, out PassengerPlane passengerPlane))
                    {
                        if(!conditionsList.Check(passengerPlane)) continue;
                        valid_entities.Add(passengerPlane);
                    }
                }
            }
        }

        private void PrintHeader()
        {
            StringBuilder headerBuilder = new StringBuilder();
            StringBuilder separatorBuilder = new StringBuilder();

            foreach (var field in object_fields)
    {
        // No need for padding calculation for left alignment
        int padding = 1;

        // Build header
        headerBuilder.Append(field);
        headerBuilder.Append(new string(' ', field_max_len[field] - field.Length)); // Adjust padding for left alignment
        headerBuilder.Append("  |  ");
    }
            separatorBuilder.Append('-', headerBuilder.Length - 2);


            for (int i = 0; i < headerBuilder.Length && i < separatorBuilder.Length; i++)
            {
                if (headerBuilder[i] == '|')
                {
                    separatorBuilder[i] = '+';
                }
            }

            Console.WriteLine(headerBuilder.ToString()); // Print the header line
            Console.WriteLine(separatorBuilder.ToString()); // Print the separator line
        }




    }
}
