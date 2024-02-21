using Negus = System.Console;

namespace ParkingSystem
{
    public abstract class Charge
    {
        public double chargefee = 0;
        public string plateNo = "", vehicleType = "", vehicleBrand = "";
        public double motorbikeFee = 20.00, suvVanFee = 40.00, sedanFee = 30.00;
        public DateTime dateTime = DateTime.Now;
        public abstract double chargeFee(double hours, double minutes);
    }
    class MotorBike : Charge
    {
        public override double chargeFee(double hours, double minutes)
        {
            if (minutes >= 30)
            {
                chargefee = (hours * 5.00) + motorbikeFee;
                return chargefee + 5.00;
            }
            else
            {
                chargefee = (hours * 5.00) + motorbikeFee;
                return chargefee;
            }
        }
    }

    class SUVan : Charge
    {
        public override double chargeFee(double hours, double minutes)
        {
            if (minutes >= 30)
            {
                chargefee = (hours * 20.00) + suvVanFee;
                return chargefee + 20.00;
            }
            else
            {
                chargefee = (hours * 20.00) + suvVanFee;
                return chargefee;
            }
        }
    }

    class Sedan : Charge
    {
        public override double chargeFee(double hours, double minutes)
        {
            if (minutes >= 30)
            {
                chargefee = (hours * 15.00) + sedanFee;
                return chargefee + 15.00;
            }
            else
            {
                chargefee = (hours * 15.00) + sedanFee;
                return chargefee;
            }
        }
    }
    class Blueprint
    {
        public Blueprint(string plateNo, string vehicleType, string vehicleBrand)
        {
            this.plateNo = plateNo;
            this.vehicleType = vehicleType;
            this.vehicleBrand = vehicleBrand;
        }

        public void GetPoint(out string plateNo, out string vehicleType, out string vehicleBrand)
        {
            plateNo = this.plateNo;
            vehicleType = this.vehicleType;
            vehicleBrand = this.vehicleBrand;
        }

