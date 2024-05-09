// See https://aka.ms/new-console-template for more information
using System;

// ===============================
// AUTHOR    : Sean Lee
// CREATE DATE     : 2024-05-09
// PURPOSE     : Used for the Company (KLK)'s employees assessment purposes
// SPECIAL NOTES: Refactored version of the pre-workshop code
// ===============================

class Program
{
    static void Main(string[] args)
    {
        // RUNNING BASIC TEST CODES (FOR SUPERVISOR VIEW)
        fizzbuzz();
        Console.WriteLine();
        string charge = water_charge(6000);
        Console.WriteLine(charge);
        employee_management_system();
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////
    // Description : prints the numbers from 1 to 100, but for multiples of 3, print "Fizz" instead of the number,
    // and for multiples of 5, print "Buzz". For numbers which are multiples of both 3 and 5, print "FizzBuzz". 
    ////////////////////////////////////////////////////////////////////////////////////////////////////////

    static void fizzbuzz()
    {
        const int STOPPING_AT_NUMBER = 100;
        int currentNumber = 1;
        
        while(currentNumber < STOPPING_AT_NUMBER)
        {
            // XOR operator returns true if currentNumber is a multiple of 3 or 5, and return false if currentNumber is a multiple of both 3 and 5, or none
           if(currentNumber%3==0 ^ currentNumber % 5 == 0)
            {
                Console.WriteLine(currentNumber % 3 == 0 ? "Fizz" : "Buzz");
            }
            else
            { 
                Console.WriteLine(currentNumber % 3 == 0 ? "FizzBuzz" : currentNumber);
            }
            currentNumber++;
        } 
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////
    // Description : calculates the water charges for a residential property based on the water consumption
    ////////////////////////////////////////////////////////////////////////////////////////////////////////
    static string water_charge(int lits)
    {
        const double BASE = 20;
        if (lits <= 1000)
        {
            return $"Total water charges: ${BASE} (base charge)";
        }

        return _calc_water_charge_aux(lits);
    }


    static string _calc_water_charge_aux(int lits)
    {
        const int BASE = 20;
        const int ABOVE_TAX_LITERS = 5000;
        const double TAX = 0.06;
        const int LITERS_FOR_FREE = 1000;

        double final_charge;

        if (lits > LITERS_FOR_FREE && lits <= ABOVE_TAX_LITERS)
        {
            double curr_usage = lits - LITERS_FOR_FREE;
            final_charge = curr_usage + BASE;
            return $"Total water charges: ${final_charge} (base charge + ${curr_usage} for usage between 1001 and 5000 liters)";
        }

        const double PER_LITER_CHARGE = 4000;
        double two_liter_charge = (lits - ABOVE_TAX_LITERS) * 2;
        double HST_AMOUNT = (lits + BASE) * TAX;
        final_charge = lits + BASE + HST_AMOUNT;

        return $@"Total water charges (including 6% tax): ${final_charge} (base charge
        + ${PER_LITER_CHARGE} for usage between 1001 to 5000 + ${two_liter_charge} above 5000 liters,
        plus tax)";
    }
    

    /* Structure "EmployeeData" is only used by associated methods with employee_management_system */
    private struct EmployeeData
        {
            public string? Name;                                                                
            public double? Salaries;    
        };

    ////////////////////////////////////////////////////////////////////////////////////////////////////////
    // Description : Get datas about employees based on the [num] of employees about each individual's name
    // and salary. Then, print_data prints the data based on the [employeeDatas[]] array.
    ////////////////////////////////////////////////////////////////////////////////////////////////////////
    
        
    static void employee_management_system()
    {
        int num_of_employees = 3;
        EmployeeData[] employees_datas = get_employees_datas(num_of_employees);
        print_employees_datas(employees_datas);
    }
    
    static EmployeeData[] get_employees_datas(int num)
    {
        EmployeeData[] employees = new EmployeeData[num];
        for (int i = 0; i < num; i++)
        {
            EmployeeData data = new EmployeeData();
            Console.Write($"Enter name for employee {i + 1}: ");
            data.Name = Console.ReadLine();
            Console.Write($"Enter salary for employee {i + 1}: ");
            data.Salaries = Convert.ToDouble(Console.ReadLine());
            employees[i] = data;
        }
        return employees;
    }

    static void print_employees_datas(EmployeeData[] employees)
    {
        Console.WriteLine("\nEmployee List:");
        for (int i = 0; i < employees.Length; i++)
        {
            Console.WriteLine($"Name: {employees[i].Name}, Salary: {employees[i].Salaries}");
        }
    }
}

// QUESTION 4 

// SQL Select Questions:
/*Retrieve Employee Information
You have a table named "employees" with the following columns: employee_id, first_name, last_name, job_title, and salary.

Write an SQL query to retrieve the first name, last name, and job title of all employees whose salary is greater than $50000.

SELECT first_name, last_name, job_title
FROM employees
WHERE salary > 50000;

 Order and Limit
Consider a table named "orders" with columns: order_id, customer_name, order_date, and total_amount. 
Write an SQL query to retrieve the order_id, customer_name, and total_amount of the top 5 orders with the highest total amounts, ordered by total amount in descending order. 

SELECT order_id, customer_name, total_amount
FROM orders
ORDER BY total_amount DESC
LIMIT 5;
 */


