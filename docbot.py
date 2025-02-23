import google.generativeai as genai
import sys
import json

genai.configure(api_key="AIzaSyAEmyN32tkNpOAR7NmeDVL0Z1BBgcIB6es")

model = genai.GenerativeModel("gemini-pro")

#symptoms = "I have sore throat I feel cold all the time and I have a runny nose"
sick_list = []
question_list = []
symptoms = sys.argv[1]
question_list.append(symptoms) 
response = model.generate_content("You are part of a website that gives diagnostics to people based on symptoms, The user will provide you details on how they are feeling. Given the list of symptoms, list at most four plausible illnesses and come up with a series of questions that could assist in properly diagnosing the patient. In your response, format your questions where each line has a unique question, at the beginning of each question there is a ; charcter. In your possible illness list, format where each element has a $ in front."+symptoms)
iterator = iter(response.text.splitlines())
num_question = 0
with open("buf.json", "w", encoding="utf-8") as f:
    for line in iterator:
        ill = line.find("$") + 1
        start = line.find(";") + 1
        end = line.find("?", start)
        if ill > 0:
            sick_list.append(line[ill:])    
        if start > 0:
            question_list.append(line[start:end])
            num_question += 1
    json.dump(question_list, f)


# Save `sick_list` to a JSON file
with open("ill.json", "w", encoding="utf-8") as f:
        json.dump(sick_list, f)

