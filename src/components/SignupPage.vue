<template>
    <div class="flex items-center justify-center min-h-screen bg-gray-100">
      <div class="w-full max-w-md p-6 bg-white rounded-2xl shadow-lg">
        <h2 class="text-2xl font-bold text-center text-gray-800 mb-4">Login</h2>
        <form @submit.prevent="handleLogin">
          <div class="mb-4">
            <label class="block text-gray-700">Email</label>
            <input v-model="email" type="email" required class="w-full p-2 border rounded-lg focus:outline-none focus:ring-2 focus:ring-blue-400" />
          </div>
          <div class="mb-4">
            <label class="block text-gray-700">Password</label>
            <input v-model="password" type="password" required class="w-full p-2 border rounded-lg focus:outline-none focus:ring-2 focus:ring-blue-400" />
          </div>
          <button type="submit" class="w-full p-2 text-white bg-blue-500 rounded-lg hover:bg-blue-600">Login</button>
        </form>
        <p class="text-center text-gray-600 mt-4">Don't have an account? <router-link to="/signup" class="text-blue-500">Sign Up</router-link></p>
      </div>
    </div>
  </template>
  
  <script>
  export default {
    name: "LoginPage",
    data() {
      return {
        email: '',
        password: ''
      };
    },
    methods: {
      handleLogin() {
        console.log('Logging in with:', this.email, this.password);
      }
    }
  };
  </script>
  
  <style scoped>
  /* Additional styling can go here */
  </style>
  