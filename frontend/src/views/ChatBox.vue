<template>
    <div class="flex h-screen">
      <div class="w-52 bg-gray-200 p-5 flex flex-col">
        <div class="mb-4">
          <h2 class="text-lg font-semibold">Blossom Chat</h2>
        </div>
        <button @click="viewPreviousChat" class="bg-blue-500 text-white px-5 py-2 rounded-full mb-3">View Previous Chat</button>
        <button @click="logout" class="bg-black text-white px-5 py-2 rounded-full mt-auto mx-auto mb-5">Logout</button>
      </div>
  
      <div class="flex-grow flex flex-col p-5">
        <div v-for="(message, index) in messages" :key="index" :class="[
          'p-3 mb-2 rounded-2xl max-w-2xl break-words',
          message.sender === 'bot' ? 'bg-red-500 text-white self-start' : 'bg-black text-white self-end',
        ]">
          {{ message.text }}
        </div>
  
        <div class="flex mt-auto border-t border-gray-300 pt-2">
          <input v-model="userInput" placeholder="Type a message..." @keyup.enter="sendMessage" class="flex-grow p-2 border border-gray-300 rounded-md mr-2 outline-none" />
          <button @click="sendMessage" class="bg-black text-white px-5 py-2 rounded-2xl">Send</button>
        </div>
      </div>
  
      <!-- Modal for Previous Chat -->
      <div v-if="showChatHistory" class="fixed inset-0 flex items-center justify-center bg-black bg-opacity-50">
        <div class="bg-white p-5 rounded-lg w-1/3">
          <h2 class="text-lg font-semibold mb-3">Previous Chat</h2>
          <div class="overflow-y-auto h-60 border p-2">
            <div v-if="chatHistory">
              <div v-for="(message, index) in chatHistory" :key="index" class="mb-2">
                <p :class="message.sender === 'bot' ? 'text-red-500' : 'text-black'">
                  <strong>{{ message.sender === 'bot' ? 'Bot' : 'User' }}:</strong> {{ message.text }}
                </p>
              </div>
            </div>
            <p v-else class="text-gray-500">No previous chats found.</p>
          </div>
          <button @click="showChatHistory = false" class="mt-3 bg-red-500 text-white px-4 py-2 rounded-lg">Close</button>
        </div>
      </div>
    </div>
  </template>
  
  <script>
  import { useAuth0 } from '@auth0/auth0-vue';
  import axios from 'axios';
  import { toRaw } from 'vue';
  
  export default {
    setup() {
      const auth0 = useAuth0();
      return {
        logout() {
          auth0.logout({ logoutParams: { returnTo: window.location.origin } });
        }
      };
    },
    data() {
      return {
        userInput: "",
        flag: "",
        messages: [
          { text: "Hello! How can I assist you today?", sender: "bot" }
        ],
        diagnostics: [],
        questions: [],
        formQ: [],
        len: "",
        iter: "0"
      };
    },
    methods: {
     async fetchMessages() {
      try {


        if (this.flag == "x"){
           console.log("lots of progres")
           const response = await axios.get("http://localhost:3000/get-res");
           this.messages = response.data;
           this.questions = this.messages.map(msg => toRaw(msg));
           let i = 0;
           while(i < this.questions.length){

             this.messages.push({ text: this.questions[i], sender: "bot" });

              i += 1;
            }
            this.flag = ""
            this.diagnostics = []
            this.questions = []
            this.formQ = []
            this.iter = "0"
        }else{
    
        this.flag = "r";

          if (this.iter == "0"){
           const response = await axios.get("http://localhost:3000/get-messages");
           this.messages = response.data;
           this.questions = this.messages.map(msg => toRaw(msg));
            this.len = parseInt(this.questions.length, 10);
        }
        let sub = Number(this.iter); 
         sub -= this.questions.length;
         console.log(sub);

        if (sub == -1 ){

            

            for (let i = 0; i < this.questions.length; i++) {
              if (i < this.questions.length) this.formQ.push(this.questions[i]);
              if (i < this.diagnostics.length) this.formQ.push(this.diagnostics[i]);
           }



            this.flag = "x";
            await axios.post("http://localhost:3000/save-message", { message: this.userInput, type: this.formQ});
            setTimeout(async () => {
              await this.fetchMessages();
            }, 100);  

        }else{ 
        
        console.log(this.questions[0])

          let inter = parseInt(this.iter, 10)
          inter += 1;
          this.iter = inter;


          this.messages.push({ text: this.questions[inter], sender: "bot" });
        console.log("wingo")
     }
       console.log("ended");


   }
      } catch (error) {
        console.error("Error fetching messages:", error);
    }
  },

      async sendMessage() {

                    console.log("msg!")
        if (!this.userInput.trim()) return;
  
        // Store user message
        this.messages.push({ text: this.userInput, sender: "user" });
        await this.saveMessage(this.userInput, "user"); 
  
        if (this.chatState === "initial") {
          this.symptoms = this.userInput;
          this.addBotMessage("Do you have a fever?", "question1");
        } else if (this.chatState === "question1") {
          this.handleYesNoResponse("Do you have a cough?", "Do you have a sore throat?", "question2");
        } else if (this.chatState === "question2") {
          this.handleDiagnosis();
        } else if (this.chatState === "diagnosis") {
          this.addBotMessage("Would you like to share your report with a doctor?", "doctor");
        } else if (this.chatState === "doctor") {
          this.handleDoctorResponse();
        }
  
        this.userInput = "";
      },
      
      async addBotMessage(text, nextState) {
        this.messages.push({ text, sender: "bot" });
        await this.saveMessage(text, "bot");
        this.chatState = nextState;
      },
  
      async handleYesNoResponse(yesResponse, noResponse, nextState) {
        const response = this.userInput.toLowerCase();
        if (response === "yes") {
          this.addBotMessage(yesResponse, nextState);
        } else if (response === "no") {
          this.addBotMessage(noResponse, nextState);
        } else {
          this.addBotMessage("Please answer with yes or no.", this.chatState);
        }
      },
  
      async handleDiagnosis() {
        const response = this.userInput.toLowerCase();
        if (response === "yes") {
          this.addBotMessage("Based on your symptoms, you might have a common cold.", "diagnosis");
        } else if (response === "no") {
          this.addBotMessage("Based on your symptoms, you might have allergies.", "diagnosis");
        } else {
          this.addBotMessage("Please answer with yes or no.", "question2");
        }
      },
  
      async handleDoctorResponse() {
        if (this.userInput.toLowerCase() === "yes") {
          this.addBotMessage("Your report has been shared with a doctor.", "initial");
        } else {
          this.addBotMessage("Okay, your report will not be shared.", "initial");
        }
      },
  
      async saveMessage(text, sender) {
        try {
          await axios.post("http://localhost:5000/save-message", { text, sender });
        } catch (error) {
          console.error("Error saving message:", error);
        }
      },
  
      async viewPreviousChat() {
        try {
          const response = await axios.get("http://localhost:5000/get-messages");
          if (response.data.success) {
            const messages = response.data.messages.split("\n").map(message => {
              const [sender, ...messageText] = message.split(": ");
              return { sender, text: messageText.join(": ") };
            });
            this.chatHistory = messages;
            this.showChatHistory = true;
          }
        } catch (error) {
          console.error("Error fetching chat history:", error);
        }
      }
    }
  };
  </script>
 