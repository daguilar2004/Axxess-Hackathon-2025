<template>
    <div class="flex h-screen">
      <div class="w-52 bg-gray-200 p-5 flex flex-col">
        <div class="mb-4">
          <h2 class="text-lg font-semibold">Blossom Chat</h2>
        </div>
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
    </div>
  </template>
  
  <script>
  import axios from 'axios';
  import { useAuth0 } from '@auth0/auth0-vue';
  
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
          { text: "Hello! How can I assist you today?", sender: "bot" }
        ]
      };
    },
    methods: {
      async sendMessage() {
        if (!this.userInput.trim()) return;
  
        this.messages.push({ text: this.userInput, sender: "user" });
  
        await axios.post("http://localhost:3000/save-message", { message: this.userInput });
  
        setTimeout(() => {
          this.messages.push({ text: "Got it! Let me analyze your symptoms...", sender: "bot" });
        }, 1000);
  
        this.userInput = "";
      },
    }
  };
  </script>