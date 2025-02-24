<template>
  <div class="flex h-screen">
    <div class="w-52 bg-gray-200 p-5 flex flex-col">
      <div class="mb-4">
        <h2 class="text-lg font-semibold">Blossom Chat</h2>
      </div>
      <button @click="logout" class="bg-black text-white px-5 py-2 rounded-full mt-auto mx-auto mb-5">Logout</button>
      <div class="mt-4">
        <h3 class="text-sm font-semibold">Download Chat History</h3>
        <button @click="downloadChatHistory" class="bg-blue-500 text-white px-3 py-1 rounded mt-2">Download</button>
      </div>
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
  </div>
</template>

<script>
import { useAuth0 } from '@auth0/auth0-vue';
import axios from 'axios';

export default {
  setup() {
    const auth0 = useAuth0();
    return {
      logout() {
        auth0.logout({
          logoutParams: {
            returnTo: window.location.origin
          }
        });
      }
    };
  },
  data() {
    return {
      userInput: "",
      messages: [
        { text: "Hello! Please describe your symptoms.", sender: "bot" }
      ],
      chatState: "initial",
      symptoms: "",
    };
  },
  methods: {
    async sendMessage() {
      if (!this.userInput.trim()) return;

      const userMessage = { text: this.userInput, sender: "user" };
      this.messages.push(userMessage);
      await this.appendMessageToFile(`${userMessage.sender}: ${userMessage.text}`);

      if (this.chatState === "initial") {
        this.symptoms = this.userInput;
        this.messages.push({ text: "Do you have a fever?", sender: "bot" });
        await this.appendMessageToFile("bot: Do you have a fever?");
        this.chatState = "question1";
      } else if (this.chatState === "question1") {
        if (this.userInput.toLowerCase() === "yes") {
          this.messages.push({ text: "Do you have a cough?", sender: "bot" });
          await this.appendMessageToFile("bot: Do you have a cough?");
          this.chatState = "question2";
        } else if (this.userInput.toLowerCase() === "no") {
          this.messages.push({ text: "Do you have a sore throat?", sender: "bot" });
          await this.appendMessageToFile("bot: Do you have a sore throat?");
          this.chatState = "question2";
        } else {
          this.messages.push({ text: "Please answer with yes or no.", sender: "bot" });
        }
      } else if (this.chatState === "question2") {
        if (this.userInput.toLowerCase() === "yes") {
          this.messages.push({ text: "Based on your symptoms, you might have a common cold.", sender: "bot" });
          await this.appendMessageToFile("bot: Based on your symptoms, you might have a common cold.");
          this.chatState = "diagnosis";
        } else if (this.userInput.toLowerCase() === "no") {
          this.messages.push({ text: "Based on your symptoms, you might have allergies.", sender: "bot" });
          await this.appendMessageToFile("bot: Based on your symptoms, you might have allergies.");
          this.chatState = "diagnosis";
        } else {
          this.messages.push({ text: "Please answer with yes or no.", sender: "bot" });
        }
      } else if (this.chatState === "diagnosis") {
        this.messages.push({ text: "Would you like to share your report with a doctor?", sender: "bot" });
        await this.appendMessageToFile("bot: Would you like to share your report with a doctor?");
        this.chatState = "doctor";
      } else if (this.chatState === "doctor") {
        if (this.userInput.toLowerCase() === "yes") {
          this.messages.push({ text: "Your report has been shared with a doctor.", sender: "bot" });
          await this.appendMessageToFile("bot: Your report has been shared with a doctor.");
        } else {
          this.messages.push({ text: "Okay, your report will not be shared.", sender: "bot" });
          await this.appendMessageToFile("bot: Okay, your report will not be shared.");
        }
        this.chatState = "initial"; // Reset the chat
      }

      this.userInput = "";
    },
    async appendMessageToFile(message) {
      try {
        await axios.post('http://localhost:3000/append-chat', { message });
      } catch (error) {
        console.error('Error appending message:', error);
      }
    },
    downloadChatHistory() {
      const link = document.createElement('a');
      link.href = 'http://localhost:3000/download-chat-history';
      link.setAttribute('download', 'chat_history.txt');
      document.body.appendChild(link);
      link.click();
      document.body.removeChild(link);
    }
  }
};
</script>