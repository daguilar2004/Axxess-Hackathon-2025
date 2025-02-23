<template>
           <button @click="logout" class="w-full p-2 text-white bg-blue-500 rounded-lg hover:bg-blue-600">Logout</button>

    <div class="chat-container">
      <div class="chat-box">
        <div v-for="(message, index) in messages" :key="index" 
             :class="message.sender === 'user' ? 'user-message' : 'bot-message'">
          {{ message.text }}
        </div>
      </div>
      <div class="input-container">
        <input v-model="userInput" @keyup.enter="sendMessage" placeholder="Type a message..." />
        <button @click="sendMessage">Send</button>
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
  
        // Store user message
        this.messages.push({ text: this.userInput, sender: "user" });
  
        // Save message to local file via backend API
        await axios.post("http://localhost:3000/save-message", { message: this.userInput });
  
        // Simulate bot response
        setTimeout(() => {
          this.messages.push({ text: "Got it! Let me analyze your symptoms...", sender: "bot" });
        }, 1000);
  
        this.userInput = "";
      }
    }
  };
  </script>
  
  <style>
  .chat-container {
    width: 100%;
    max-width: 400px;
    margin: auto;
  }
  
  .chat-box {
    height: 400px;
    overflow-y: auto;
    padding: 10px;
    background: #f4f4f4;
    border-radius: 8px;
  }
  
  .user-message {
    background: #3a3a3a;
    color: white;
    padding: 8px;
    margin: 5px;
    border-radius: 8px;
    text-align: right;
  }
  
  .bot-message {
    background: red;
    color: white;
    padding: 8px;
    margin: 5px;
    border-radius: 8px;
    text-align: left;
  }
  
  .input-container {
    display: flex;
    gap: 10px;
    margin-top: 10px;
  }
  
  input {
    flex: 1;
    padding: 8px;
  }
  
  button {
    padding: 8px;
    background: red;
    color: white;
    border: none;
    cursor: pointer;
  }
  </style>
  