        string plateNo = "", vehicleType = "", vehicleBrand = "";

    }
    class MainClass
    {
        public static void Main(string[] args)
        {
            MainClass m = new MainClass();
            string choice = "";
            do
            {
                string vehicleType = "";
                DateTime parkingTime = DateTime.Now;
                DateTime parkout = DateTime.Now;
                Negus.Write("\tParking System\n\n\nPlate No.:");
                string plateNo = Negus.ReadLine();
                while (true)
                {
                    if (plateNo.Trim().Equals(""))
                    {
                        Negus.WriteLine("Field cannot be null.");
                        Negus.Write("\nPlate No.:");
                        plateNo = Negus.ReadLine();
                    }
                    else
                        break;
                }           
                bool check = true;
                Negus.Write("\nVehicle Type.:");
                vehicleType = Negus.ReadLine();
                while (check)
                {
                    switch (vehicleType.ToLower().Trim())
                    {
                        case "motorbike":
                        case "suv":
                        case "van":
                        case "sedan":
                            check = false;
                            break;
                        case "":
                            Negus.WriteLine("Field cannot be empty.");
                            check = true;
                            Negus.Write("\nVehicle Type.:");
                            vehicleType = Negus.ReadLine();
                            break;
                        default:
                            Negus.WriteLine("Invalid Vehicle Type");
                            check = true;
                            Negus.Write("\nVehicle Type.:");
                            vehicleType = Negus.ReadLine();
                            break;

                    }
                }
                Negus.Write("\nVehicle Brand:");
                string vehicleBrand = Negus.ReadLine();
                while (true)
                {
                    if (vehicleBrand.Trim().Equals(""))
                    {
                        Negus.WriteLine("Field cannot be empty.");
                        Negus.Write("\nVehicle Brand:");
                        vehicleBrand = Negus.ReadLine();
                    }
                    else
                        break;
                }
               

                // Out Parameters
                Blueprint bp = new Blueprint(plateNo.Trim(), vehicleType.Trim(), vehicleBrand.Trim());
                string pn, vt, vb;
                bp.GetPoint(out pn, out vt, out vb);

                string parkin = parkingTime.ToString();
                double days = 0;
                double hours = 0;
                double minutes = 0;
                while (true)
                {
                    try
                    {
                        Negus.Write("\n\nEnter Date and Time of Park-out in this format\nmm/dd/yy 00:00AM/PM\n:");
                        string input = Negus.ReadLine();
                        parkout = DateTime.Parse(input);
                        TimeSpan calcDate = parkout.Subtract(parkingTime);
                        days = calcDate.Days;
                        hours = calcDate.Hours;
                        minutes = calcDate.Minutes;
                        if (days < 0 || hours < 0 || minutes < 0)
                        {
                            Negus.WriteLine("\nInvalid Date/Time\nNOTE: Park-out date/day is invalid, must not be date/time before the park-in date\nTry again.\n");
                            Negus.Write("\n\nEnter Date and Time of Park-out in this format\nmm/dd/yy 00:00AM/PM\n:");
                            input = Negus.ReadLine();
                            parkout = DateTime.Parse(input);
                            calcDate = parkout.Subtract(parkingTime);
                            days = calcDate.Days;
                            hours = calcDate.Hours;
                            minutes = calcDate.Minutes;
                        }
                        else
                            break;
                    }
                    catch (Exception e)
                    {
                        Negus.WriteLine("\nInvalid Date/ Time Format. Please follow these format.\nmm/dd/yy 00:00AM/PM\nex. 09/06/03 11:00PM\n");
                        continue;
                    }
                }
                double amount = identifyVehicleType(vt.ToLower(), (double)hours, days, minutes);
                Negus.Write($"\nPlate No.: {pn}\nVehicle Type: {vt}\nVehicle Brand: {vb}\n");
                Negus.Write($"\nDate/Time-in: {parkin}\n\t Out: {parkout}\nParking Time: {days} day(s) {hours} hr(s) {minutes} minute(s)\nAmount: {amount}"); //identify vehicle and compute charge
                Negus.WriteLine("\nDo another computation?(Yes/No): ");
                choice = Negus.ReadLine();

            } while (!choice.ToLower().Trim().Equals("no"));

        }
        // Identifies vehicle and calculate charge fees with hours and days arguments
        public static double identifyVehicleType(string vehicle, double hours, double days, double minutes)
        {
            MotorBike motorbike = new MotorBike();
            SUVan suvan = new SUVan();
            Sedan sedan = new Sedan();
            switch (vehicle)
            {
                case "motorbike":
                    if (days > 0)
                    {
                        if (hours == 0) // if hours == 0 then proceed to charge 1 day == 24 hours to chargeFee method
                            return motorbike.chargeFee((days * 24), minutes);
                        else
                        {
                            double current = motorbike.chargeFee((hours + (days * 24)), minutes);
                            return current;
                        }
                    }
                    else
                        return motorbike.chargeFee(hours, minutes);
                    break;
                case "suv":
                case "van":
                    if (days > 0)
                    {
                        if (hours == 0)
                            return suvan.chargeFee((days * 24), minutes);
                        else
                        {
                            double current = suvan.chargeFee((hours + (days * 24)), minutes);
                            return current;
                        }

                    }
                    else
                        return suvan.chargeFee(hours, minutes);
                    break;
                case "sedan":
                    if (days > 0)
                    {
                        if (hours == 0)
                            return sedan.chargeFee((days * 24), minutes);
                        else
                        {
                            double current = sedan.chargeFee((hours + (days * 24)), minutes);
                            return current;
                        }
                    }
                    else
                        return sedan.chargeFee(hours, minutes);
                    break;
                default:
                    Negus.WriteLine("Invalid Vehicle");
                    return 0;
                    break;
            }
        }
    }
}
