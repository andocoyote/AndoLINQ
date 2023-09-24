import json
import random

def generate_employee(id):
    first_names = ["John", "Jane", "Michael", "Emily", "William", "Olivia", "James", "Sophia", "Robert", "Ava"]
    last_names = ["Smith", "Johnson", "Williams", "Brown", "Jones", "Garcia", "Miller", "Davis", "Rodriguez", "Martinez"]
    
    first_name = random.choice(first_names)
    last_name = random.choice(last_names)
    annual_salary = random.randint(52000, 247000)
    is_manager = random.choice([True, False])
    department_id = random.randint(1, 7)
    
    employee = {
        "Id": id,
        "FirstName": first_name,
        "LastName": last_name,
        "AnnualSalary": annual_salary,
        "IsManager": is_manager,
        "DepartmentId": department_id
    }
    return employee

employees = [generate_employee(i + 1) for i in range(100)]

with open("employees.json", "w") as json_file:
    json.dump(employees, json_file, indent=2)
