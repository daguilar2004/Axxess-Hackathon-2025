<template>
  <div class="min-h-screen flex items-center justify-center bg-gray-100">
    <div class="bg-white p-8 rounded-lg shadow-md w-full max-w-md">
      <h1 class="text-2xl font-bold mb-6 text-center">Sign Up</h1>

      <!-- Email Field -->
      <div class="mb-4">
        <label for="email" class="block text-sm font-medium text-gray-700">Email:</label>
        <input
          type="email"
          id="email"
          class="mt-1 block w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-indigo-500 focus:border-indigo-500"
          placeholder="Enter your email"
        />
      </div>

      <!-- SSN Field -->
      <div class="mb-4">
        <label for="ssn" class="block text-sm font-medium text-gray-700">SSN:</label>
        <input
          type="text"
          id="ssn"
          class="mt-1 block w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-indigo-500 focus:border-indigo-500"
          placeholder="Enter your SSN"
        />
      </div>

      <!-- Doctor's Email Field -->
      <div class="mb-4">
        <label for="drEmail" class="block text-sm font-medium text-gray-700">Doctor's Email:</label>
        <input
          type="email"
          id="drEmail"
          class="mt-1 block w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-indigo-500 focus:border-indigo-500"
          placeholder="Enter your doctor's email"
        />
      </div>

      <!-- Password Field -->
      <div class="mb-6">
        <label for="password" class="block text-sm font-medium text-gray-700">Password:</label>
        <input
          type="password"
          id="password"
          class="mt-1 block w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-indigo-500 focus:border-indigo-500"
          placeholder="Enter your password"
        />
      </div>

      <!-- Submit Button -->
      <button
        @click="goToHomePage"
        class="w-full p-2 text-white bg-red-500 rounded-lg hover:bg-red-600 mb-2"
      >
        Submit
      </button>
    </div>
  </div>
</template>

<script>
// import axios from 'axios';
import { useRouter } from 'vue-router';

export default {
  setup(){
    const router = useRouter(); // Ensure this is initialized

      const goToHomePage = () => {
        router.push('/'); // Navigate to HomePage page
      };
      return{
        goToHomePage
      }

  },
};
</script>