import google.generativeai as genai
import sys
import json

genai.configure(api_key="your key")
model = genai.GenerativeModel("gemini-pro")
with open("ill.json", "r", encoding="utf-8") as f:
        sick_list = json.load(f)

sick_joined = " -".join(sick_list)

with open("buf.json", "r", encoding="utf-8") as f:
        question_list = json.load(f)


question_joined = "\n".join(question_list[1:])

response = model.generate_content("Previously I asked you to diagnose the following symptoms: "+question_list[0]+
                                  " You said that these were the possible sicknessess -"+sick_joined+
                                  " You then asked the patient the following questions, their responses are at the end of each line \n"+question_joined+" Given the patient survey, symptoms and possible sicknesses, produce a formatted list of what might be wrong with the patient for and explain why in less than 4 sentences. Format like so: <sickness> | <explination>. Put a $ at the beginning of each listing and terminate each listing with = do not put **")

iterator = iter(response.text.splitlines())

proposed_illness = []
explanation = []

with open("diagnosis.json", "w", encoding="utf-8") as f:
    for line in iterator:
        ill = line.find("$") + 1
        ill_end = line.find("|")
        end = line.find("=")
        if ill > 0:
            proposed_illness.append(line[ill:ill_end])
            proposed_illness.append(line[ill_end+1:end])

    proposed_illness.append(" ");
    proposed_illness.append(" ");
    json.dump(proposed_illness, f)
        
for i in proposed_illness:
    print(i)
print(response.text)
