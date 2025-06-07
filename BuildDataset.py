from faker import Faker
import csv
import random

fake = Faker()

output_file = "people_dataset.csv"

genders = ["Male", "Female"]

with open(output_file, mode="w", newline="", encoding="utf-8") as file:
    writer = csv.writer(file)

    writer.writerow(["FirstName", "LastName", "Age", "WeightLbs", "Gender"])

    for _ in range(1000):
        first_name = fake.first_name()
        last_name = fake.last_name()
        age = random.randint(18, 70)
        weight = round(random.uniform(100, 300), 1)
        gender = random.choice(genders)
        writer.writerow([first_name, last_name, age, weight, gender])

